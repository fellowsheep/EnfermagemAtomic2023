using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnGameEnd;
    public UnityEvent OnGameStart;
    public UnityEvent OnStartCoroutineEnd;

    [SerializeField] private RandomPassingOut _randomPassingOut = default; 
    [SerializeField] private CharacterSelector _characterSelector = default; 
    [SerializeField] private CharacterView _characterView = default; 
    [SerializeField] private PlayerController _playerController = default; 
    [SerializeField] private Timer _timer = default;
    [SerializeField] private GameObject _timerCanvasObject = default;
    [SerializeField] private TweenBase _timerTween = default;

    private bool _hasGameEnded;
    private bool _hasGameStarted;

    private static bool _isFirstRun = true;

    public void Awake()
    {
        string version = PlayerPrefs.GetString("version");

        if(_isFirstRun)
        {
            _isFirstRun = false;

            if(version == "" || version != Application.version)
            {
                PlayerPrefs.SetString("version", Application.version);

                bool success = Caching.ClearCache();

                if(success)
                {
                    Debug.Log("Cache cleared!");
                }
            }
        }
    }

    public void StartGame() 
    {
        //_timer.SetTime(71);
        _playerController.GoToStartLocation();
        _playerController.enabled = true;
        OnGameStart?.Invoke();
        StartCoroutine(StartGameCoroutine());
    }

    public void EndGame()
    {
        _hasGameEnded = true;
    }

    private void Update()
    {
        if(_hasGameStarted)
        {
            _timer.DecreaseTime();

            if(_timer.IsTimeOver()) 
            {
                _hasGameEnded = true;
            }
        }

        if(_hasGameEnded)
        {
            _hasGameEnded = false;
            _hasGameStarted = false;
            //ChoiceMusic.Stop();
            //MenuMusic.Play();
            _timerCanvasObject.SetActive(false);

            OnGameEnd.Invoke();
        }            
    }

    private IEnumerator StartGameCoroutine()
    {
        GameObject selectedCharacter = _characterSelector.SelectCharacter();
        _randomPassingOut.SetCharacterPosition(selectedCharacter);

        yield return StartCoroutine(_characterView.ShowCharacterPresentation());

        _randomPassingOut.PassOut(selectedCharacter);
        //MenuMusic.Stop();
        //ChoiceMusic.Play();

        yield return new WaitForSeconds(2f);

        Initialize();
    }

    private void Initialize()
    {
        OnStartCoroutineEnd?.Invoke();
        
        _timerTween.TweenToFinalAmount();

        _hasGameEnded = false;
        _hasGameStarted = true;
    }
}
