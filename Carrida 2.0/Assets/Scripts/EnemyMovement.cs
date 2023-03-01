using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _secondsBeforeAttack;
    [SerializeField] private float _speed;
    private PlayerMovement _player;
    private Rigidbody _rb;
    private bool _isAttack = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        _player = FindObjectOfType<PlayerMovement>();

        if (_player)
        {
            Vector3 playerPosition = _player.transform.position;
            Vector3 direction = (playerPosition - transform.position).normalized;
            transform.LookAt(playerPosition);
            MoveToPlayer(direction);
        }
    }

    private void MoveToPlayer(Vector3 direction)
    {
        if (_isAttack)
            _rb.velocity = new Vector3(direction.x, direction.y, direction.z) * _speed;
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
}
