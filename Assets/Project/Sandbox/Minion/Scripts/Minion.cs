using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Minion : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    //件の討伐時間を攻撃力依存にする場合のもの
    [SerializeField] private int _attackPoint;
    private Vector3 _direction;
    //テストが終わればSerializeFieldは消すこと
    [SerializeField]private BoolReactiveProperty _isBattleRx;
    public IReadOnlyReactiveProperty<bool> IsBattleRx => _isBattleRx;

    private BaseEnemy _tmpEnemyData = null;

    private float _timer = 0;

    void Update()
    {
        if (!IsBattleRx.Value) { Move(); }
        else 
        { 
            CountTimer(); 
        }
    }

    private void Move()
    {
        //transform.position += _direction * _movementSpeed * Time.deltaTime;
        this.GetComponent<Rigidbody>().velocity = _direction * _movementSpeed;
    }

    private void CountTimer()
    {
        _timer -= Time.deltaTime * _attackPoint;
        if(_timer <= 0) { BattleEnd(); }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (IsBattleRx.Value) { return; }

        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            CollisionEnemy(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Minion"))
        {
            return;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Light"))
        {
            TriggeLight(other);
        }
    }


    private void TriggeLight(Collider collider)
    {
        Vector3 dir = transform.position - collider.transform.position;
        dir.y = 0;
        dir.Normalize();
        if (dir == Vector3.zero)
        {
            dir.z = 1;
        }
        _direction = dir;
    }
    private void CollisionEnemy(Collision collision)
    {
        //敵情報取得 -> 終了時結果を渡すため(BaseEnemy)
        _tmpEnemyData = collision.gameObject.GetComponent<BaseEnemy>();

        if(_tmpEnemyData.IsDead.Value)
        {
            return;
        }

        //戦闘開始につきdirectionを0に
        _direction = Vector3.zero;
        //動きを停止
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;      
        //戦闘 -> 時間計測のみ？ _timerに戦闘時間を
        _tmpEnemyData.BattleStart();
        _timer = _tmpEnemyData.RequiredBattleTime;
        //戦闘開始/終了 -> EventでLightに送る
        _isBattleRx.Value = true;
    }

    private void BattleEnd()
    {
        _isBattleRx.Value = false;
        //動きを再開
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //敵に戦闘終了を送る
        _tmpEnemyData.TakeDamage();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Light"))
        {
            _direction = Vector3.zero;

        }
    }
}