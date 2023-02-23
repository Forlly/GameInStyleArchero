using UnityEngine;

public abstract class AttackableBase : MonoBehaviour, IAttackable
{
    public abstract void Attack(Vector3 target);
}
