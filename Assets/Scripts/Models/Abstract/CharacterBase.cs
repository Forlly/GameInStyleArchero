using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IMoveable, IAttackable, ISkillable
{
    public abstract Vector3 Move(Vector3 direction);

    public abstract void Attack();

    public abstract void UseSkill();
}
