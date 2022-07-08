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
    

    async void Start()
    {
        _rankView.GetComponent<RankView>().SetRank(RankType.A);
        _characterView.GetComponent<CharacterView>().SetRank(RankType.A);

        await _rankView.GetComponent<IAnimation>().AnimationStart();
        await _characterView.GetComponent<IAnimation>().AnimationStart();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _rankView.GetComponent<IAnimation>().AnimationSkip();
        }
    }
}
