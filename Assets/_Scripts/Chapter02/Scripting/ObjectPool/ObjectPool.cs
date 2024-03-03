using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
namespace Chapter.Scripting.ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;

        private Queue<GameObject> inactiveObject = new Queue<GameObject>();

        public GameObject GetObject()
        {
            if (inactiveObject.Count > 0)
            {
                var dequeuedObject = inactiveObject.Dequeue();

                dequeuedObject.transform.parent = null;
                dequeuedObject.SetActive(true);

                var notifiers = dequeuedObject.GetComponents<IObjectPoolNotifier>();

                foreach (var notifier in notifiers)
                {
                    notifier.OnCreatedOfDequeuedFromPool(false);
                }
                return dequeuedObject;
            }else{
                var newObject = Instantiate(prefab);
                var poolTag = newObject.AddComponent<PooledObject>();
                poolTag.owner = this;

                poolTag.hideFlags = HideFlags.HideInInspector;
                
                var notifiers = newObject.GetComponents<IObjectPoolNotifier>();

                foreach (var notifier in notifiers)
                {
                    notifier.OnCreatedOfDequeuedFromPool(true);
                }
                return newObject;
            }
        }
        
        public void ReturnObject(GameObject gameObject)
        {
            var notifiers = gameObject.GetComponents<IObjectPoolNotifier>();

            foreach (var notifier in notifiers)
            {
                notifier.OnEnqueuedToPool();
            }

            gameObject.SetActive(false);
            gameObject.transform.parent = this.transform;

            inactiveObject.Enqueue(gameObject);
        }
    }
}

