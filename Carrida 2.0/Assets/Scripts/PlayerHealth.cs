using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _secondsBeforeDamage;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private UnityEvent _dead = new UnityEvent();
    [SerializeField] private UnityEvent _hit = new UnityEvent();

    private int _currentHealth;
    private bool _avaibleToAttack = true;
    private bool _isAlive = true;
    public bool IsAlive => _isAlive;

    public event UnityAction PlayerDead
    {
        add => _dead.AddListener(value);
        remove => _dead.RemoveListener(value);
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyAttack>(out EnemyAttack enemyAttack))
        {
            if (_avaibleToAttack)
            {
                TakeDamage(enemyAttack.Damage);
                StartCoroutine(UnableToAttack());
            }
        }
    }

    private void TakeDamage(int damage)
    {
        if (_currentHealth > damage)
        {
            _hit.Invoke();
            _currentHealth -= damage;
            _healthBar.SetHealth(_currentHealth);
        }
        else
        {
            _currentHealth = 0;
            _healthBar.SetHealth(_currentHealth);
            _isAlive = false;
            _dead.Invoke();
        }
    }

    private IEnumerator UnableToAttack()
    {
        _avaibleToAttack = false;
        yield return new WaitForSeconds(_secondsBeforeDamage);
        _avaibleToAttack = true;
    }
}


