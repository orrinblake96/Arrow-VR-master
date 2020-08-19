using System;
using Audio;
using System.Collections;
using PillarOfLight;
using UnityEngine;

namespace WaveBasedLevel
{
     public class TimeController : MonoBehaviour
     {

          private bool _slowingTime = false;
          private AudioSource _audioSource;
          private SpecialAbilitiesBar _specialAbilitiesBar;

          public float slowTimeFactor = 0.25f;
          public AudioClip slowTimeClip;
          public AudioClip speedUpTimeClip;

          private void Start()
          {
               _audioSource = GetComponent<AudioSource>();
               _specialAbilitiesBar = GameObject.Find("AbilitiesSlider").GetComponent<SpecialAbilitiesBar>();
          }

          private void Update()
          {
               if (OVRInput.GetDown(OVRInput.Button.Four) && !_slowingTime && _specialAbilitiesBar.IsPowerCharged())
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
               _specialAbilitiesBar.ResetPower();
               _slowingTime = true;
               
               _audioSource.clip = slowTimeClip;
               _audioSource.Play();
               
               Time.timeScale = slowTimeFactor;
          }

          private void ResumeTime()
          {
               if (_slowingTime)
               {
                    StartCoroutine(ResumeTimeRoutine());
               }
          }

          IEnumerator ResumeTimeRoutine()
          {
               yield return new WaitForSeconds(_specialAbilitiesBar.CurrentPower() * 10f);
               
               _audioSource.Stop();
               _audioSource.clip = speedUpTimeClip;
               _audioSource.Play();
               
               yield return new WaitForSeconds(1f);

               Time.timeScale = 1f;

               _slowingTime = false;
          }
     }
}
