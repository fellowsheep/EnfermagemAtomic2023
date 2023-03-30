using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _timeText = default;
    [SerializeField]
    private float _time;

    private bool _isUpdating = true;

    public bool IsTimeOver()
    {
        return _time < 0;
    }

    public void DecreaseTime()
    {
        if(_isUpdating)
        {
            _time -= Time.deltaTime;
        }
    }

    public void ResumeTime()
    {
        _isUpdating = true;
    }

    public void StopTime()
    {
        _isUpdating = false;
    }

    /*public void SetTime(float time)
    {
        _time = time;
        _timeText.text = _time.ToString(); 
    }*/
    
    private void Update()
    {
        int minutes = (int)_time / 60;
        int seconds = (int)_time % 60;
        _timeText.SetText("{0}:{1:00}", minutes, seconds);
    }
}
