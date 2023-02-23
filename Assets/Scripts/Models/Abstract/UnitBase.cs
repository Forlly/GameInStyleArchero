using UnityEngine;

public abstract class UnitBase : MonoBehaviour,IAttackable, IMoveable, ISkillable
{
    public abstract Vector3 Move(Vector3 direction);

    public abstract void Attack(Vector3 target);

    public abstract void UseSkill();
}
