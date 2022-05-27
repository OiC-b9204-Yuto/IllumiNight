using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

public class Light : MonoBehaviour
{
    [SerializeField] private GameObject _light;
    [SerializeField] private Minion _minion;

    //�C�x���g���쐬-> �~�j�I���̐퓬�J�n/�I��
    //��L�C�x���g���Ŏ��g�̃I���I�t��ύX

    private void Start()
    {
        _minion.IsBattleRx.Subscribe((battleFlg) => _light.SetActive(!battleFlg));
    }
}
