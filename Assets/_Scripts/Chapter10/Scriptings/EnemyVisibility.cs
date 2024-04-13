using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.BehaviorSimulationAndAi
{
#if UNITY_EDITOR
    using UnityEditor;
#endif
    public class EnemyVisibility : MonoBehaviour
    {
        public Transform target = null;
        public float maxDistance = 10f;
        [Range(0f, 360f)]
        public float angle = 45f;
        [SerializeField] bool visualize = true;
        public bool targetIsVisible { get; private set; }
        // Update is called once per frame
        void Update()
        {
            targetIsVisible = CheckVisibility();
            if (visualize)
            {
                var color = targetIsVisible ? Color.yellow : Color.white;

                GetComponent<Renderer>().material.color = color;
            }
        }

        public bool CheckVisibility()
        {
            var directionToTarget = target.position - transform.position;
            var degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);
            var withinArc = degreesToTarget < (angle / 2);
            if (withinArc == false)
            {
                return false;
            }
            var distanceToTarget = directionToTarget.magnitude;

            var rayDistance = Mathf.Min(maxDistance, distanceToTarget);
            var ray = new Ray(transform.position, directionToTarget);
            RaycastHit hit;
            var canSee = false;
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.transform == target)
                {
                    canSee = true;
                }
                Debug.DrawLine(transform.position, hit.point);
            }
            else
            {
                Debug.DrawRay(transform.position, directionToTarget.normalized * rayDistance);
            }
            return canSee;
        }
        public bool CheckVisibilityToPoint(Vector3 worldPoint)
        {
            var directionToTarget = worldPoint - transform.position;

            var degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);

            var withinArc = degreesToTarget < (angle / 2);

            if (withinArc == false)
            {
                return false;
            }
            var distanceToTarget = directionToTarget.magnitude;
            var rayDistance = Mathf.Min(maxDistance, distanceToTarget);
            var ray = new Ray(transform.position, directionToTarget);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.transform == target)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }

        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyVisibility))]
    public class EnemyVisibilityEditor : Editor
    {
        private void OnSceneGUI()
        {
            var visibility = target as EnemyVisibility;
            Handles.color = new Color(1, 1, 1, 0.1f);
            var forwardPointMinusHalfAngle = Quaternion.Euler(0, -visibility.angle / 2, 0)
                            * visibility.transform.forward;
            Vector3 arcStart = forwardPointMinusHalfAngle * visibility.maxDistance;
            Handles.DrawSolidArc(
                visibility.transform.position,
                Vector3.up,
                arcStart,
                visibility.angle,
                visibility.maxDistance
            );

            Handles.color = Color.white;
            Vector3 handlePosition = visibility.transform.position 
                + visibility.transform.forward * visibility.maxDistance;
            visibility.maxDistance = Handles.ScaleValueHandle(
                visibility.maxDistance,
                handlePosition,
                visibility.transform.rotation,
                1,
                Handles.ConeHandleCap,
                0.25f
            );
        }
    }
    #endif
}

