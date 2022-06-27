using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private int _initLifePoint;

    [SerializeField] private IntReactiveProperty _currentLifePoint;
    /// <summary>
    /// 現在のライフポイント
    /// </summary>
    public IReadOnlyReactiveProperty<int> CurrentLifePoint => _currentLifePoint;

    [SerializeField] protected BoolReactiveProperty _isDead;
    /// <summary>
    /// 死亡用フラグ
    /// </summary>  
    public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
    void Awake()
    {
        _currentLifePoint = new IntReactiveProperty(_initLifePoint);
        _isDead = new BoolReactiveProperty(false);
        CurrentLifePoint.Where(_ => _ <= 0).Subscribe(_ => _isDead.Value = true);
    }
    public void TakeDamage(int damage)
    {
        _currentLifePoint.Value -= damage;
    }
}
