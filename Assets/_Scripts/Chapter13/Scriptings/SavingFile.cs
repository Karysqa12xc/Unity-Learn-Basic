using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.FilesNetworking
{
    public class SavingFile : MonoBehaviour
    {
        public string PathForFilename(string filename)
        {
            var folderToStoreFilesIn = Application.persistentDataPath;
            Debug.Log(folderToStoreFilesIn);
            var path = System.IO.Path.Combine(folderToStoreFilesIn, filename);
            return path;

        }
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(PathForFilename("hello"));
        }


    }
}

