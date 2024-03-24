using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Chapter.LogicAndGameplay
{
    public class Menu : MonoBehaviour
    {
        public UnityEvent menuDidAppear = new UnityEvent();
        public UnityEvent menuWillDisappear = new UnityEvent();
    }
}

