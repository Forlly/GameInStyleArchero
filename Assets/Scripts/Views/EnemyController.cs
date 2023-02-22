using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : UnitBase
{
    public EnemyType EnemyType;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Image _totalHealthImg;
    [SerializeField] private Image _currentHealthImg;

    public EnemyMoveable EnemyMoveable = new EnemyMoveable();
    private CharacterSkillable _characterSkillable = new CharacterSkillable();
    private CharacterAttackable _characterAttackable = new CharacterAttackable();
    
    [SerializeField] private float _speedMove;
    [SerializeField] private Vector2 _distanceMoving;
    
    private float _immobilityTime;
    private float _currentImmobilityTime;
    private int _startHealth;
    private int _currentHealth;
    private int _attackDelay;
    private int _attackDamage;

    public void Init(GameModel gameModel)
    {
        _immobilityTime = 3500f;
        _currentImmobilityTime = 3500f;
        _startHealth = 20;
        _currentHealth = 20;
        _attackDelay = 1000;
        _attackDamage = 5;
        _agent.speed = _speedMove;
        
        EnemyMoveable.MoveSpeed = _speedMove;
        EnemyMoveable.SpawnPoint = transform.position;
        EnemyMoveable.Distance = _distanceMoving;
        EnemyMoveable.SetExtremePoints();

        gameModel.EnemyMoveEvent += TryMove;
    }

    public void TryMove(int msec)
    {
        _currentImmobilityTime += msec;

        if (!(_currentImmobilityTime >= _immobilityTime)) return;
        
       
        _currentImmobilityTime -= _immobilityTime;
        _agent.SetDestination(Move(CharacterController.Instance.transform.position));

    }
    
    public override Vector3 Move(Vector3 direction)
    {
      return EnemyMoveable.Move(direction);
    }
    
    public override void Attack()
    {
        _characterAttackable.Attack();
    }

    public override void UseSkill()
    {
        _characterSkillable.UseSkill();
    }
    
    public void UpdateHealthView(int currentHP, int totalHP)
    {

        _totalHealthImg.enabled = true;
        _currentHealthImg.enabled = true;
        
        float percentCurrentHp = 100f * currentHP / totalHP;
        
        _currentHealthImg.fillAmount = percentCurrentHp/100f;
        
    }
}

public enum EnemyType
{
    Ground,
    Flying
}