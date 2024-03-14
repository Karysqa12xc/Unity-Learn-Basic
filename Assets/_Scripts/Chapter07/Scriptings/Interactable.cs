using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.PhysicsAndCharacterCtrl
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        public void Interact(GameObject fromObject)
        {
            Debug.LogFormat("I've been interacted with by {0}!", fromObject.name);
        }
    }

}
