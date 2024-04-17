using System.Collections;
using System.Collections.Generic;
using Chapter.Scripting;
using UnityEngine;
namespace Chapter.SoundAndMusic
{
    public class AudioManager : Singleton<AudioManager>
    {
        public SoundEffect[] effects;
        private Dictionary<string, SoundEffect> _effectDictionary;
        private AudioListener _listener;
        private void Awake() {
            _effectDictionary = new Dictionary<string, SoundEffect>();
            foreach (var effect in effects)
            {
                Debug.LogFormat("registered effect {0}", effect.name);
                _effectDictionary[effect.name] = effect;
            }
        }
        public void PlayEffect(string effectName)
        {
            if(_listener == null){
                _listener = FindAnyObjectByType<AudioListener>();
            }
            PlayEffect(effectName, _listener.transform.position);
        }
        public void PlayEffect(string effectName, Vector3 worldPosition)
        {
            if(_effectDictionary.ContainsKey(effectName) == false){
                Debug.LogWarningFormat("Effect {0} is not registered.", effectName);
                return;
            } 
            var clip = _effectDictionary[effectName].GetRandomClip();
            if(clip == null){
                Debug.LogWarningFormat("Effect {0} has no clips to play,", effectName);
                return;
            }
            AudioSource.PlayClipAtPoint(clip, worldPosition);
        }
    }
}

