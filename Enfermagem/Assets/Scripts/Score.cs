using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public OnScoreCalculatedEvent OnScoreCalculated;

    [SerializeField] private DEA _dea = default;

    private float _finalScore;

    public void CalculateFinalScore(List<InteractableChoice> choices)
    {
        foreach(InteractableChoice choice in choices)
        {
            _finalScore += choice.Data.Points;
        }

        if(!_dea.HasDea && choices.Count > 3)
        {
            _finalScore += 14.3f;
        }

        OnScoreCalculated?.Invoke((int)_finalScore);
    }
}
