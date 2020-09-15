using System;
using UnityEngine;

namespace Audio
{
    public class FmodBackgroundMusicManager : MonoBehaviour
    {
        private static FmodBackgroundMusicManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
