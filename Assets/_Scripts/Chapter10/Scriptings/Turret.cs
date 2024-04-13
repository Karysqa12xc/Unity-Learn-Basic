using UnityEngine;
namespace Chapter.BehaviorSimulationAndAi
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] Transform weapon;
        [SerializeField] Transform target;
        [SerializeField] float range = 5f;
        [SerializeField] float arc = 45;
        StateMachine stateMachine;

        // Start is called before the first frame update
        void Start()
        {
            stateMachine = new StateMachine();
            var searching = stateMachine.CreateState("searching");
            searching.onEnter = delegate {
                Debug.Log("Now searching for the target...");
            };
            searching.onFrame = delegate{
                var angle = Mathf.Sin(Time.time) * arc / 2f;
                weapon.eulerAngles = Vector3.up * angle;
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if(distanceToTarget < range){
                    stateMachine.TransitionTo("aiming");

                }
            };
            var aiming = stateMachine.CreateState("aiming");
            aiming.onFrame = delegate{
                weapon.LookAt(target.transform);
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if(distanceToTarget > range){
                    stateMachine.TransitionTo("searching");

                }
            };
            aiming.onEnter = delegate{
                Debug.Log("Target is in range");
            };
            aiming.onExit = delegate{
                Debug.Log("Target went out of range");
            };
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
        }
    }
}

