using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.LogicAndGameplay
{

#if UNITY_EDITOR
    using UnityEditor;
#endif
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] public bool isLapStart;
        [SerializeField] public CheckPoint next;
        internal int index;
        private void OnDrawGizmos()
        {
            if (isLapStart)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.blue;
            }
            Gizmos.DrawSphere(transform.position, 0.5f);
            if (next != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, next.transform.position);
            }
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(CheckPoint))]
    public class CheckPointEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var checkpoint = this.target as CheckPoint;

            if (GUILayout.Button("Insert Checkpoint"))
            {
                var newCheckPoint = new GameObject("CheckPoint").AddComponent<CheckPoint>();
                newCheckPoint.next = checkpoint.next;
                checkpoint.next = newCheckPoint;
                newCheckPoint.transform.SetParent(checkpoint.transform.parent, true);
                var nextSiblingIndex = checkpoint.transform.GetSiblingIndex() + 1;
                newCheckPoint.transform.SetSiblingIndex(nextSiblingIndex);

                newCheckPoint.transform.position = checkpoint.transform.position + new Vector3(1, 0, 0);
            }
            var disableRemoveButton = checkpoint.next == null || checkpoint.next.isLapStart;

            using (new EditorGUI.DisabledGroupScope(disableRemoveButton))
            {
                if (GUILayout.Button("Remove Next Checkpoint"))
                {
                    var next = checkpoint.next.next;
                    DestroyImmediate(checkpoint.next.gameObject);
                    checkpoint.next = next;
                }
            }
        }
    }
#endif
}
