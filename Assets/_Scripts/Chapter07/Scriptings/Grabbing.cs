using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Chapter.PhysicsAndCharacterCtrl
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grabbing : MonoBehaviour
    {
        [SerializeField] float grabbingRange = 3;
        [SerializeField] float pullingRange = 20;
        [SerializeField] Transform holdPoint = null;
        [SerializeField] KeyCode grabKey = KeyCode.F;
        [SerializeField] KeyCode throwKey = KeyCode.Mouse0;
        [SerializeField] float throwForce = 100f;
        [SerializeField] float pullForce = 50f;
        [SerializeField] float grabBreakingForce = 100f;
        [SerializeField] float grabBreakingTorque = 100f;
        [SerializeField] int limitMassCanPick = 10;
        FixedJoint grabJoint;
        Rigidbody grabbedRigidbody;
        private void Awake()
        {
            if (holdPoint == null)
            {
                Debug.LogError("Grab hold point must not be null");
            }
            if (holdPoint.IsChildOf(transform) == false)
            {
                Debug.LogError("Grab hold point must be a child of this object");
            }
            var playerCollider = GetComponentInParent<Collider>();
            playerCollider.gameObject.layer = LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            if (Input.GetKey(grabKey) && grabJoint == null)
            {
                AttemptPull();
                Debug.Log("AttemptPull");
            }
            else if (Input.GetKeyDown(grabKey) && grabJoint != null)
            {
                Drop();
                Debug.Log("Drop");
            }
            else if (Input.GetKeyDown(throwKey) && grabJoint != null)
            {
                Throw();
                Debug.Log("Throw");
            }
        }

        private void Throw()
        {
            if (grabbedRigidbody == null)
            {
                return;
            }
            var throwBody = grabbedRigidbody;
            var force = transform.forward * throwForce;

            throwBody.AddForce(force);
            Drop();
        }



        public void AttemptPull()
        {
            var ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            var everythingExceptPlayers = ~(1 << LayerMask.NameToLayer("Player"));

            var layerMask = Physics.DefaultRaycastLayers & everythingExceptPlayers;

            var hitSomething = Physics.Raycast(ray, out hit, pullingRange, layerMask);

            if (hitSomething == false)
            {
                return;
            }
            grabbedRigidbody = hit.rigidbody;

            if (grabbedRigidbody == null || grabbedRigidbody.isKinematic)
            {
                return;
            }
            if (grabbedRigidbody.mass > limitMassCanPick)
            {
                Debug.Log("Can't pick");
                return;
            }
            if (hit.distance < grabbingRange)
            {
                grabbedRigidbody.transform.position = holdPoint.position;
                grabJoint = gameObject.AddComponent<FixedJoint>();
                grabJoint.connectedBody = grabbedRigidbody;
                grabJoint.breakForce = grabBreakingForce;
                grabJoint.breakTorque = grabBreakingTorque;

                foreach (var myCollider in GetComponentsInParent<Collider>())
                {
                    Physics.IgnoreCollision(myCollider, hit.collider, true);
                }
            }
            else
            {
                var pull = -transform.forward * this.pullForce;
                grabbedRigidbody.AddForce(pull);
            }
        }
        private void Drop()
        {
            if (grabJoint != null)
            {
                Destroy(grabJoint);
            }
            if (grabbedRigidbody == null)
            {
                return;
            }
            foreach (var myCollider in GetComponentsInParent<Collider>())
            {
                Physics.IgnoreCollision(myCollider, grabbedRigidbody.GetComponent<Collider>(), false);
            }
            grabbedRigidbody = null;
        }
        void OnDrawGizmos()
        {
            if (holdPoint == null)
            {
                return;
            }
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(holdPoint.position, .02f);
        }
        void OnJointBreak(float breakForce)
        {
            Drop();
        }
    }
}

