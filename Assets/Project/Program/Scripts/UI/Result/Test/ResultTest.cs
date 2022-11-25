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

    [SerializeField] ScoreData scoreData;

    bool _isEndAnimetion = false;

    async void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        int damage = scoreData.ReceivedDamage;
        RankType rankType = damage <= 3 ? RankType.A : (damage <= 6 ? RankType.B : RankType.C); 
        _rankView.GetComponent<RankView>().SetRank(rankType);
        _characterView.GetComponent<CharacterView>().SetRank(RankType.C);

        _defeatedEnemyView.SetValue(scoreData.DefeatedEnemy);
        await _defeatedEnemyView.AnimationStart();
        _receivedDamageView.SetValue(damage);
        await _receivedDamageView.AnimationStart();
        _totalScoreView.SetValue(scoreData.GetScore());
        await _totalScoreView.AnimationStart();

        await _rankView.GetComponent<IAnimation>().AnimationStart();
        _characterView.GetComponent<CharacterView>().SetRank(rankType);

        _isEndAnimetion = true;
    }

    void Update()
    {
        if (_isEndAnimetion && Input.GetMouseButtonDown(0))
        {
            TestSceneChange.Instance.LoadSceneStart("TitleScene", true);
        }
    }
}
