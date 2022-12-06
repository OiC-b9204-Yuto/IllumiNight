using Cysharp.Threading.Tasks;
using IllumiNight;
using IllumiNight.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace IllumiNight.UI.Result
{
    public class RankView : MonoBehaviour, IAnimation
    {
        [SerializeField] List<GameObject> _ImageList;
        FadeAnimation _fadeAnimation;
        [SerializeField] AnimationCurve _animationCurve;

        [SerializeField] float _animetionSpeed = 3;

        RankType _rank;

        public bool IsAnimationEnd { get; protected set; }

        private CancellationTokenSource _cancellationTokenSource;

        void Awake()
        {
            IsAnimationEnd = false;
            _ImageList.ForEach(_ => _.SetActive(false));
            _fadeAnimation = GetComponent<FadeAnimation>();
        }

        public void SetRank(RankType rank)
        {
            _rank = rank;
            _ImageList.ForEach(_ => _.SetActive(false));
            _ImageList[(int)_rank].SetActive(true);
        }

        public async UniTask AnimationStart(CancellationToken token = default(CancellationToken))
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await AnimetionTask(_cancellationTokenSource.Token);
        }

        public void AnimationSkip()
        {
            _cancellationTokenSource.Cancel();
            _ImageList[(int)_rank].transform.localScale = new Vector3(1, 1, 1);
        }

        async UniTask AnimetionTask(CancellationToken token)
        {
            float _timer = 0;
            _fadeAnimation.FadeIn();
            while (_timer < 1)
            {
                _timer += Time.deltaTime * _animetionSpeed;
                _ImageList[(int)_rank].transform.localScale = new Vector3(_animationCurve.Evaluate(_timer), _animationCurve.Evaluate(_timer), 1.0f);
                await UniTask.DelayFrame(1, cancellationToken: token);
            }
            _ImageList[(int)_rank].transform.localScale = new Vector3(1, 1, 1);
            IsAnimationEnd = true;
        }

        void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}