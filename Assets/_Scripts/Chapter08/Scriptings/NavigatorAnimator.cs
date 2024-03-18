using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;
using UnityEngine.AI;
namespace Chapter.AnimationAndMovement
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class NavigatorAnimator : MonoBehaviour
    {
        enum Mode
        {
            AgentControlsPosition,
            AnimatorControlsPosition
        }
        [SerializeField] Mode mode = Mode.AnimatorControlsPosition;

        [SerializeField] string isMovingParameterName = "Moving";
        [SerializeField] string sidewaysSpeedParameterName = "X Speed";
        [SerializeField] string forwardSpeedParameterName = "Z Speed";

        Animator animator;
        NavMeshAgent agent;
        Vector2 smoothDeltaPosition = Vector2.zero;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            agent.updatePosition = false;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

            float xMovement = Vector3.Dot(transform.right, worldDeltaPosition);
            float zMovement = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 localDeltaPosition = new Vector2(xMovement, zMovement);
            float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);

            smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, localDeltaPosition, smooth);
            var velocity = smoothDeltaPosition / Time.deltaTime;

            bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;
            animator.SetBool(isMovingParameterName, shouldMove);
            animator.SetFloat(sidewaysSpeedParameterName, velocity.x);
            animator.SetFloat(forwardSpeedParameterName, velocity.y);
            if(mode == Mode.AnimatorControlsPosition){
                if(worldDeltaPosition.magnitude > agent.radius){
                    transform.position = Vector3.Lerp(transform.position, agent.nextPosition, Time.deltaTime / 0.15f);
                }
            }
        }
        void OnAnimatorMove()
        {
            switch(mode){
                case Mode.AgentControlsPosition:
                    transform.position = agent.nextPosition;
                    break;
                case Mode.AnimatorControlsPosition:
                    Vector3 position = animator.rootPosition;
                    position.y = agent.nextPosition.y;
                    transform.position = position;
                    break;
            }
        }
    }
}

