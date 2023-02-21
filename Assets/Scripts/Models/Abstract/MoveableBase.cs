using UnityEngine;

public abstract class MoveableBase : IMoveable
{
    public float MoveSpeed;
    public abstract Vector3 Move(Vector3 direction);
}
