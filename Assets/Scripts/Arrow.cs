using System;
using _BowAndArrow.Scripts;
using _BowAndArrow.Scripts.Crate;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float m_Speed = 2000.0f;
    public Transform m_Tip = null;
    private TrailRenderer _trailRenderer;

    private Rigidbody _mRigidbody = null;
    private bool _mIsStopped = true;
    private Vector3 _mLastPosition = Vector3.zero;

    private void Awake()
    {
        _mRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _mLastPosition = transform.position;
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        if (_mIsStopped) return;
            
        //Rotate to direction of velocity
        _mRigidbody.MoveRotation(Quaternion.LookRotation(_mRigidbody.velocity, transform.up));
            
        //collision check if hit
        //linecast prevents objs passing through eachother
        RaycastHit hit;
        if (Physics.Linecast(_mLastPosition, m_Tip.position, out hit))
        {
            Stop(hit.collider.gameObject);
        }

        //store current position
        _mLastPosition = m_Tip.position;
    }

    private void Stop(GameObject hitObject)
    {
        //stops arrow
        _mIsStopped = true;
            
        //parent to hitObect
        transform.parent = hitObject.transform;
            
        //Disable Physics
        //can collide/be affected by other objects
        _mRigidbody.isKinematic = true;
        //Doesnt react to gravity, wont fall
        _mRigidbody.useGravity = false;
            
        //check if damageable
        CheckForDamage(hitObject);
    }

    //called by bow script when released 
    public void Fire(float pullValue)
    {
        FindObjectOfType<AudioManager>().Play("BowRelease");
        _mLastPosition = transform.position;
            
        //arrow now in air
        _mIsStopped = false;

        //detach arrow from bow (parent)
        transform.parent = null;
            
        //reset kinematic affect
        _mRigidbody.isKinematic = false;
            
        //add gravity (flying behaviour)
        _mRigidbody.useGravity = true;
            
        //add a force relative to the pull value from Bow script
        _mRigidbody.AddForce(transform.forward * (pullValue * m_Speed));
            
        //Add Trail to see better
        _trailRenderer.enabled = true;
            
        //after 5 seconds, remove arrow (scene management, overloading scenes)
        Destroy(gameObject, 10.0f);
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