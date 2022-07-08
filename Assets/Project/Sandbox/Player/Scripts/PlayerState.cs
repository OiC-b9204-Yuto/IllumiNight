using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PlayerState :MonoBehaviour , IHealth
{
    [SerializeField] private int _initLifePoint;

    [SerializeField] private IntReactiveProperty _currentLifePoint;

    public int Health => _currentLifePoint.Value;

    public IObservable<int> HealthSubject => _currentLifePoint;

    public int MaxHealth => _initLifePoint;

    void Awake()
    {
        _currentLifePoint = new IntReactiveProperty(_initLifePoint);
    }
    public void TakeDamage(int damage)
    {
        _currentLifePoint.Value -= damage;
    }
}
