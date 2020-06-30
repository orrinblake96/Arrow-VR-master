using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
//    public Animator fadeToNextLevelAnimator;
//    private static readonly int FadeToBlack = Animator.StringToHash("FadeToBlack");
    
    public void StartSelectedGameMode(string levelName)
    {
        print("changing scenes ********************************");
//        fadeToNextLevelAnimator.SetBool(FadeToBlack, true);
        StartCoroutine(LoadNextLevel(levelName));
    }

    private IEnumerator LoadNextLevel(string levelName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(levelName);
    }
}
