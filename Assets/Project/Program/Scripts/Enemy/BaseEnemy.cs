using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

// 敵のベースクラス（作成中：竹中）
//  移動と攻撃を変更できるようにコードを書く
//  Reactiveに頼りすぎない（コードが追いづらくなるのを防ぐ）

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected int _initLifePoint;

    // 現在のライフポイント
    [SerializeField] protected IntReactiveProperty _currentLifePoint;
    public IReadOnlyReactiveProperty<int> CurrentLifePoint => _currentLifePoint;

    // ダメージを受けるまでに必要な戦闘時間
    [SerializeField] protected float _requiredBattleTime;
    public float RequiredBattleTime => _requiredBattleTime;

    // 死亡用フラグ
    [SerializeField] protected BoolReactiveProperty _isDead;
    public IReadOnlyReactiveProperty<bool> IsDead => _isDead;



    protected void Initialize()
    {
        _currentLifePoint = new IntReactiveProperty(_initLifePoint);
        _isDead = new BoolReactiveProperty(false);
        CurrentLifePoint.Where(_ => _ <= 0).Subscribe(_ => _isDead.Value = true); 
    }

    void Start()
    {
        Initialize();
    }


    void Update()
    {
        
    }
}
