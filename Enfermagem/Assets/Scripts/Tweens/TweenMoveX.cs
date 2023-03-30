using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMoveX : TweenBase
{
    public override void TweenToInitialAmount() //Vector3 tweenOffset
    {
        float rectWidth = _rectTransform.rect.width * Mathf.Sign(_initialAmount.x);
        float rectPositionX = _rectTransform.anchoredPosition.x;

        LeanTween.moveX(_rectTransform, rectPositionX + rectWidth + _initialAmount.x, _tweenDuration).setEase(_tweenType);
    }

    public override void TweenToFinalAmount() //Vector3 tweenOffset
    {
        float rectWidth = _rectTransform.rect.width * Mathf.Sign(_finalAmount.x);
        float rectPositionX = _rectTransform.anchoredPosition.x;

        LeanTween.moveX(_rectTransform, rectPositionX + rectWidth + _finalAmount.x, _tweenDuration).setEase(_tweenType);
    }
}

