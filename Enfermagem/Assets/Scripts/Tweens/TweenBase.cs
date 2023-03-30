using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenBase : MonoBehaviour
{
    [SerializeField] protected LeanTweenType _tweenType = LeanTweenType.linear;
    [SerializeField] protected float _tweenDuration = default;
    [SerializeField] protected Vector3 _initialAmount = default;
    [SerializeField] protected Vector3 _finalAmount = default;

    protected RectTransform _rectTransform;

    public void OnEnable()
    {
        if(TryGetComponent(out RectTransform rectTransform))
        {
            _rectTransform = rectTransform;
        }
    }

    public virtual void TweenToInitialAmount()
    {
        
    }

    public virtual void TweenToFinalAmount()
    {
        
    }

    /*if(TryGetComponent(out RectTransform rectTransform))
    {
        LeanTween.moveY(rectTransform, finalYposition, _tweenDuration).setEase(_tweenType);
    }*/
}
