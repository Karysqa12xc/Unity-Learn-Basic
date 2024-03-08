using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Chapter.InputBase
{
    public class CubePlayer : MonoBehaviour
    {
        [SerializeField]
        float moveSpeed = 5;
        private Vector2 moveInput = Vector2.zero;
        // Update is called once per frame
        void Update()
        {
            var movement = new Vector3(moveInput.x, moveInput.y, 0)
                            * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
            
        }
        public void OnChangeColour(InputAction.CallbackContext context){
            if(context.performed){
                ChangerColour();
            }
        }
        public void ChangerColour(){
            var color = Color.HSVToRGB(Random.value, 0.8f, 1f);
            GetComponent<Renderer>().material.color = color;
        }
        public void OnMove(InputAction.CallbackContext context){
            moveInput = context.ReadValue<Vector2>();
        }
    }
}

