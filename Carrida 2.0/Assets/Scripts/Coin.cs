using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Wallet>(out Wallet wallet))
        {
            wallet.AddCoin();
            Destroy(gameObject);
        }
    }
}

