using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Chapter.UserInterface
{
    public class HelpBoxDemo : MonoBehaviour
    {
        [HelpBox(text = "Here is help box above the variables!")]
        [SerializeField] int integer;
    }
}

