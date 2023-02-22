using UnityEngine;

public abstract class AttackableCharacterBase : MonoBehaviour, IAttackableCharacter
{
    public abstract void Attack(EnemyController targetEnemy);
}
