using IllumiNight.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;

namespace IllumiNight.UI.Result
{
    public class CharacterView : MonoBehaviour, IAnimation
    {
        [SerializeField] List<GameObject> _ImageList;

        RankType _rank;

        void Awake()
        {
            _ImageList.ForEach(_ => _.SetActive(false));
        }

        public void SetRank(RankType rank)
        {
            _rank = rank;
            _ImageList.ForEach(_ => _.SetActive(false));
            _ImageList[(int)_rank].SetActive(true);
        }

        public UniTask AnimationStart()
        {
            return UniTask.CompletedTask;
        }

        public void AnimationSkip()
        {

        }
    }
}