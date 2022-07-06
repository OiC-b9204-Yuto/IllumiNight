using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IllumiNight.UI.Result
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField] List<GameObject> _characterImage;

        [SerializeField] TMP_Text _defeatedEnemy;
        [SerializeField] TMP_Text _receivedDamage;
        [SerializeField] TMP_Text _totalScore;

        [SerializeField] List<GameObject> _rankImage;

        void Start()
        {

        }

        void SetScoreData(int defeatedEnemy, int receivedDamage, int totalScore)
        {
            _defeatedEnemy.text = "+" + defeatedEnemy.ToString("00");
            _receivedDamage.text = "-" + receivedDamage.ToString("00");
            _receivedDamage.text = totalScore.ToString("00");
        }

    }
}