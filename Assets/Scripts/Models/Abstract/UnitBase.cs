using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IMoveable, IAttackable, ISkillable
{
    public abstract Vector3 Move(Vector3 direction);

    public abstract void Attack();

    public abstract void UseSkill();
}
