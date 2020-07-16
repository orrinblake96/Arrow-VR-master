using System.Collections;
using UnityEngine;

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

        private void Awake() 
        {
            m_Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            StartCoroutine(CreateArrow(0.0f));
        }

        private void Update()
        {
            if (!m_PullingHand || !m_CurrentArrow) return;

            m_PullValue = CalculatePull(m_PullingHand);
            _vibrateAmount = m_PullValue;
            
            m_PullValue = Mathf.Clamp(m_PullValue, 0.0f, 1.0f);
            _vibrateAmount = Mathf.Clamp(_vibrateAmount, 0.2f, 0.8f);
            
            m_Animator.SetFloat(Blend, m_PullValue);
            OVRInput.SetControllerVibration(_vibrateAmount, _vibrateAmount, oculusInput.m_Controller[oculusInput.dominantHandIndex]);
        }

        private float CalculatePull(Transform pullHand)
        {
            Vector3 direction = m_End.position - m_Start.position;
            float magnitude = direction.magnitude;
     
            direction.Normalize();
            Vector3 differnce = pullHand.position - m_Start.position;

            return Vector3.Dot(differnce, direction) / magnitude;
        }

        private IEnumerator CreateArrow(float waitTime)
        {
            // Wait
            yield return new WaitForSeconds(waitTime);
     
            //Create & child
            GameObject arrowObject = Instantiate(m_ArrowPrefab, m_Socket);

            //orient
            arrowObject.transform.localPosition = new Vector3(0, 0, 0.6274f);
            arrowObject.transform.localEulerAngles = Vector3.zero;

            //set
            m_CurrentArrow = arrowObject.GetComponent<Arrow>();
        }

        public void Pull (Transform hand)
        {
            float distance = Vector3.Distance(hand.position, m_Start.position);

            if (distance > m_GrabThreshold) return;

            m_PullingHand = hand;
        }
 
        public void Release ()
        {
            if (m_PullValue > 0.4f) FireArrow();

            m_PullingHand = null;
            m_PullValue = 0.0f;
            m_Animator.SetFloat("Blend", 0);

            if (!m_CurrentArrow) StartCoroutine(CreateArrow(0.25f));
        }

        private void FireArrow()
        {
            m_CurrentArrow.Fire(m_PullValue);
            m_CurrentArrow = null;
        }
 
    }
}