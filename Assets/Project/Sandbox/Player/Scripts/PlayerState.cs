using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PlayerState :MonoBehaviour , IHealth
{
    [SerializeField] private int _initLifePoint;

    [SerializeField] private IntReactiveProperty _currentLifePoint;

    [SerializeField] private float _invincibleTime;
    private float _currentInvincibleTime;

    public int Health => _currentLifePoint.Value;

    public IObservable<int> HealthSubject => _currentLifePoint;

    public int MaxHealth => _initLifePoint;

    void Awake()
    {
        _currentLifePoint = new IntReactiveProperty(_initLifePoint);
        _currentInvincibleTime = 0;
    }

    void Update()
    {
        if(_currentInvincibleTime > 0) { _currentInvincibleTime -= Time.deltaTime; }
    }
    public void TakeDamage(int damage)
    {
        if(_currentInvincibleTime <= 0)
        {
            _currentLifePoint.Value -= damage;
            _currentInvincibleTime = _invincibleTime;
        }
    }
}
