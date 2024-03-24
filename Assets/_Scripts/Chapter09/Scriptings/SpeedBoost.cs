using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace Chapter.LogicAndGameplay
{
    public class SpeedBoost : MonoBehaviour
    {
        [SerializeField] float boostDuration = 1;

        [SerializeField] float boostForce = 50;

        private void OnTriggerEnter(Collider other) {
            var body = other.GetComponentInParent<Rigidbody>();
            Debug.Log(body.name);
            if(body == null){
                return;
            }
            var boost = body.gameObject.AddComponent<ConstantForce>();
            boost.relativeForce = Vector3.forward * boostForce;

            Destroy(boost, boostDuration);
        }
    }
}

