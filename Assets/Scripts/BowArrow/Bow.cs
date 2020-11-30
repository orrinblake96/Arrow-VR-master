using System;
using UnityEngine;
using System.Threading.Tasks;

namespace BowArrow
{
    public class Bow : MonoBehaviour
    {
        public GameObject m_ArrowPrefab = null;

        public float m_GrabThreshold = 0.15f;
        public Transform m_Start = null;
        public Transform m_End = null;
        public Transform m_Socket = null;
        public OculusInput oculusInput;

        private Transform m_PullingHand = null;
        private Arrow m_CurrentArrow = null;
        private Animator m_Animator = null;

        private float m_PullValue = 0.0f;
        private float _vibrateAmount = 0.0f;
        private static readonly int Blend = Animator.StringToHash("Blend");

        private float _slowedTimeWait;

        private void Awake() 
        {
            m_Animator = GetComponent<Animator>();
        }

        private async void Start()
        {
            await CreateArrow(0.0f);
        }

        private void Update()
        {
            if (!m_PullingHand || !m_CurrentArrow) return;

            m_PullValue = CalculatePull(m_PullingHand);
            _vibrateAmount = m_PullValue;
            
            m_PullValue = Mathf.Clamp(m_PullValue, 0.0f, 1.0f);
            _vibrateAmount = Mathf.Clamp(_vibrateAmount, 0f, 0.5f);
            
            m_Animator.SetFloat(Blend, m_PullValue);
            OVRInput.SetControllerVibration(_vibrateAmount, _vibrateAmount, oculusInput.m_Controller[oculusInput.dominantHandIndex]);
        }

        private float CalculatePull(Transform pullHand)
        {
            // Get pull direction by returning vector length
            Vector3 direction = m_End.position - m_Start.position;
            float magnitude = direction.magnitude;
            
            // Get pull length and return
            direction.Normalize();
            Vector3 difference = pullHand.position - m_Start.position;

            return Vector3.Dot(difference, direction) / magnitude;
        }

        private async Task CreateArrow(float waitTime)
        {
            // Wait set time for new arrow to spawn
            await Task.Delay(TimeSpan.FromSeconds(waitTime));
     
            //Create arrow at socket location
            GameObject arrowObject = Instantiate(m_ArrowPrefab, m_Socket);

            //orient arrow to sit perfectly in the notch
            arrowObject.transform.localPosition = new Vector3(0, 0, 0.4235f);
            arrowObject.transform.localEulerAngles = Vector3.zero;

            //set current arrow to newly instantiated arrow
            m_CurrentArrow = arrowObject.GetComponent<Arrow>();
        }

        public void Pull (Transform hand)
        {
            float distance = Vector3.Distance(hand.position, m_Start.position);

            if (distance > m_GrabThreshold) return;

            m_PullingHand = hand;
        }
 
        /// <summary>
        /// Handles the pre-fire of the arrow. If the arrow is below the pull-value then it wont fire.
        /// This then resets the pulling-hand to Null, pull-value to 0, bow-sting animation and creates a new arrow.
        /// </summary>
        public async void Release ()
        {
            if (m_PullValue > 0.3f) FireArrow();

            m_PullingHand = null;
            m_PullValue = 0.0f;
            m_Animator.SetFloat("Blend", 0);

            _slowedTimeWait = Time.timeScale < 1f ? 0f : .25f;

            if (!m_CurrentArrow) await CreateArrow(_slowedTimeWait);
        }

        private void FireArrow()
        {
            OVRInput.SetControllerVibration(0, 0, oculusInput.m_Controller[oculusInput.dominantHandIndex]);
            m_CurrentArrow.Fire(m_PullValue);
            m_CurrentArrow = null;
        }
    }
}