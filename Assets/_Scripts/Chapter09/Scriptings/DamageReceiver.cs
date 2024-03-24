using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Chapter.LogicAndGameplay
{
    public class DamageReceiver : MonoBehaviour
    {
        UnityEvent onDeath;
        [SerializeField] int hitPoints = 5;

        int currentHitPoints;

        private void Awake()
        {
            currentHitPoints = hitPoints;
        }
        public void TakeDamage(int damageAmount)
        {
            currentHitPoints -= damageAmount;
            if (currentHitPoints <= 0)
            {
                if (onDeath != null)
                {
                    onDeath.Invoke();
                }
                Destroy(gameObject);
            }
        }
    }
}

