using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.LogicAndGameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class AdjustCenterMass : MonoBehaviour
    {
        [SerializeField] Vector3 centerOfMass = Vector3.zero;
        // Start is called before the first frame update
        private void Start()
        {
            GetComponent<Rigidbody>().centerOfMass += centerOfMass;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            var currentCenterOfMass = this.GetComponent<Rigidbody>().worldCenterOfMass;
            Gizmos.DrawSphere(currentCenterOfMass + centerOfMass, 0.125f);
        }
    }
}

