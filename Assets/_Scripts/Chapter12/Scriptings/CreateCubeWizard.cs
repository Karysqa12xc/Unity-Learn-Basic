using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.UserInterface
{
#if UNITY_EDITOR
    using UnityEditor;
    public class CreateCubeWizard : ScriptableWizard
    {
        [MenuItem("GameObject/Cube with Color")]
        public static void CreateWizard()
        {
            DisplayWizard<CreateCubeWizard>("Create Cube");
        }
        [SerializeField] Vector3 size = Vector3.zero;
        [SerializeField] Color color = Color.white;

        private void OnWizardCreate() {
            var newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newCube.transform.localScale = size;
            var tintedMaterial = new Material(Shader.Find("Standard"));

            tintedMaterial.color = color;
            var desiredPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Tinted.mat");
            AssetDatabase.CreateAsset(tintedMaterial, desiredPath);
            AssetDatabase.SaveAssets();

            EditorGUIUtility.PingObject(tintedMaterial);
            newCube.GetComponent<MeshRenderer>().material = tintedMaterial;


        }
    }
#endif
}

