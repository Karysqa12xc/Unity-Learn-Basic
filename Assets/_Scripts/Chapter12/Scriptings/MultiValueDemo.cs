using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.UserInterface
{
    public class MultiValueDemo : MonoBehaviour
    {
        [SerializeField]
        MultiValue multiValue = new MultiValue(
         "One",
         "Two",
         "Three"
        );
    }

}
