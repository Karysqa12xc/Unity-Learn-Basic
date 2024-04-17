using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
namespace Chapter.SoundAndMusic
{
    public class ControlSound : MonoBehaviour
    {
        public AudioSource source;
        public AudioClip clip;
        // Start is called before the first frame update
        void Start()
        {
            // source = GetComponent<AudioSource>();
            AudioManager.instance.PlayEffect("Bell");
        }

        // Update is called once per frame
        void Update()
        {
            source.Play();
            source.PlayDelayed(0.5f);
            source.PlayOneShot(clip, 0.5f);
            source.PlayOneShot(clip, 1f);
            source.Stop();
        }
    }

}
