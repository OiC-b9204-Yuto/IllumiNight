using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Minion : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Vector3 _direction;
    //�e�X�g���I����SerializeField�͏�������
    [SerializeField]private BoolReactiveProperty _isBattleRx;
    public IReadOnlyReactiveProperty<bool> IsBattleRx => _isBattleRx;


    void Update()
    {
        if (!IsBattleRx.Value) { Move(); }
    }

    private void Move()
    {
       transform.position += _direction * _movementSpeed * Time.deltaTime; 
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsBattleRx.Value) { return; }

        if(other.gameObject.tag == "Enemy")
        {
            TriggerEnemy(other);
        }
        else if (other.gameObject.tag == "Light")
        {
            TriggerLight(other);
        }
    }

    private void TriggerLight(Collider collider)
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
    private void TriggerEnemy(Collider collider)
    {
        //�G���擾 -> �I�������ʂ�n������(BaseEnemy)
        //�퓬 -> ���Ԍv���̂݁H
        //�퓬�J�n/�I�� -> Event��Light�ɑ���
        //���ʂ��Y���G�l�~�[�ɑ���
        _isBattleRx.Value = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            _direction = Vector3.zero;
        }
    }
}