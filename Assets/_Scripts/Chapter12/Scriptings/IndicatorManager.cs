using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
namespace Chapter.UserInterface
{
    public class IndicatorManager : MonoBehaviour
    {
        [SerializeField] RectTransform indicatorPrefab = null;
        [SerializeField] RectTransform indicatorContainer = null;
        public static IndicatorManager manager;
        Dictionary<TrackedObject, RectTransform> indicators = new Dictionary<TrackedObject, RectTransform>();
        private void Awake()
        {
            manager = this;
        }
        void LateUpdate()
        {
            foreach(var pair in indicators){
                TrackedObject target = pair.Key;
                RectTransform indicator = pair.Value;
                if(target == null){
                    continue;
                }
                indicator.anchoredPosition = GetCanvasPositionForTarget(target);
            }
        }
        private Vector2 GetCanvasPositionForTarget(TrackedObject target){
            var indicatorPoint = Camera.main.WorldToViewportPoint(target.transform.position);
            indicatorPoint.x = Mathf.Clamp01(indicatorPoint.x);
            indicatorPoint.y = Mathf.Clamp01(indicatorPoint.y);
            if(indicatorPoint.z < 0){
                indicatorPoint.y = 0;
                indicatorPoint.x = 1f - indicatorPoint.x;
            }
            var canvas = indicatorContainer.GetComponent<Canvas>();
            Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
            indicatorPoint.Scale(canvasSize);
            return indicatorPoint;
        }
        public void AddTrackingIndicator(TrackedObject transform)
        {
            if(indicators.ContainsKey(transform)){
                return;
            }
            var indicator = Instantiate(indicatorPrefab);
            indicator.name = string.Format("Indicator for {0}", transform.gameObject.name);
            indicator.SetParent(indicatorContainer, false);
            indicator.pivot = new Vector2(0.5f, 0.5f);
            indicator.anchorMin = Vector2.zero;
            indicator.anchorMax = Vector2.zero;
            indicators[transform] = indicator;
            indicator.anchoredPosition = GetCanvasPositionForTarget(transform);
        }
        public void RemoveTrackingIndicator(TrackedObject transform)
        {
            if(indicators.ContainsKey(transform)){
                if(indicators[transform] != null) {
                    Destroy(indicators[transform].gameObject);
                }
            }
            indicators.Remove(transform);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

