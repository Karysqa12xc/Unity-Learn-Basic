using System.Collections;
using System.Collections.Generic;
using Chapter.UserInterface;
using UnityEngine;

public class TrackedObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       IndicatorManager.manager.AddTrackingIndicator(this);
    }
    private void OnDestroy() {
        IndicatorManager.manager.RemoveTrackingIndicator(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
