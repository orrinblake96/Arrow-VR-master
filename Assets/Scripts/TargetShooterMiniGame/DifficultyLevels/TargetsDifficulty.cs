using UnityEngine;

namespace TargetShooterMiniGame.DifficultyLevels
{
    [CreateAssetMenu(menuName="Difficulty Stats")]
    public class TargetsDifficulty : ScriptableObject
    {
        public float timeLower;
        public float timeHigher;
    }
}
