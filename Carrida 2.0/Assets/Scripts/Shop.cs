using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Motor> _motors;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private MotorView _template;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private Wallet _wallet;

    private void Start()
    {
        for (int i = 0; i < _motors.Count; i++)
        {
            AddItem(_motors[i]);
        }
    }

    private void AddItem(Motor motor)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(motor);
    }

    private void OnSellButtonClick(Motor motor, MotorView view)
    {
        TrySellWeapon(motor, view);
    }

    private void TrySellWeapon(Motor motor, MotorView view)
    {
        if (motor.Price <= _wallet.Amount)
        {
            var motorToPlayer = Instantiate(motor);
            _playerMovement.ChangeMotor(motorToPlayer);
            _wallet.Spend(motor.Price);
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}

