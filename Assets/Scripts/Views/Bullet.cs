using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<EnemyController>()
                .ReceiveDamage(Damage);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<CharacterController>()
                .ReceiveDamage(Damage);
        }
        else
        {
            if (!collision.gameObject.CompareTag("Bullet"))
                BulletsPool.Instance.TurnOfObject(gameObject);
        }
    }
}
