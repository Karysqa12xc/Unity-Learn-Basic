using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.UserInterface
{
    public class List : MonoBehaviour
    {
        [SerializeField] int itemCount = 5;
        [SerializeField] ListItem itemPrefabs;
        [SerializeField] RectTransform itemContainer;
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < itemCount; i++)
            {
                var label = string.Format("Item {0}", i);
                CreateNewListItem(label);
            }
        }

        public void CreateNewListItem(string label)
        {
            var newItem = Instantiate(itemPrefabs);
            newItem.transform.SetParent(itemContainer, worldPositionStays: false);
            newItem.Label = label;
        } 
    }

}
