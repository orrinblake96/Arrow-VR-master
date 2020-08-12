using System;
using System.Collections;
using UnityEngine;

namespace WaveBasedLevel
{
     public class TimeController : MonoBehaviour
     {

          private bool _slowingTime = false;
          private AudioSource _audioSource;

          public float slowTimeFactor = 0.5f;
          public AudioClip slowTimeClip;
          public AudioClip speedUpTimeClip;

          private void Start()
          {
               _audioSource = GetComponent<AudioSource>();
          }

          private void Update()
          {
               if (OVRInput.GetDown(OVRInput.Button.Four))
               {
                    SlowTime();
               }
               else
               {
                    ResumeTime();
               }
          }

          private void SlowTime()
          {
               _audioSource.clip = slowTimeClip;
               _audioSource.Play();
               
               Time.timeScale = slowTimeFactor;

               _slowingTime = true;
          }

          private void ResumeTime()
          {
               if (_slowingTime)
               {
                    _audioSource.Stop();
                    StartCoroutine(ResumeTimeRoutine());
               }
          }

          IEnumerator ResumeTimeRoutine()
          {
               _audioSource.clip = speedUpTimeClip;
               _audioSource.Play();
               
               yield return new WaitForSeconds(1f);

               Time.timeScale = 1f;

               _slowingTime = false;
          }
     }
}
