using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Chapter.LogicAndGameplay
{
    public class TriggerObjectiveOnClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] ObjectiveTrigger objective = new ObjectiveTrigger();
        public void OnPointerClick(PointerEventData eventData)
        {
            objective.Invoke();

            this.enabled = false;
        }
    }
}

