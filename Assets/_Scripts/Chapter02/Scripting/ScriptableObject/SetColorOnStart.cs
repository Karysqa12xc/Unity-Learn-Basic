using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Scripting.ScriptableObjectBase
{
    public class SetColorOnStart : MonoBehaviour
    {
        [SerializeField] ObjectColour objectColour;
        // Update is called once per frame
        void Update()
        {
            if (objectColour == null)
            {
                return;
            }
            GetComponent<Renderer>().material.color = objectColour.color;
        }
    }
}

