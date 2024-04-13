using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Chapter.BehaviorSimulationAndAi
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MoveToPoint : MonoBehaviour
    {
        NavMeshAgent agent;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0)){
                var mousePosition = Input.mousePosition;

                var ray = Camera.main.ScreenPointToRay(mousePosition);

                RaycastHit hit;

                if(Physics.Raycast(ray, out hit)){
                    var selectedPoint = hit.point;

                    agent.destination = selectedPoint;
                }
            }
        }
    }
}

