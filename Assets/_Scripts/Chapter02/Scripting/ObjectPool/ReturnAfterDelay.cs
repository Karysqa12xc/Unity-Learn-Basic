using System;
using System.Collections;
using System.Collections.Generic;
using Chapter.Scripting.ObjectPool;
using UnityEngine;

public class RunAfterDelay : MonoBehaviour, IObjectPoolNotifier
{
    public void OnCreatedOfDequeuedFromPool(bool created)
    {
        Debug.Log("Dequeued from object pool!");

        StartCoroutine(DoReturnAfterDelay());
    }

    public void OnEnqueuedToPool()
    {
        Debug.Log("Enqueued to object pool!");
    }

    IEnumerator DoReturnAfterDelay()
    {
        yield return new WaitForSeconds(1.0f);

        gameObject.ReturnToPool();
    }
}

