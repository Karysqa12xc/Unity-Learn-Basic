using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using LitJson;
using UnityEngine.SceneManagement;

namespace Chapter.FilesNetworking
{
    public interface ISaveable
    {
        string SaveID { get; set; }
        JsonData SaveData { get; }
        void LoadFromData(JsonData data);

    }
    public static class SavingService
    {
        private const string ACTIVE_SCENE_KEY = "activeScene";
        private const string SCENE_KEY = "scenes";
        private const string OBJECT_KEY = "objects";
        private const string SAVEID_KEY = "$saveID";
        static UnityEngine.Events.UnityAction<Scene, LoadSceneMode> LoadObjectsAfterSceneLoad;


        public static void SaveGame(string filename)
        {
            var results = new JsonData();
            var allSaveableObjects = Object
            .FindObjectsOfType<MonoBehaviour>()
            .OfType<ISaveable>();
            if (allSaveableObjects.Count() > 0)
            {
                var saveObjects = new JsonData();
                foreach (var saveableObject in allSaveableObjects)
                {
                    var data = saveableObject.SaveData;
                    if (data.IsObject)
                    {
                        data[SAVEID_KEY] = saveableObject.SaveID;
                        saveObjects.Add(data);
                    }
                    else
                    {
                        var behavior = saveableObject as MonoBehaviour;
                        Debug.LogWarningFormat(behavior, "{0}'s save data is not dictionary. The "
                         + " object was not saved.", behavior.name);
                    }
                }
                results[OBJECT_KEY] = saveObjects;
            }
            else
            {
                Debug.LogWarningFormat(
                    "The scene did not include any saveable objects.");
            }
            var openScenes = new JsonData();
            var sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                openScenes.Add(scene.name);
            }
            results[SCENE_KEY] = openScenes;
            results[ACTIVE_SCENE_KEY] = SceneManager.GetActiveScene().name;
            var outputPath = Path.Combine(Application.persistentDataPath, filename);
            var writer = new JsonWriter();
            writer.PrettyPrint = true;
            results.ToJson(writer);
            File.WriteAllText(outputPath, writer.ToString());
            Debug.LogFormat("Wrote saved game to {0}", outputPath);
            results = null;
            System.GC.Collect();
        }

        public static bool LoadGame(string filename)
        {
            var dataPath = Path.Combine(Application.persistentDataPath, filename);
            if (File.Exists(dataPath) == false)
            {
                Debug.LogErrorFormat("No file exists at {0}", dataPath);
                return false;
            }
            var text = File.ReadAllText(dataPath);
            var data = JsonMapper.ToObject(text);
            if (data == null || data.IsObject == false)
            {
                Debug.LogErrorFormat(
                    "Data at {0} is not a JSON object", dataPath);
                return false;
            }
            if (!data.ContainsKey("scenes"))
            {
                Debug.LogWarningFormat(
                    "Data at {0} does not contain any scenes; not " +
                    "loading any!",
                    dataPath
                );
                return false;
            }
            var scenes = data[SCENE_KEY];
            int sceneCount = scenes.Count;
            if (sceneCount == 0)
            {
                Debug.LogWarningFormat(
                    "Data at {0} doesn't specify any scenes to load.",
                    dataPath
                );
                return false;
            }
            for (int i = 0; i < sceneCount; i++)
            {
                var scene = (string)scenes[i];
                if (i == 0)
                {
                    SceneManager.LoadScene(scene, LoadSceneMode.Single);
                }
                else
                {
                    SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                }
            }
            if (data.ContainsKey(ACTIVE_SCENE_KEY))
            {
                var activeSceneName = (string)data[ACTIVE_SCENE_KEY];
                var activeScene = SceneManager.GetSceneByName(activeSceneName);
                if (activeScene.IsValid() == false)
                {
                    Debug.LogErrorFormat(
                        "Data at {0} specifies an active scene that " +
                        "doesn't exist. Stopping loading here.",
                        dataPath
                    );
                    return false;
                }
                SceneManager.SetActiveScene(activeScene);
            }else{
                    Debug.LogWarningFormat("Data at {0} does not specify an " +
                        "active scene.", dataPath);
            }
            if(data.ContainsKey(OBJECT_KEY)){
                var objects = data[OBJECT_KEY];
                LoadObjectsAfterSceneLoad = (scene, loadSceneMode) => {
                    var allLoadableObjects = Object
                    .FindObjectsOfType<MonoBehaviour>()
                    .OfType<ISaveable>()
                    .ToDictionary(o => o.SaveID, o => o);
                    
                    var objectCount = objects.Count;
                    for(int i = 0; i < objectCount; i++){
                        var objectData = objects[i];
                        var saveID = (string) objectData[SAVEID_KEY];
                        if(allLoadableObjects.ContainsKey(saveID)){
                            var loadableObject = allLoadableObjects[saveID];
                            loadableObject.LoadFromData(objectData);
                        }
                    }
                    SceneManager.sceneLoaded -= LoadObjectsAfterSceneLoad;
                    LoadObjectsAfterSceneLoad = null;
                    System.GC.Collect();
                };
                SceneManager.sceneLoaded += LoadObjectsAfterSceneLoad;
            }
            return true;
        }

    }
}

