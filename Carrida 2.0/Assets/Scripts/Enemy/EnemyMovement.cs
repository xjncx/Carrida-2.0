using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 22f;
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
        if (other.TryGetComponent<PlayerTag>(out PlayerTag player))
        {
            Debug.Log("Hit!");
            StartCoroutine(DontAttack());
        }
    }

    private IEnumerator DontAttack()
    {
        _isAttack = false;
        yield return new WaitForSeconds(3f);
        _isAttack = true;
    }
}
