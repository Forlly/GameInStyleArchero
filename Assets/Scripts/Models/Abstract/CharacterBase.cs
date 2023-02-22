using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IMoveable, IAttackableCharacter, ISkillable
{
    public abstract Vector3 Move(Vector3 direction);

    public abstract void Attack(EnemyController targetEnemy);

    public abstract void UseSkill();
}
