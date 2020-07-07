using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator fadeToNextLevelAnimator;
    private static readonly int FadeToBlack = Animator.StringToHash("FadeToBlack");

    public void StartSelectedGameMode(string levelName)
    {
        print("******************************** SCENE CHNAGE ********************************");
        if(fadeToNextLevelAnimator) fadeToNextLevelAnimator.SetBool(FadeToBlack, true);
        StartCoroutine(LoadNextLevel(levelName));
    }

    private IEnumerator LoadNextLevel(string levelName)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(levelName);
    }
}
