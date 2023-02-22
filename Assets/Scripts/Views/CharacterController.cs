using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : CharacterBase
{
    public static CharacterController Instance;
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Image _totalHealthImg;
    [SerializeField] private Image _currentHealthImg;
    
    private CharacterMoveable _characterMoveable = new CharacterMoveable();
    private CharacterSkillable _characterSkillable = new CharacterSkillable();
    private CharacterAttackable _characterAttackable = new CharacterAttackable();
    private int _attackDelay;
    private int _currentAttackDelay = 0;
    private int _startHealth;
    private int _currentHealth;
    
    
    public void Init(GameModel gameModel)
    {
        if (Instance == null)
            Instance = this;
        
        _characterMoveable.MoveSpeed = _moveSpeed;
        _attackDelay = 1000;
        _currentAttackDelay = 0;
        _startHealth = 10;
        _currentHealth = 10;
        _characterAttackable.AttackDamage = 5;
        
        gameModel.CharacterMoveEvent += TryMove;
        gameModel.StartAttackUnitEvent += TryAttack;
    }

    public void TryMove(Vector3 direction)
    {
        _rigidbody.velocity = new Vector3(Move(direction).x, _rigidbody.velocity.y, Move(direction).y);
        _rigidbody.rotation = Quaternion.LookRotation(_rigidbody.velocity);
    }
    public override Vector3 Move(Vector3 direction)
    {
        return _characterMoveable.Move(direction);
    }

    private void StopMoving()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void TryAttack(List<EnemyController> enemies, int msec)
    {
        StopMoving();
        
        if (enemies.Count <= 0) return;
        
        _currentAttackDelay += msec;
        if (_currentAttackDelay >= _attackDelay)
        {
            _currentAttackDelay -= _attackDelay;

            float minDistance = 1000f;

            EnemyController targetEnemy = enemies[0];
            foreach (EnemyController enemy in enemies)
            {
                if (minDistance > Vector3.Distance(enemy.transform.position, this.transform.position))
                {
                    minDistance = Vector3.Distance(enemy.transform.position, this.transform.position);
                    targetEnemy = enemy;
                }
            }
            _characterAttackable.Attack(targetEnemy);
            

        }
    }

    public override void Attack(EnemyController targetEnemy)
    {
        _characterAttackable.Attack(targetEnemy);
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
