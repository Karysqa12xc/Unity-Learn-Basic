using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Mathematics
{
    public class AngleChecker : MonoBehaviour
    {
        [SerializeField] Transform target;
    
        // Update is called once per frame
        void Update()
        {
            var directionToTarget = (target.position - transform.position).normalized;

            var dotProduct = Vector3.Dot(transform.forward, directionToTarget);

            var angle = Mathf.Acos(dotProduct);
            Debug.LogFormat("The angle between my forward direction and {0} is {1:F1}",
                target.name, angle * Mathf.Rad2Deg);
        }
    }

}
