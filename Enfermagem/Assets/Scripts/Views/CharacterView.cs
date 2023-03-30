using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Sprite[] _characterSprites = default;
    [SerializeField] private Image _currentImage = default;
    [SerializeField] private CharacterSelector _characterSelector = default;
    [SerializeField] private TweenBase _characterTween = default;

    private void OnEnable()
    {
        _characterSelector.OnCharacterSelected += SetImage;
    }

    private void OnDisable()
    {
        _characterSelector.OnCharacterSelected -= SetImage;
    }

    public IEnumerator ShowCharacterPresentation()
    {
        yield return new WaitForSeconds(1f);
        
        _characterTween.TweenToFinalAmount();

        yield return new WaitForSeconds(16f); 

        _characterTween.TweenToInitialAmount();

        yield return new WaitForSeconds(1.5f);
    }

    private void SetImage(int index)
    {
        _currentImage.sprite = _characterSprites[index];
    }
}
