using System;
using _BowAndArrow.Scripts.Crate;
using UnityEngine;

namespace _BowAndArrow.Scripts
{
    public class Arrow : MonoBehaviour
    {
        public float m_Speed = 2000.0f;
        public Transform m_Tip = null;

        private Rigidbody m_Rigidbody = null;
        private bool m_IsStopped = true;
        private Vector3 m_LastPosition = Vector3.zero;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            m_LastPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if (m_IsStopped) return;
            
            //Rotate to direction of velocity
            m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Rigidbody.velocity, transform.up));
            
            //collision check if hit
            //linecast prevents objs passing through eachother
            RaycastHit hit;
            if (Physics.Linecast(m_LastPosition, m_Tip.position, out hit))
            {
                Stop(hit.collider.gameObject);
            }

            //store current position
            m_LastPosition = m_Tip.position;
        }

        private void Stop(GameObject hitObject)
        {
            //stops arrow
            m_IsStopped = true;
            
            //parent to hitObect
            transform.parent = hitObject.transform;
            
            //Disable Physics
            //can collide/be affected by other objects
            m_Rigidbody.isKinematic = true;
            //Doesnt react to gravity, wont fall
            m_Rigidbody.useGravity = false;
            
            //check if damageable
            CheckForDamage(hitObject);
        }

        //called by bow script when released 
        public void Fire(float pullValue)
        {
            FindObjectOfType<AudioManager>().Play("BowRelease");
            m_LastPosition = transform.position;
            
            //arrow now in air
            m_IsStopped = false;

            //detach arrow from bow (parent)
            transform.parent = null;
            
            //reset kinematic affect
            m_Rigidbody.isKinematic = false;
            
            //add gravity (flying behaviour)
            m_Rigidbody.useGravity = true;
            
            //add a force relative to the pull value from Bow script
            m_Rigidbody.AddForce(transform.forward * (pullValue * m_Speed));
            
            //after 5 seconds, remove arrow (scene management, overloading scenes)
            Destroy(gameObject, 5.0f);
        }

        private void CheckForDamage(GameObject hitObject)
        {
            MonoBehaviour[] behaviours = new[] {hitObject.GetComponent<MonoBehaviour>()};

            Debug.Log("Damaged");

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour is IDamageable)
                {
                    IDamageable damageable = (IDamageable)behaviour;
                    damageable.Damage(5);

                    break;
                }
            }
        }
    }
}
