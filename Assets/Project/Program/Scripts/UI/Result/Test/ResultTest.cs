using IllumiNight.Interface;
using IllumiNight.UI.Result;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;
using IllumiNight;

public class ResultTest : MonoBehaviour
{
    [SerializeField] GameObject _rankView;
    [SerializeField] GameObject _characterView;
    [SerializeField] DefeatedEnemyView _defeatedEnemyView;
    [SerializeField] ReceivedDamageView _receivedDamageView;
    [SerializeField] TotalScoreView _totalScoreView;
    

    async void Start()
    {
        _rankView.GetComponent<RankView>().SetRank(RankType.A);
        _characterView.GetComponent<CharacterView>().SetRank(RankType.C);

        _defeatedEnemyView.SetValue(7);
        await _defeatedEnemyView.AnimationStart();
        _receivedDamageView.SetValue(-3);
        await _receivedDamageView.AnimationStart();
        _totalScoreView.SetValue(7 * 600 - 3 * 150);
        await _totalScoreView.AnimationStart();

        await _rankView.GetComponent<IAnimation>().AnimationStart();
        _characterView.GetComponent<CharacterView>().SetRank(RankType.A);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _rankView.GetComponent<IAnimation>().AnimationSkip();
        }
    }
}
