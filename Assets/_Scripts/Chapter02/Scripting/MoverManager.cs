using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Scripting
{
    public class MoverManager : Singleton<MoverManager>
    {
        public void ManagerMover()
        {
            Debug.Log("Doing some useful work!");
        }
        public IEnumerator LogAfterDelay()
        {
            Debug.Log("Back in a second");
            yield return new WaitForSeconds(1);
            Debug.Log("I'm back");
        }
    }
}

