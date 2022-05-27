using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

public class Light : MonoBehaviour
{
    [SerializeField] private GameObject _light;
    [SerializeField] private Minion _minion;

    //イベントを作成-> ミニオンの戦闘開始/終了
    //上記イベント内で自身のオンオフを変更

    private void Start()
    {
        _minion.IsBattleRx.Subscribe((battleFlg) => _light.SetActive(!battleFlg));
    }
}
