using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IMoveable, IAttackable, ISkillable
{
    public abstract Vector3 Move(Vector3 direction);

    public abstract void Attack(Vector3 target);

    public abstract void UseSkill();
}
