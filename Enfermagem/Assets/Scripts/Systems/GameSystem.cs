using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private ChoicesManager _choicesManager = default;
    [SerializeField] private GameManager _gameManager = default;
    [SerializeField] private PlayerController _playerController = default;
    [SerializeField] private Score _score = default;

    private void OnEnable()
    {
        _gameManager.OnGameStart.AddListener(_choicesManager.GameStartHandler);
        _gameManager.OnGameStart.AddListener(_playerController.GoToStartLocation);
        _gameManager.OnStartCoroutineEnd.AddListener(_choicesManager.StartCoroutineEndHandler);
        _gameManager.OnGameEnd.AddListener(_choicesManager.GameEndHandler);
        _gameManager.OnGameEnd.AddListener(_playerController.GoToEndLocation);
        _gameManager.OnGameEnd.AddListener(GameEnd);
        _choicesManager.OnWrongChoiceSelected.AddListener(_gameManager.EndGame);
        _choicesManager.OnLastChoiceSelected.AddListener(_gameManager.EndGame);
    }

    private void OnDisable()
    {
        _gameManager.OnGameStart.RemoveListener(_choicesManager.GameStartHandler);
        _gameManager.OnGameStart.RemoveListener(_playerController.GoToStartLocation);
        _gameManager.OnStartCoroutineEnd.RemoveListener(_choicesManager.StartCoroutineEndHandler);
        _gameManager.OnGameEnd.RemoveListener(_choicesManager.GameEndHandler);
        _gameManager.OnGameEnd.RemoveListener(_playerController.GoToEndLocation);
        _gameManager.OnGameEnd.RemoveListener(GameEnd);
        _choicesManager.OnWrongChoiceSelected.RemoveListener(_gameManager.EndGame);
    }
    
    private void GameEnd()
    {
        _score.CalculateFinalScore(_choicesManager.ChoicesDone);
    }
}
