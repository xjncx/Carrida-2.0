using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Motor : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private float _maxPower;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;

    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;
    public float MaxPower => _maxPower;

    public void Buy()
    {
        _isBuyed = true;
    }
}
