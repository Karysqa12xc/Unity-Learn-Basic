using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.LogicAndGameplay
{

    public class DamageGiver : MonoBehaviour
    {
        [SerializeField] int damageAmount = 1;
        private void OnCollisionEnter(Collision collision)
        {
            var otherDameReceiver = collision.gameObject.GetComponent<DamageReceiver>();

            if(otherDameReceiver != null){
                otherDameReceiver.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
    }
}
