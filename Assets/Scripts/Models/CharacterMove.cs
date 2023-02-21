using UnityEngine;

public class CharacterMove : CharacterMoveBase
{
    public override Vector2 Move(Vector2 direction)
    {
        return new Vector2(direction.x*MoveSpeed, direction.y*MoveSpeed);
    }
}
