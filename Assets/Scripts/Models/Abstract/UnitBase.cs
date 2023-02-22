using UnityEngine;

public abstract class UnitBase : MonoBehaviour,IAttackableUnit, IMoveable, ISkillable
{
    public abstract Vector3 Move(Vector3 direction);

    public abstract void Attack(CharacterController target);

    public abstract void UseSkill();
}
