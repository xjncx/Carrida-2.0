using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _amount;

    public int Amount => _amount;

    public void AddCoin()
    {
            _amount++;
    }
}
