using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace Chapter.UserInterface
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEngine.AI;
#endif
    public class Wall : MonoBehaviour
    {
        [SerializeField] public int rows = 5;
        [SerializeField] public int columns = 5;
        [SerializeField] public Renderer brickPrefabs;

    }
#if UNITY_EDITOR
    [CustomEditor(typeof(Wall))]
    public class WallEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rows"));

            EditorGUILayout.PropertyField(
                serializedObject.FindProperty("columns")
            );

            EditorGUILayout.PropertyField(
                serializedObject.FindProperty("brickPrefabs")
            );
            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Create Wall"))
            {
                CreateWall();
            }
        }
        public void CreateWall()
        {
            Undo.RegisterFullObjectHierarchyUndo(target, "Create Wall");
            var wall = target as Wall;
            if (wall == null)
            {
                return;
            }
            GameObject[] allChildren = new GameObject[wall.transform.childCount];
            int i = 0;
            foreach (Transform child in wall.transform)
            {
                allChildren[i] = child.gameObject;
                i++;
            }
            foreach(GameObject child in allChildren){
                DestroyImmediate(child.gameObject);
            }
            var brickSize = wall.brickPrefabs.GetComponent<Renderer>().bounds.size;
            for(int row = 0; row < wall.rows; row++){
                var rowPosition = Vector3.zero;
                rowPosition.y += brickSize.y * row;
                for(int column = 0; column < wall.columns; column++){
                    var columnPosition = rowPosition;
                    columnPosition.x += brickSize.x * column;
                    if(row % 2 == 0){
                        columnPosition.x += brickSize.x / 2f;
                    }
                    var brick = PrefabUtility.InstantiatePrefab(wall.brickPrefabs.gameObject) as GameObject;
                    brick.name = string.Format("{0} ({1}, {2})", wall.brickPrefabs.name, column, row);
                    brick.transform.SetParent(wall.transform, false);
                    brick.transform.localPosition = columnPosition;
                    brick.transform.localRotation = Quaternion.identity;
                    
                }
            }
            

        }
    }
#endif
}

