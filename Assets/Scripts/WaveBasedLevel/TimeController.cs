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
          private bool _resumeTime = false;
          private SpecialAbilitiesBar _specialAbilitiesBar;
          private float _powerValue;

          public float slowTimeFactor = 0.25f;
          public AudioClip slowTimeClip;
          public AudioClip speedUpTimeClip;

          private void Start()
          {
               _specialAbilitiesBar = GameObject.Find("AbilitiesSlider").GetComponent<SpecialAbilitiesBar>();
          }

          private void Update()
          {
               if (OVRInput.GetDown(OVRInput.Button.Four) && !_slowingTime && _specialAbilitiesBar.IsPowerCharged() && Time.timeScale >= 1.0f)
               {
                    SlowTime();
               }

               if (_resumeTime)
               {
                    // Bring timescale back to normal (1)
                    Time.timeScale += (1f / 2f) * Time.unscaledDeltaTime;
                    Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

                    if (Time.timeScale == 1f) _resumeTime = false;
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
               
               ResumeTime();
          }

          private void ResumeTime()
          {
               if (_slowingTime)
               {
                    _slowingTime = false;
                    StartCoroutine(ResumeTimeRoutine());
               }
          }

          private IEnumerator ResumeTimeRoutine()
          {
               
               // Wait for time (power value: 0.5 - 10)
               // Use RealTime as normal was causing increased wait times
               yield return new WaitForSecondsRealtime(_powerValue);
               
               FindObjectOfType<AudioManager>().Play("ResumeTime");

               _resumeTime = true;
          }
     }
}
