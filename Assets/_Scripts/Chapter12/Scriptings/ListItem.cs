using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Chapter.UserInterface
{
    public class ListItem : MonoBehaviour
    {
        [SerializeField] Text labelText;
        public string Label
        {
            get
            {
                return labelText.text;
            }
            set
            {
                labelText.text = value;
            }
        }


    }
}

