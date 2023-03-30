using UnityEngine;
using UnityEngine.UI;

public class InteractableScroll : Interactable
{
    [SerializeField] private float _maxScroll = default;
    [SerializeField] private ScrollRect _scrollRect = default;

    private static float _scrollPercentage = 1;

    public override void Interact()
    {
        if(_maxScroll == 1)
        {
            _scrollPercentage += Time.deltaTime;

            if(_scrollPercentage >= 1)
            {
                _scrollPercentage = 1;
            }
        }
        else
        {
            _scrollPercentage -= Time.deltaTime;

            if(_scrollPercentage <= 0)
            {
                _scrollPercentage = 0;
            }
        }

        _scrollRect.verticalNormalizedPosition = Mathf.Lerp(0, 1, _scrollPercentage);
    }
}
