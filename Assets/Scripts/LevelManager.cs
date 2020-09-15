using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator fadeToNextLevelAnimator;
    private static readonly int FadeToBlack = Animator.StringToHash("FadeToBlack");
    
    // Prevent instantiation on destroyed enemies 1
    private bool _sceneChanging;

    public void StartSelectedGameMode(string levelName)
    {
        if(fadeToNextLevelAnimator) fadeToNextLevelAnimator.SetBool(FadeToBlack, true);
        
        // Prevent instantiation on destroyed enemies 2
        _sceneChanging = true;
        StartCoroutine(LoadNextLevel(levelName));
    }

    private IEnumerator LoadNextLevel(string levelName)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(levelName);
    }
    
    // Prevent instantiation on destroyed enemies 3
    public bool IsSceneChanging()
    {
        return _sceneChanging;
    }
}
