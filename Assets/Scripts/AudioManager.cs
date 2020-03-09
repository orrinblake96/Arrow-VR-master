using UnityEngine;
using System;
using UnityEngine.Audio;

namespace _BowAndArrow.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        // Start is called before the first frame update
        void Awake()
        {
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }

        public void Play(string soundName)
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + soundName + " not found!!");
                return;
            }
            
            s.source.Play();
        }
    }
}
