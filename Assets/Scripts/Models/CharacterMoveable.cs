using UnityEngine;

public class CharacterMoveable : MoveableBase
{
    public override Vector3 Move(Vector3 direction)
    {
        return new Vector3(direction.x*MoveSpeed, direction.y*MoveSpeed);
    }
}
