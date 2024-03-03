using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Scripting
{
    public class Mover : MonoBehaviour
    {
        public Vector3 direction = Vector3.up;
        [SerializeField]
        float speed = 0.1f;
        // Start is called before the first frame update
        void Start()
        {
            MoverManager.instance.ManagerMover();
            SimpleSingleton.instance.DoSomething();
            StartCoroutine(MoverManager.instance.LogAfterDelay());
        }
        // Update is called once per frame
        void Update()
        {
            var movement = direction * speed;
            movement *= Time.deltaTime;
            this.transform.Translate(movement);
        }
    }
}

