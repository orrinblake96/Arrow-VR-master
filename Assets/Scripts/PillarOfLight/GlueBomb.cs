using System.Collections;
using Crate;
using Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace PillarOfLight
{
    public class GlueBomb : MonoBehaviour, IDamageable
    {
        public ParticleSystem explosionEffect;
        
        public float blastRadius = 10f;
        public RespawnGlueBomb respawnGlueBomb;

        private bool _glueBombArrowReady = false;
        private Collider[] _glueBombOverlapResults = new Collider[20];

        public bool GlueBombArrowReady()
        {
            return _glueBombArrowReady;
        }

        public void Damage(int amount)
        {
            Physics.OverlapSphereNonAlloc(transform.position, blastRadius, _glueBombOverlapResults);

            foreach (Collider nearObject in _glueBombOverlapResults)
            {
                if (nearObject == null) continue;
                if (nearObject.transform.name != "Monster" && nearObject.transform.name != "LargeMonster") continue;
                nearObject.transform.parent.GetComponent<EnemyBehaviour>().GlueBomb();
            }
            
            explosionEffect.Play();
            gameObject.SetActive(false);
            
            respawnGlueBomb.RespawnGlue(gameObject);
        }
    }
}
