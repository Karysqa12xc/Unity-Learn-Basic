using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.PhysicsAndCharacterCtrl
{
    [RequireComponent(typeof(CharacterController))]
    public class PlatformRiding : MonoBehaviour
    {
        [SerializeField] CharacterController controller;
        // Start is called before the first frame update
        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            var capsulePoint1 = transform.position +
            new Vector3(0, (controller.height / 2)
            - controller.radius, 0);

            var capsulePoint2 = transform.position -
            new Vector3(0, (controller.height / 2)
            + controller.radius, 0);


            Collider[] overlappingColliders = new Collider[10];

            var overlapCount = Physics.OverlapCapsuleNonAlloc(
                capsulePoint1,
                capsulePoint2,
                controller.radius,
                overlappingColliders
            );
            
            for (int i = 0; i < overlapCount; i++)
            {
                var overlappingCollider = overlappingColliders[i];

                if (overlappingCollider == controller)
                {
                    continue;
                }
                Vector3 direction;
                float distance;

                bool penetration = 
                Physics.ComputePenetration(
                    controller,
                    transform.position,
                    transform.rotation,
                    overlappingCollider,
                    overlappingCollider.transform.position,
                    overlappingCollider.transform.rotation,
                    out direction,
                    out distance
                );
                if (penetration)
                {
                    direction.y = 0;
                    Debug.LogFormat("{0}", direction * distance);
                    Debug.LogFormat("{0}-{1}", direction.x, distance);
                    // transform.position += direction * distance;
                }

            }
            //Phần chính để vật thể có thể đi theo vật thể đang cưỡi
            var ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            float maxDistance = (controller.height / 2f) + 0.1f;
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                var platform = hit.collider.gameObject.GetComponent<MovingPlatform>();
                if (platform != null)
                {
                    transform.position += platform.velocity * Time.deltaTime;
                }

            }
        }

    }
}
