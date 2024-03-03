using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Scripting.ObjectPool
{
    public class ObjectPoolDemo : MonoBehaviour
    {
        [SerializeField]
        private ObjectPool pool;
        // Start is called before the first frame update
        IEnumerator Start()
        {
            while (true)
            {
                var o = pool.GetObject();

                var position = Random.insideUnitCircle * 4;

                o.transform.position = position;
                var delay = Random.Range(0.1f, 0.5f);

                yield return new WaitForSeconds(delay);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
