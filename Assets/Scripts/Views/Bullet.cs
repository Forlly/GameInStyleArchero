using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool InPull;
    public int Damage;
    public Action BulletCollision;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<EnemyController>()
                .ReceiveDamage(Damage);
            BulletCollision?.Invoke();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<CharacterController>()
                .ReceiveDamage(Damage);
            BulletCollision?.Invoke();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            BulletCollision?.Invoke();
        }
    }
}
