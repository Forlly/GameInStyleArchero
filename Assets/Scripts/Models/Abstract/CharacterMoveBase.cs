using UnityEngine;

public abstract class CharacterMoveBase : IMoveable
{
    public float MoveSpeed;
    public abstract Vector2 Move(Vector2 direction);
}
