using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Chapter.LogicAndGameplay
{

    public class OrbitingCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        [SerializeField] float rotationSpeed = 120.0f;
        [SerializeField] float elevationSpeed = 120.0f;

        [SerializeField] float elevationMinLimit = -20f;
        [SerializeField] float elevationMaxLimit = 80;

        [SerializeField] float distance = 5.0f;
        [SerializeField] float distanceMin = .5f;
        [SerializeField] float distanceMax = 15f;
        float rotationAroundTarget = 0.0f;
        float elevationToTarget = 0.0f;
        [SerializeField] bool clipCamera;
        // Start is called before the first frame update
        void Start()
        {
            Vector3 angles = transform.eulerAngles;
            rotationAroundTarget = angles.y;
            elevationToTarget = angles.x;
            if (target)
            {
                float currentDistance =
                    (transform.position - target.position).magnitude;

                distance = Mathf.Clamp(currentDistance, distanceMin, distanceMax);
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (target)
            {
                rotationAroundTarget += Input.GetAxis("Mouse X") * rotationSpeed * distance * 0.02f;

                elevationToTarget -= Input.GetAxis("Mouse Y") * elevationSpeed * 0.02f;

                elevationToTarget = ClampAngle(elevationToTarget, elevationMinLimit, elevationMaxLimit);
                Quaternion rotation = Quaternion.Euler(elevationToTarget, rotationAroundTarget, 0);

                distance = distance - Input.GetAxis("Mouse ScrollWheel") * 5;

                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = rotation * negDistance + target.position;

                transform.position = position;

                transform.rotation = rotation;
                if (clipCamera)
                {
                    RaycastHit hitInfo;

                    var ray = new Ray(target.position, position - target.position);
                    var hit = Physics.Raycast(ray, out hitInfo, distance);
                    if (hit)
                    {
                        position = hitInfo.point;
                    }
                }
            }
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
            {
                angle -= 360F;
            }
            return Mathf.Clamp(angle, min, max);

        }
    }
}
