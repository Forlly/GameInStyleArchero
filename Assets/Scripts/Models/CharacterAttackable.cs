

using UnityEngine;

public class CharacterAttackable : AttackableCharacterBase
{
    
    public int AttackDamage;
    public override void Attack(EnemyController targetEnemy)
    {
        targetEnemy.ReceiveDamage(AttackDamage);
        Debug.Log("Attack");
    }
    
}
