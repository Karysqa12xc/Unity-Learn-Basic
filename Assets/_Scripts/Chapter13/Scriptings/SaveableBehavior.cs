using System.Collections;
using System.Collections.Generic;
using LitJson;
using Unity.Mathematics;
using UnityEngine;
namespace Chapter.FilesNetworking
{
    public abstract class SaveableBehavior :
    MonoBehaviour,
    ISaveable,
    ISerializationCallbackReceiver
    {

        public abstract JsonData SaveData{ get; }
        public abstract void LoadFromData(JsonData data);

        public string SaveID
        {
            get
            {
                return _saveId;
            }
            set{
                _saveId = value;
            }
        }
        [HideInInspector]
        [SerializeField]
        private string _saveId; 
        public void OnAfterDeserialize()
        {
                
        }

        public void OnBeforeSerialize()
        {
            if(_saveId == null){
                _saveId = System.Guid.NewGuid().ToString();
            }
        }
    }
}

