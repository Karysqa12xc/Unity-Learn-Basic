using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.LogicAndGameplay
{
    public class BoxSelectable : MonoBehaviour
    {
        public void Selected()
        {
            Debug.LogFormat("{0} was selected!", gameObject.name);
        }
    }
}

