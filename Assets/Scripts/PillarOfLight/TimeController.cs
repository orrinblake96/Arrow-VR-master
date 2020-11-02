using System;
using FMODUnity;
using PillarOfLight;
using UnityEngine;
using System.Threading.Tasks;

namespace WaveBasedLevel
{
     public class TimeController : MonoBehaviour
     {

          private bool _slowingTime = false;
          private bool _resumeTime = false;
          private StudioEventEmitter _slowMasterMix;
          private float _powerValue;
          private PowerUpManager _powerUpManager;

          public float slowTimeFactor = 0.25f;
          public string slowTimeAudio;

          private void Start()
          {
               _powerUpManager = GameObject.Find("PowerUpsManagers").GetComponent<PowerUpManager>();
               _slowMasterMix = GetComponent<StudioEventEmitter>();
          }

          private void Update()
          {
               if (OVRInput.GetDown(OVRInput.Button.Four) && !_slowingTime &&  _powerUpManager.slowTimeAcquired  && Time.timeScale >= 1.0f)
               {
                    _powerUpManager.slowTimeAcquired = false;
                    SlowTime();
               }

               if (_resumeTime)
               {
                    // Bring timescale back to normal (1)
                    Time.timeScale += (1f / 2f) * Time.unscaledDeltaTime;
                    Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
                    _slowMasterMix.SetParameter("Slow-Time", Time.timeScale);

                    if (Time.timeScale == 1f)
                    {
                         _slowMasterMix.Stop();
                         _resumeTime = false;
                    }
               }
          }

          private void SlowTime()
          {
               _slowingTime = true;
               FMODUnity.RuntimeManager.PlayOneShot(slowTimeAudio, transform.position);
               _slowMasterMix.Play();
               Time.timeScale = slowTimeFactor;
               _slowMasterMix.SetParameter("Slow-Time", slowTimeFactor);

               ResumeTime();
          }

          // Testing Async/Await functionality
          private async void ResumeTime()
          {
               if (_slowingTime)
               {
                    _slowingTime = false;
//                    StartCoroutine(ResumeTimeRoutine());
                    await ResumeTimeRoutine();
               }
          }
          
          // Testing Async/Await functionality
          private async Task ResumeTimeRoutine()
          {
               
               // Wait for set time
               // Use RealTime as normal was causing increased wait times
               await Task.Delay(TimeSpan.FromSeconds(7));
               
               _resumeTime = true;
          }
     }
}
