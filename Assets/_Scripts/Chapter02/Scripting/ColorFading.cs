using System.Collections;
using System.Collections.Generic;
using Chapter.Scripting;
using UnityEngine;
namespace Chapter.Scripting
{
    public class ColorFading : MonoBehaviour
    {
        MeshRenderer meshRenderer;
        IEnumerator Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            Debug.Log("Hello...");
            yield return new WaitForSeconds(1);
            Debug.Log("...World");
            StartCoroutine(CoolDownTime(6));
            // StartCoroutine(LogEveryFiveSeconds());
            StartCoroutine(RunEveryHundredTimes());
        }
        // Update is called once per frame
        void Update()
        {
            if (meshRenderer == null)
            {
                return;
            }
            var sineTime = Mathf.Sin(Time.time) + 1 / 2f;
            var color = new Color(sineTime, 0.5f, 0.5f);
            meshRenderer.material.color = color;
        }
        IEnumerator CoolDownTime(int times)
        {
            for (int i = times; i > 0; i--)
            {
                Debug.LogFormat("Times {0}", i);
                yield return new WaitForSeconds(1);
            }
            Debug.Log("Done counting");
        }
        IEnumerator LogEveryFiveSeconds()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                Debug.Log("Hi");
            }
        }
        IEnumerator RunEveryHundredTimes()
        {
            while (true)
            {
                yield return null;
                if (Time.frameCount % 100 == 0)
                {
                    Debug.LogFormat("Frame {0}", Time.frameCount);
                }
                if (Time.frameCount == 354)
                {
                    yield break;
                }
            }


        }
    }
}

