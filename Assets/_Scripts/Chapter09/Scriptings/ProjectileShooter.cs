using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Chapter.LogicAndGameplay
{
    public class ProjectileShooter : MonoBehaviour
    {
        [SerializeField] GameObject projectilePrefab = null;
        
        [SerializeField] float timeBetweenShots = 1;
        [SerializeField] float projectileSpeed = 10;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(ShootProjectiles());
        }
        IEnumerator ShootProjectiles(){
            while(true){
                ShootNewProjectile();
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }

        void ShootNewProjectile(){
            var projectile = Instantiate(projectilePrefab, 
            transform.position, 
            transform.rotation);

            var rigidbody = projectile.GetComponent<Rigidbody>();

            if(rigidbody == null){
                UnityEngine.Debug.LogError("Projectile prefabs has no rigidbody");
                return;
            }
            rigidbody.velocity = transform.forward * projectileSpeed;

            var collider = projectile.GetComponent<Collider>();
            var myCollier = this.GetComponent<Collider>();
            //Bỏ qua va chạm vật lý
            if(collider != null && myCollier != null){
                Physics.IgnoreCollision(collider, myCollier);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}

