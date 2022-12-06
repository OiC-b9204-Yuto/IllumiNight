using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TipsData", menuName = "ScriptableObjects/TipsData")]
public class TipsData : ScriptableObject
{
    [SerializeField] private string _title;
    public string Title { get { return _title; } }
    [SerializeField, Multiline] private string _description;
    public string Description { get { return _description; } }
}