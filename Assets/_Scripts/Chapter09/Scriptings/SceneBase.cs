
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Chapter.LogicAndGameplay
{
    public class SceneBase : MonoBehaviour
    {
        int index = 0;
        void Update()
        {
            if(index < 1)
                UnloadLevel();
            index++;
        }
        // Hàm load scene cơ bản
        void LoadSceneBasic()
        {
            SceneManager.LoadScene("LoadScene");
        }
        // Đối với những cảnh có dung 
        // lượng lớn ta cần tải ngầm thì dùng cách bất
        // đồng bộ này để tải
        void LoadLevelAsync()
        {
            var operation = SceneManager.LoadSceneAsync("LoadScene");

            Debug.Log("begin load...");

            operation.allowSceneActivation = false;

            StartCoroutine(WaitForLoading(operation));

        }
        IEnumerator WaitForLoading(AsyncOperation operation)
        {
            while (operation.progress < 0.9f)
            {
                yield return null;
            }
            Debug.Log("Done load");
            operation.allowSceneActivation = true;
        }
        // Có những trường hợp phải Scene này sử dụng
        // dữ liệu từ Scene khác nên ta có cách nạp 
        // chồng scene vào chung một cảnh
        void LoadLevelAdditive()
        {
            while (index < 1)
            {
                if (index == 1)
                {
                    break;
                }
                SceneManager.LoadScene("LoadScene", LoadSceneMode.Additive);
                index++;
            }
        }
        // Dỡ bỏ một scene đã được nạp chồng vào cảnh
        void UnloadLevel(){
            var unloadOperation = SceneManager.UnloadSceneAsync("LoadScene");
            StartCoroutine(WaitForUnloading(unloadOperation));
        }

        IEnumerator WaitForUnloading(AsyncOperation operation){
            yield return new WaitUntil(() => operation.isDone);
            Resources.UnloadUnusedAssets();
        }

    }
}

