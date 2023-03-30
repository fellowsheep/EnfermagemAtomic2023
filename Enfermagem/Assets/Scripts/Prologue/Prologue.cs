using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    [SerializeField] private Fade _fade = default;
    [SerializeField] private CanvasGroup _canvas = default;
    [SerializeField] private TweenScale[] _scaleTween = default; 
    [SerializeField] private GameManager _gameManager = default; 
    [SerializeField] private float _imageDuration = default; 

    public void StartPrologue()
    {
        StartCoroutine(StartPrologueCoroutine());
    }

    //Could this get better??
    private IEnumerator StartPrologueCoroutine()
    {
        yield return _fade.FadeCoroutine(_canvas, 1f, 2f);

        yield return ShowImagesCoroutine(); 

        _gameManager.StartGame();

        yield return _fade.FadeCoroutine(_canvas, 0, 2f);
    }

    private IEnumerator ShowImagesCoroutine()
    {
        int tweenIndex = 0;

        while(tweenIndex < _scaleTween.Length)
        {
            float prologueImageDuration = _scaleTween[tweenIndex].GetTweenDuration() + _imageDuration;
            _scaleTween[tweenIndex].gameObject.SetActive(true);

            _scaleTween[tweenIndex].TweenToFinalAmount();

            yield return new WaitForSeconds(prologueImageDuration);

            _scaleTween[tweenIndex].TweenToInitialAmount();

            yield return new WaitForSeconds(_scaleTween[tweenIndex].GetTweenDuration());

            tweenIndex++;
        }
    }
}
