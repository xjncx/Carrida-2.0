using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _secondsBeforeAttack;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _player;

    private Rigidbody _enemyCarRb;
    private bool _isAttack = true;

    private void Start()
    {
        _enemyCarRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement _))
        {
            StartCoroutine(DontAttack());
        }
    }

    private IEnumerator DontAttack()
    {
        _isAttack = false;
        yield return new WaitForSeconds(_secondsBeforeAttack);
        _isAttack = true;
    }

    private void FollowPlayer()
    {

        if (_player)
        {
            Vector3 playerPosition = _player.transform.position;
            Vector3 direction = (playerPosition - transform.position).normalized;
            transform.LookAt(playerPosition);
            Move(direction);
        }
    }

    private void Move(Vector3 direction)
    {
        if (_isAttack)
            _enemyCarRb.velocity = new Vector3(direction.x, direction.y, direction.z) * _speed;
    }
}
