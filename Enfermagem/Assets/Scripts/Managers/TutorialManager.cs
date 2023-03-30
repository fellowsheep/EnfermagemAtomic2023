using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Tutorial _tutorial = default;

    public void StartTutorial()
    {
        StartCoroutine(StartTutorialCoroutine());
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene("Shop");
    }

    private IEnumerator StartTutorialCoroutine()
    {
        yield return new WaitForSeconds(2f);

        _tutorial.ChangeAnimation();
    }
}
