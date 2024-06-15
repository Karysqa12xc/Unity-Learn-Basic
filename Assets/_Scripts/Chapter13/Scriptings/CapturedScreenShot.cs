using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Chapter.FilesNetworking
{
    public class CapturedScreenShot : MonoBehaviour
    {


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Captured");
                ScreenCapture.CaptureScreenshot("MyScreenshotDoubleSize.png", 2);
                StartCoroutine(CaptureScreenshotAtEndOfFrame());
            }

        }
        IEnumerator CaptureScreenshotAtEndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            Texture2D capturedTexture = ScreenCapture.CaptureScreenshotAsTexture();
        }

    }
}

