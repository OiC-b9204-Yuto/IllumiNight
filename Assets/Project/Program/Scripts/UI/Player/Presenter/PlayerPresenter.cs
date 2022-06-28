using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerPresenter : MonoBehaviour
{
    IScoreView _scoreView;
    IHealthView _healthView;

    [SerializeField] GameObject _model;

    void Start()
    {
        var health = _model.GetComponent<IHealth>();
        var score = _model.GetComponent<IScore>();

        _scoreView = GetComponentInChildren<IScoreView>();
        if (_scoreView != null && score != null)
        {
            score.ScoreSubject.Subscribe(_ => _scoreView.Score = _);
        }
        _healthView = GetComponentInChildren<IHealthView>();
        if (_healthView != null && health != null)
        {
            _healthView.MaxHealth = health.MaxHealth;
            health.HealthSubject.Subscribe(_ => _healthView.Health = _);
        }
    }

}
