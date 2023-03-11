using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class MotorView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private Motor _motor;

    public event UnityAction<Motor, MotorView> SellButtonClick;
    private void OnEnable()
    {
        _sellButton.onClick.AddListener(onButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(onButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }
    public void Render(Motor motor)
    {
        _motor = motor;

        _label.text = motor.Label;
        _price.text = motor.Price.ToString();
        _icon.sprite = motor.Icon;
    }

    private void onButtonClick()
    {
        SellButtonClick?.Invoke(_motor, this);
    }

    private void TryLockItem()
    {
        if (_motor.IsBuyed)
        {
            _sellButton.interactable = false;
        }
    }

}
