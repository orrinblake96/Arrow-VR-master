using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator fadeToNextLevelAnimator;
    private static readonly int FadeToBlack = Animator.StringToHash("FadeToBlack");

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ArrowTip"))
        {
            StartCoroutine(StartSelectedGameMode());
        }
    }

    private IEnumerator StartSelectedGameMode()
    {
        print("Fading");
        fadeToNextLevelAnimator.SetBool(FadeToBlack, true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene( (SceneManager.GetActiveScene().buildIndex + 1 ) % 2);
    }
}
