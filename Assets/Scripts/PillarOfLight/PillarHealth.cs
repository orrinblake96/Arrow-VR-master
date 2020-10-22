using AllLevels.HighScore;
using FMODUnity;
using TargetShooterMiniGame;
using UnityEngine;

namespace PillarOfLight
{
    public class PillarHealth : MonoBehaviour
    {
        
        [SerializeField] private int startingHealth = 100;
        [SerializeField] private Material pillarOfLightMaterial;
        
        [Header("Pillar Effects")]
        [SerializeField] private ParticleSystem pillarExplosionEffect;
        [SerializeField] private StudioEventEmitter pillarExplosionSound;

        [Header("Objects To Show/Hide")] 
        [SerializeField] private GameObject[] objectsToShow;
        [SerializeField] private GameObject[] objectsToHide;
        
        private int _currentHealth;
        private float _pillarColor = 255f;
        private int _hitCount = 0;
        private Material _pillarOfLightMaterialInstance;
        private Collider[] _pillarExplosionOverlapResults = new Collider[20];
        
        private WaveScore _targetScore;
        private ScoreboardEntryData _entryData = new ScoreboardEntryData();
        private Scoreboard _highscoreBoard;
        
        private Animator _gameScoreAnimator;
        private static readonly int GameOver = Animator.StringToHash("GameOver");

        private void Start()
        {
            // Make an instance of the material so its color changes back to white on Quitting
            _pillarOfLightMaterialInstance = Instantiate(pillarOfLightMaterial);
            GetComponent<MeshRenderer>().material = _pillarOfLightMaterialInstance;
            
            _currentHealth = startingHealth;

            _targetScore = GameObject.Find("ScorenumberTMP").GetComponent<WaveScore>();
            _gameScoreAnimator = GameObject.Find("PlayerScoreCanvasUI").GetComponent<Animator>();
            
            
        }

        public void DamageTaken()
        {
            _currentHealth -= 10;

            // Changes pillar color from white to red the more times it takes damage
            _pillarColor -= 25.5f;
            _pillarOfLightMaterialInstance.color = new Color(255f/255f, _pillarColor/255f, _pillarColor/255f, 1f);
            
            // Check for damage taken then explode to destroy enemies
            _hitCount++;
            if (_hitCount == 3)
            {
                _hitCount = 0;
                
                // Position & play explosion effects
                pillarExplosionEffect.Play();
                pillarExplosionSound.Play();
                DestroyEnemies();
            }

            if (!(_currentHealth <= 0)) return;
            EndGame();
            
        }
        private void DestroyEnemies()
        {
            Physics.OverlapSphereNonAlloc(transform.position, 5f, _pillarExplosionOverlapResults);

            foreach (Collider nearObject in _pillarExplosionOverlapResults)
            {
                if (nearObject == null || (nearObject.transform.name != "Monster" && nearObject.transform.name != "LargeMonster")) continue;
                if (nearObject.transform.name == "LargeMonster")
                {
                    nearObject.transform.parent.GetComponent<DestroyLargeEnemy>().Damage(50);
                    continue;
                }
                nearObject.transform.parent.GetComponent<DestroyingEnemies>().Damage(10);
            }
        }

        private void EndGame()
        {
            ObjectsToShow();
            ObjectsToHide();
            _gameScoreAnimator.SetBool(GameOver, true);
            
            HighscoreHandler();
            gameObject.SetActive(false);
        }

        private void ObjectsToShow()
        {
            foreach (GameObject showable in objectsToShow)
            {
                showable.SetActive(true);
            }
        }
        
        private void ObjectsToHide()
        {
            foreach (GameObject hideable in objectsToHide)
            {
                hideable.SetActive(false);
            }
        }

        private void HighscoreHandler()
        {
            _highscoreBoard = GameObject.Find("ScoreBoard").GetComponent<Scoreboard>();
            _entryData.entryName = "Player 1";
            _entryData.entryScore = _targetScore.CurrentScore;
            _highscoreBoard.AddEntry(_entryData);
        }
        
        
        public void ResetPillarHits(int hitCount)
        {
            _hitCount = hitCount;
        }

        // Calculating random power-up drops
        public int CurrentHealth => _currentHealth;
    }
}
