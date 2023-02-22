using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<EnemyController>()
                .ReceiveDamage(CharacterController.Instance._weapon.Damage);
        }
        else
        {
            BulletsPool.Instance.TurnOfObject(gameObject);
        }
    }
}
