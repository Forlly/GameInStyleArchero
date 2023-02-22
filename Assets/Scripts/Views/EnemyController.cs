using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : UnitBase
{
    public EnemyMoveable EnemyMoveable = new EnemyMoveable();
    public Action<EnemyController> DieUnitEvent;
    public EnemyType EnemyType;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Image _totalHealthImg;
    [SerializeField] private Image _currentHealthImg;
    
    private CharacterSkillable _characterSkillable = new CharacterSkillable();
    private EnemyAttackable _enemyAttackable = new EnemyAttackable();
    
    [SerializeField] private float _speedMove;
    [SerializeField] private Vector2 _distanceMoving;
    
    private float _immobilityTime;
    private float _currentImmobilityTime;
    private int _startHealth;
    private int _currentHealth;
    private int _attackDelay;
    private int _attackDamage;

    private GameModel _gameModel;

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
        _gameModel = gameModel;
        _gameModel.EnemyMoveEvent += TryMove;
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
    
    public override void Attack(CharacterController target)
    {
        _enemyAttackable.Attack(target);
    }

    public override void UseSkill()
    {
        _characterSkillable.UseSkill();
    }

    public void StopMoving()
    {
        _agent.velocity = Vector3.zero;
        _agent.Stop();
        _gameModel.EnemyMoveEvent -= TryMove;
    }
    public void ReceiveDamage(int damage)
    {
        _currentHealth -= damage;
        UpdateHealthView(_currentHealth, _startHealth);

        if (_currentHealth <= 0)
        {
            Debug.Log("DIE");
            DieUnitEvent?.Invoke(this);
            
        }
        
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