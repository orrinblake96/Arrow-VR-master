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
          private bool _timeSlowed = false;
          private AudioSource _audioSource;
          private SpecialAbilitiesBar _specialAbilitiesBar;
          private float _powerValue;

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
               if (OVRInput.GetDown(OVRInput.Button.Four) && !_slowingTime && _specialAbilitiesBar.IsPowerCharged() && Time.timeScale >= 1.0f)
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
               // safe power value to be used later
               // reset so bar is empty
               _powerValue = _specialAbilitiesBar.CurrentPower();
               _specialAbilitiesBar.ResetPower();
               
               _slowingTime = true;
               FindObjectOfType<AudioManager>().Play("SlowTime");
               
               Time.timeScale = slowTimeFactor;
          }

          private void ResumeTime()
          {
               if (_slowingTime)
               {
                    _slowingTime = false;
                    
                    StartCoroutine(ResumeTimeRoutine());
               }
          }

          IEnumerator ResumeTimeRoutine()
          {
               print("Slowdown Time length: " + _powerValue + "<-------------"); // Debug
               
               // Wait for time (power value: 0.5 - 10)
               yield return new WaitForSeconds(_powerValue * 10f);
               
               FindObjectOfType<AudioManager>().Play("ResumeTime");

               Time.timeScale = 1f;
          }
     }
}
