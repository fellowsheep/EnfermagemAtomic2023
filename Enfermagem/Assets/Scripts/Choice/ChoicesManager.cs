using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoicesManager : MonoBehaviour
{
    public UnityEvent OnWrongChoiceSelected;
    public UnityEvent OnLastChoiceSelected;

    [SerializeField] private GameObject _scoreboardContent = default; 
    [SerializeField] private GameObject _scoreboardChoicePrefab = default;
    [SerializeField] private DEA _dea = default;
    [SerializeField] private SAMU _samu = default;
    [SerializeField] private List<GameObject> _choices = default;
    [SerializeField] private List<GameObject> _conditionalChoices = default;
    [SerializeField] private Image _manImage = default;
    [SerializeField] private Image _popUpImage = default;
    [SerializeField] private TweenBase _popUpTween = default;
    [SerializeField] private Timer _timer = default;

    private List<InteractableChoice> _choicesDone;
    private AudioSource _source;
    private int _choicesCurrentIndex;
    private int _choicesDoneCurrentIndex;
    private bool _isLastNormalChoice;
    
    public bool IsWrongChoice()
    {
        if(ChoicesDone.Count > 0 && ChoicesDone[ChoicesDone.Count - 1].Data.State == -1)
        {
            return true;
        }

        return false;
    }

    public void GameEndHandler()
    {
        foreach (InteractableChoice choice in ChoicesDone)
        {
            GameObject aux = GameObject.Instantiate(_scoreboardChoicePrefab, _scoreboardContent.transform);

            aux.GetComponent<TextMeshProUGUI>().text = choice.Name;

            aux.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = choice.Data.ScoreboardSprite;
            aux.transform.GetChild(0).gameObject.SetActive(true);
        }

        if(!_timer.IsTimeOver())
        {
            //Debug.Log(_choicesDoneCurrentIndex);
            if(_choicesDoneCurrentIndex > 0)
            {
                _manImage.sprite = _choicesDone[_choicesDoneCurrentIndex - 1].Data.ManSprite;
            }
            else
            {
                _manImage.sprite = _choicesDone[0].Data.ManSprite;
            }
        }
    }

    public void GameStartHandler()
    {
        _choicesDone?.Clear();
    }

    public void StartCoroutineEndHandler()
    {
        _choices[_choicesCurrentIndex].SetActive(true);
    }

    public void ChoiceSelectedHandler(InteractableChoice choice)
    {
        StartCoroutine(ShowChoicePopUp(choice));
        _choicesDone.Add(choice);
        _choicesDone[_choicesDoneCurrentIndex].Data.Audio.Play(_source);

        if(IsWrongChoice())
        {
            _choicesDoneCurrentIndex++;
            OnWrongChoiceSelected?.Invoke();
        }
        else
        {
            if(_choicesCurrentIndex == _choices.Count - 1 && !_isLastNormalChoice)
            {
                _isLastNormalChoice = true;

                if(_dea.HasDea)
                {
                    _choices.Add(_conditionalChoices[0]);
                }

                if(_samu.HasCalledSAMU)
                {
                    _choices.Add(_conditionalChoices[1]);
                }
            }

            ChangeChoices();    
        }
    }

    public void ChangeChoices()
    {
        if(_choicesCurrentIndex == _choices.Count - 1)
        {
            OnLastChoiceSelected?.Invoke();
        }
        else
        {
            _choices[_choicesCurrentIndex].SetActive(false);
            _choicesCurrentIndex++;
            _choicesDoneCurrentIndex++;
            _choices[_choicesCurrentIndex].SetActive(true);
        }
    }

    public IEnumerator ShowChoicePopUp(InteractableChoice choice)
    {
        if(choice.Data.PopUpSprite != null)
        {
            _timer.StopTime();
            _popUpImage.sprite = choice.Data.PopUpSprite;
            _popUpTween.TweenToFinalAmount();

            yield return new WaitForSeconds(7f);

            _timer.ResumeTime();
            _popUpTween.TweenToInitialAmount();
        }

        yield return null;
    }

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _choicesDone = new List<InteractableChoice>();
        _choicesCurrentIndex = 0;
        _choicesDoneCurrentIndex = 0;
    }

    public List<InteractableChoice> ChoicesDone { get => _choicesDone; }
}
