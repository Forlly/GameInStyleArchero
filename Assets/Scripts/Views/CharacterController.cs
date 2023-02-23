using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : CharacterBase
{
    public static CharacterController Instance;
    public Action DieCharacterEvent;
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Image _totalHealthImg;
    [SerializeField] private Image _currentHealthImg;
    [SerializeField] private LayerMask _bulletsLayer;

    public Weapon Weapon;
    [SerializeField] private Transform _spawnBulletPos;
    
    private CharacterMoveable _characterMoveable = new CharacterMoveable();
    private CharacterSkillable _characterSkillable = new CharacterSkillable();
    [SerializeField] private AttackableCharacter attackableCharacter;
    private int _currentAttackDelay;
    private int _startHealth;
    private int _currentHealth;
    
    public void Init(GameModel gameModel)
    {
        if (Instance == null)
            Instance = this;
        
        _characterMoveable.MoveSpeed = _moveSpeed;
        _currentAttackDelay = 0;
        _startHealth = 100;
        _currentHealth = _startHealth;

        attackableCharacter.SetParameters(Weapon, _spawnBulletPos, _bulletsLayer);

        gameModel.CharacterMoveEvent += TryMove;
        gameModel.StartAttackUnitEvent += TryAttack;
    }

    public void TryMove(Vector3 direction)
    {
        _rigidbody.velocity = new Vector3(Move(direction).x, _rigidbody.velocity.y, Move(direction).y);
        _rigidbody.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        _currentAttackDelay = Weapon.AttackDelay;
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
        if (_currentAttackDelay >= Weapon.AttackDelay)
        {
            _currentAttackDelay -= Weapon.AttackDelay;

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
            
            _rigidbody.transform.LookAt(new Vector3(targetEnemy.transform.position.x, _rigidbody.transform.position.y,
                targetEnemy.transform.position.z));
            attackableCharacter.Attack(targetEnemy.transform.position);
        }
    }

    public override void Attack(Vector3 targetEnemy)
    {
        attackableCharacter.Attack(targetEnemy);
    }
    public void ReceiveDamage(int damage)
    { ;
        _currentHealth -= damage;
        UpdateHealthView(_currentHealth, _startHealth);

        if (_currentHealth <= 0)
        {
            Debug.Log("DIE");
            DieCharacterEvent?.Invoke();
        }
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
