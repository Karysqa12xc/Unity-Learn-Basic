using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Scripting.ObjectPool
{
    public interface IObjectPoolNotifier
    {
        //Called when the object is being returned to the pool
        void OnEnqueuedToPool();

        //Called when the object is leaving the pool, or has 
        // just been created. If "created" is true, the object
        // has just been created, and is not being recycled

        //(Doing it this way means you use a single method to do the
        // object's setup, for your first time and all subsequent times.)

        void OnCreatedOfDequeuedFromPool(bool created);
    }
}

