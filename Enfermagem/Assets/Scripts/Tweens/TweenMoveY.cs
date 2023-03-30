using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMoveY : TweenBase
{
    public override void TweenToInitialAmount()
    {
        float rectHeight = _rectTransform.rect.height * Mathf.Sign(_initialAmount.y);
        float rectPositionY = _rectTransform.anchoredPosition.y;

        LeanTween.moveY(_rectTransform, rectPositionY + rectHeight + _initialAmount.y, _tweenDuration).setEase(_tweenType);
    }

    public override void TweenToFinalAmount()
    {
        float rectHeight = _rectTransform.rect.height * Mathf.Sign(_finalAmount.y);
        float rectPositionY = _rectTransform.anchoredPosition.y;

        LeanTween.moveY(_rectTransform, rectPositionY + rectHeight + _finalAmount.y, _tweenDuration).setEase(_tweenType);
    }
}
