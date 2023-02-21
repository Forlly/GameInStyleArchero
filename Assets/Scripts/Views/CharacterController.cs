using UnityEngine;

public class CharacterController : CharacterBase
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    
    private CharacterMove _characterMove = new CharacterMove();
    private CharacterSkill _skill = new CharacterSkill();
    private CharacterAttack _characterAttack = new CharacterAttack();
    
    public void Init(GameModel gameModel)
    {
        gameModel.CharacterMoveEvent += TryMove;
        _characterMove.MoveSpeed = _moveSpeed;
    }

    public void TryMove(Vector2 direction)
    {
        _rigidbody.velocity = new Vector3(Move(direction).x, _rigidbody.velocity.y, Move(direction).y);
        Debug.Log(_rigidbody.velocity);
    }
    public override Vector2 Move(Vector2 direction)
    {
        return _characterMove.Move(direction);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void UseSkill()
    {
        throw new System.NotImplementedException();
    }
}
