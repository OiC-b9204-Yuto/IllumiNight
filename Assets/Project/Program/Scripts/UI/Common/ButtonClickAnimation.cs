using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickAnimation : MonoBehaviour
{
    [SerializeField] Button button;

    [SerializeField] AnimationCurve animationCurve;

    [SerializeField] float _animetionSpeed = 3  ;

    bool _isAnimetion;
    bool _isEnd;

    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    private void Start()
    {   
        button.onClick.AddListener(ClickAnimetion);
    }

    public void ClickAnimetion()
    {
        if (_isAnimetion)
        {
            return;
        }
        _isAnimetion = true;
        ClickAnimetionTask(_cancellationTokenSource.Token).Forget(e => { });
    }

    async UniTask ClickAnimetionTask(CancellationToken token)
    {
        float _timer = 0;
        while (_timer < 1) {
            _timer += Time.deltaTime * _animetionSpeed;
            button.transform.localScale = new Vector3(animationCurve.Evaluate(_timer), animationCurve.Evaluate(_timer), 1.0f);
            await UniTask.DelayFrame(1, cancellationToken: token);
        }
        button.transform.localScale = new Vector3(1,1,1);
        _isAnimetion = false;
    }
}
