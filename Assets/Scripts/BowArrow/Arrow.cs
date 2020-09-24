using Crate;
using PillarOfLight;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BowArrow
{
    public class Arrow : MonoBehaviour
    {
        public float m_Speed = 2000.0f;
        public Transform m_Tip = null;
        public CapsuleCollider TipCollider;
        public LayerMask layerToIgnore;
        public bool enableTrailRenderer;
        public PaintBomb paintBomb;
        public GlueBomb glueBomb;
        
        [HideInInspector] public bool arrowFired = false;
    
        private TrailRenderer _trailRenderer;
        private Rigidbody _mRigidbody = null;
        private bool _mIsStopped = true;
        private Vector3 _mLastPosition = Vector3.zero;
        private FMODUnity.StudioEventEmitter _eventEmitterRef;

        private void Awake()
        {
            _mRigidbody = GetComponent<Rigidbody>();
            TipCollider = m_Tip.GetComponent<CapsuleCollider>();
            _eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
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
            if (Physics.Linecast(_mLastPosition, m_Tip.position, out hit, layerToIgnore))
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
        
            //Turn collider on for color matching
            TipCollider.enabled = true;
            
            //check if damageable
            CheckForDamage(hitObject);

            if (SceneManager.GetActiveScene().name == "WaveBasedAltLayout" && paintBomb.BombArrowReady()) paintBomb.Explode();
            if (SceneManager.GetActiveScene().name == "WaveBasedAltLayout" && glueBomb.GlueBombArrowReady()) glueBomb.Explode();
        
            //Destroy Arrow
            Destroy(gameObject, 2f);
        }

        //called by bow script when released 
        public void Fire(float pullValue)
        {
            // set fired to true so colour wont change
            arrowFired = true;
            
            //Play arrow sound
            _eventEmitterRef.Play();
        
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
            if(enableTrailRenderer) _trailRenderer.enabled = true;
            
            //after 10 seconds, remove arrow (scene management, overloading scenes)
            Destroy(gameObject, 10.0f);
        }

        private void CheckForDamage(GameObject hitObject)
        {
            MonoBehaviour[] behaviours = new[] {hitObject.GetComponent<MonoBehaviour>()};

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour is IDamageable)
                {
                    IDamageable damageable = (IDamageable)behaviour;
                    damageable.Damage(10);

                    break;
                }
            }
        }
    }
}