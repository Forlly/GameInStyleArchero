using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IMoveable, IAttackable, ISkillable
{
    public abstract Vector2 Move(Vector2 direction);

    public abstract void Attack();

    public abstract void UseSkill();
}
