using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public IEnumerator FadeCoroutine(CanvasGroup canvas, float targetAlpha, float fadeDuration)
    {
        float startAlpha = canvas.alpha;
        float fadeTime = 0;

        while(fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            
            canvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, fadeTime / fadeDuration);

            yield return null;
        }

        canvas.alpha = targetAlpha; 
    }
}
