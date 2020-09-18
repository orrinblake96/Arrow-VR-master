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
//          private SpecialAbilitiesBar _specialAbilitiesBar; // Remove
          private float _powerValue;
          private PowerUpManager _powerUpManager;

          public float slowTimeFactor = 0.25f;
          public string slowTimeAudio;
          public string regainTimeAudio;

          private void Start()
          {
//               _specialAbilitiesBar = GameObject.Find("AbilitiesSlider").GetComponent<SpecialAbilitiesBar>(); // Remove
               _powerUpManager = GameObject.Find("PowerUpsManagers").GetComponent<PowerUpManager>();
          }

          private void Update()
          {
               if (OVRInput.GetDown(OVRInput.Button.Four) && !_slowingTime && _powerUpManager.slowTimeAcquired && Time.timeScale >= 1.0f)
               {
                    _powerUpManager.slowTimeAcquired = false;
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
//               _powerValue = _specialAbilitiesBar.CurrentPower(); // Remove
//               _specialAbilitiesBar.ResetPower(); // Remove
               
               _slowingTime = true;
               FMODUnity.RuntimeManager.PlayOneShot(slowTimeAudio, transform.position);
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
               yield return new WaitForSecondsRealtime(7);
               
               FMODUnity.RuntimeManager.PlayOneShot(regainTimeAudio, transform.position);
               
               _resumeTime = true;
          }
     }
}
