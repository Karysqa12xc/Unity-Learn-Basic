using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
namespace Chapter.FilesNetworking
{
    public class LoadTextureFromDisk : MonoBehaviour
    {
        public Texture2D LoadTexture(string fileName)
        {
            var imagePath = Path.Combine(Application.dataPath, fileName);
            Debug.Log(imagePath);
            if(File.Exists(imagePath) == false){
                Debug.LogWarningFormat("No files exists at path {0}",
                imagePath);
                return null;
            }
            var fileData = File.ReadAllBytes(imagePath);
            var tex = new Texture2D(2,2);
            var success = tex.LoadImage(fileData);
            if(success){
                return tex;
            }
            else{
                Debug.LogWarningFormat("Failed to load texture at path {0}",
                    imagePath);
                return null;
            }
        }
        void Start()
        {
            var tex = LoadTexture("ImageToLoad.jpg");
            if(tex == null){
                return;
            }
            GetComponent<Renderer>().material.mainTexture = tex;
            SavingService.SaveGame("SaveGame.json");
        }
    }
}

