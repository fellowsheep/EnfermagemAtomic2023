using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Score _score = default;
    [SerializeField] private TextMeshProUGUI _finalScoreText = default;

    private void OnEnable()
    {
        _score.OnScoreCalculated.AddListener(ScoreCalculatedHandler);
    }

    private void OnDisable()
    {
        _score.OnScoreCalculated.RemoveListener(ScoreCalculatedHandler);
    }

    private void ScoreCalculatedHandler(int finalScore)
    {
        if(finalScore != 0)  
        {          
            _finalScoreText.text = finalScore.ToString();
        }
        else
        {
            _finalScoreText.text = "0";
        }
    }
}
