using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballz : MonoBehaviour
{
    private bool colliding = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !colliding)
        {
            colliding = true;
            collision.gameObject.GetComponent<EnemyHealth>().health -= 1;
        }

        Destroy(gameObject);
    }
}