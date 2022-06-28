using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour, IHealthView
{
    [SerializeField] private Image _healthImage;
    public int MaxHealth { protected get; set; }

    public int Health { set => _healthImage.fillAmount = Mathf.Clamp01((float)value / MaxHealth); }
}
