using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Scripting
{
    public class SimpleSingleton : MonoBehaviour
    {
        public static SimpleSingleton instance;
        void Awake()
        {
            instance = this;
        }
        public void DoSomething()
        {
            Debug.Log("Call Singleton");
        }
    }

}
