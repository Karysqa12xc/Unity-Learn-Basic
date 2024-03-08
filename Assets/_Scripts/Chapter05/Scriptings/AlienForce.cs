using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.TwoDGraphic
{
    public class AlienForce : MonoBehaviour
    {
        [SerializeField] float verticalForce = 1000;
        [SerializeField] float sidewaysForce = 1000;

        Rigidbody2D body;
        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody2D>();
        }
        
        void FixedUpdate()
        {
            var vertical = Input.GetAxis("Vertical") * verticalForce;
            var horizontal = Input.GetAxis("Horizontal") * sidewaysForce;
            var force = new Vector2(horizontal, vertical) * Time.fixedDeltaTime;
            body.AddForce(force);
        }
    }
}

