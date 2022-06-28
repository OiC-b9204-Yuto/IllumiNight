using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour, IScoreView
{
    [SerializeField] Text _valueText;
    public int Score { set => _valueText.text = value.ToString("000000"); }
}
