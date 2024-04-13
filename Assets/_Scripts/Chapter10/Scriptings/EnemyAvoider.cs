using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Chapter.BehaviorSimulationAndAi
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAvoider : MonoBehaviour
    {
        [SerializeField] EnemyVisibility visibility = null;
        [SerializeField] float searchAreaSize = 10f;
        [SerializeField] float searchCellSize = 1f;
        [SerializeField] bool visualize = true;
        NavMeshAgent agent;
        // Start is called before the first frame update
        IEnumerator Start()
        {
            agent = GetComponent<NavMeshAgent>();
            while(true){
                if(visibility.targetIsVisible){
                    Vector3 hidingSpot;
                    if(FindHidingSpot(out hidingSpot) == false){
                        yield return new WaitForSeconds(1.0f);
                        continue;
                    }
                    agent.destination = hidingSpot;
                }
                yield return new WaitForSeconds(0.1f);
            }  
        }
        bool FindHidingSpot(out Vector3 hidingSpot){
            var distribution = new PoissonDiscSampler(searchAreaSize, searchAreaSize,searchCellSize);
            var candidateHidingSpot = new List<Vector3>();
            foreach (var point in distribution.Samples())
            {
                var searchPoint = point;
                searchPoint.x -= searchAreaSize / 2f;
                searchPoint.y -= searchAreaSize / 2f;
                var searchPointLocalSpace = new Vector3(searchPoint.x, transform.localPosition.x, searchPoint.y);
                NavMeshHit hit;
                bool foundPoint;
                foundPoint = NavMesh.SamplePosition(searchPointLocalSpace, out hit, 5, NavMesh.AllAreas);
                if(foundPoint == false){
                    continue;
                }
                searchPointLocalSpace = hit.position;
                var canSee = visibility.CheckVisibilityToPoint(searchPointLocalSpace);
                if(canSee == false){
                    candidateHidingSpot.Add(searchPointLocalSpace);
                }
                if(visualize){
                    Color debugColor = canSee ? Color.red : Color.green;
                    Debug.DrawLine(transform.position, searchPointLocalSpace, debugColor, 0.1f);
                }
            }
            if(candidateHidingSpot.Count == 0){
                hidingSpot = Vector3.zero;
                return false;

            }
            List<KeyValuePair<Vector3, float>> paths;
            paths = candidateHidingSpot.ConvertAll((Vector3 point) => {
                var path = new NavMeshPath();
                agent.CalculatePath(point, path);
                float distance;
                if(path.status != NavMeshPathStatus.PathComplete){
                    distance = Mathf.Infinity;
                }else{
                    var corners = new Vector3[32];
                    var cornerCount = path.GetCornersNonAlloc(corners);
                    var current = corners[0];
                    distance = 0;
                    for(int c = 1; c < cornerCount; c++){
                        var next = corners[c];
                        distance += Vector3.Distance(current, next); 
                        current = next;
                    }
                }
                return new KeyValuePair<Vector3, float>(point, distance);
            });
            paths.Sort((a, b) => {
                return a.Value.CompareTo(b.Value);
            });
            hidingSpot = paths[0].Key;
            return true;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}

