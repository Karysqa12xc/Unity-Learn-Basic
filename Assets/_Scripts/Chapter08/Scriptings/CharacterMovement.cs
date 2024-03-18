using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.AnimationAndMovement
{
    public class CharacterMovement : MonoBehaviour
    {
        Animator animator;
        [SerializeField] float speed = 1f;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            animator.SetFloat("Speed", Input.GetAxis("Vertical") * speed);
        }
    }

}
