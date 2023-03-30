using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrientationController : MonoBehaviour
{
    [SerializeField] private GameObject _checkImage = default;
    [SerializeField] private CanvasGroup _canvasGroup = default;

    private AsyncOperation _loadingOperation;
    private bool _isLoading;
    private float _fadeDuration = 0.7f;

    private void Start()
    {
        if(Input.deviceOrientation == DeviceOrientation.LandscapeLeft && !_isLoading || Input.deviceOrientation == DeviceOrientation.LandscapeRight && !_isLoading)
        {
            _isLoading = true;
            _loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            StartCoroutine(LoadSceneCoroutine());
        }
    }

    private void Update()
    {
        Input.gyro.enabled = true;

        if(Input.deviceOrientation == DeviceOrientation.LandscapeLeft && !_isLoading || Input.deviceOrientation == DeviceOrientation.LandscapeRight && !_isLoading)
        {
            _isLoading = true;
            _loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            StartCoroutine(LoadSceneCoroutine());
        }
        else
        {
            Debug.Log("Orientação errada");
            //_isLoading = false;
            //_checkImage.SetActive(false);
        }
    }

    private IEnumerator LoadSceneCoroutine()
    {
        while(!_loadingOperation.isDone)
        {
            _checkImage.SetActive(true);

            yield return FadeCoroutine(1, _canvasGroup);

            yield return FadeCoroutine(0, _canvasGroup);
        }

        Input.gyro.enabled = true;
    }

    private IEnumerator FadeCoroutine(int alphaTarget, CanvasGroup canvasGroup)
    {
        float startAlpha = canvasGroup.alpha;
        float fadeTime = 0;

        while(fadeTime < _fadeDuration)
        {
            fadeTime += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, alphaTarget, fadeTime / _fadeDuration);

            yield return null;
        }

        canvasGroup.alpha = alphaTarget;
    }
}
