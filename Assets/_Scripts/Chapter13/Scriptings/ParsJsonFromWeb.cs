using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.Networking;
namespace Chapter.FilesNetworking
{
    public class ParsJsonFromWeb : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://httpbin.org/get");
            UnityWebRequestAsyncOperation operation = request.SendWebRequest();
            StartCoroutine(WaitForDownload(operation));
            operation.completed += (op)=>{
                switch(request.result){
                    case UnityWebRequest.Result.Success:
                        Debug.Log("Success");
                        Debug.Log(request.downloadHandler.text);
                        ParseJson(request.downloadHandler.text);
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        Debug.Log($"Connection error: {request.error}");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.Log($"Protocol error: {request.error}");
                        break;
                    default:
                        Debug.Log($"Error: {request.result}");
                        break;
                }
                
            };   
        }
        private void ParseJson(string text)
        {
            JsonData data = JsonMapper.ToObject(text);
            try
            {
                string host = (string)data["headers"]["Host"];
                Debug.Log($"The host is {host}");
            }
            catch (KeyNotFoundException)
            {
                // TODO
                Debug.LogError($"Couldn't find the host in downloaded data!");
            }
        }
        public IEnumerator WaitForDownload(UnityWebRequestAsyncOperation webOperation)
        {
            yield return webOperation;
            var request = webOperation.webRequest;
            if(request.result == UnityWebRequest.Result.Success){
                var data = request.downloadHandler.text;
            }else{

            }
        }
    }
}

