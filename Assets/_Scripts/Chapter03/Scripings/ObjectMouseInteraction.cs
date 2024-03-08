using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Chapter.InputBase
{
    public class ObjectMouseInteraction 
    : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerUpHandler,
    IPointerDownHandler,
    IPointerClickHandler
    {
        Material material;
        
        // Start is called before the first frame update
        void Start()
        {
            material = GetComponent<Renderer>().material;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.LogFormat("{0} clicked!", gameObject.name);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.LogFormat("{0} pointer down!", gameObject.name);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.LogFormat("{0} pointer enter!", gameObject.name);
            material.color = Color.yellow;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.LogFormat("{0} pointer exit!", gameObject.name);

            material.color = Color.white;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.LogFormat("{0} pointer up!", gameObject.name);

            material.color = Color.red;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}

