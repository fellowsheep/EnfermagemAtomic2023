using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : TweenBase
{
    public float GetTweenDuration()
    {
        return _tweenDuration;
    }

    public override void TweenToInitialAmount()
    {
        LeanTween.scale(_rectTransform, _initialAmount, _tweenDuration).setEase(_tweenType);
    }

    public override void TweenToFinalAmount()
    {
        LeanTween.scale(_rectTransform, _finalAmount, _tweenDuration).setEase(_tweenType);
    }
}
