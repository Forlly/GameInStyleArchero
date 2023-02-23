using System.Collections;
using UnityEngine;

public class AttackableCharacter : AttackableBase
{
    
    public int AttackDamage;
    public Weapon Weapon;
    public Transform SpawnBulletPos;
    public float LifeTime;
    public override void Attack(Vector3 targetEnemy)
    {
        Fire(targetEnemy);
    }

    public void SetParameters(int damage, Weapon weapon,  Transform spawnBulletPos)
    {
        AttackDamage = damage;
        Weapon = weapon;
        SpawnBulletPos = spawnBulletPos;
    }
    public void Fire(Vector3 targetPosition)
    {
        StartCoroutine(Shot(targetPosition));
    }

    private IEnumerator Shot(Vector3 targetPosition)
    {
        Vector3 startPoint = SpawnBulletPos.position;

        GameObject _bullet = BulletsPool.Instance.GetPooledObject();
        if (_bullet != null)
        {
            _bullet.transform.position = startPoint;
        }

        _bullet.GetComponent<Bullet>().Damage = Weapon.Damage;
        float wspeed = (Weapon.SpeedAttack * Time.deltaTime) /
                       Vector3.Distance(startPoint, targetPosition);

        LifeTime = Vector3.Distance(startPoint, targetPosition) /
                   (Weapon.SpeedAttack *  Time.deltaTime);
        

        float progressFly = 0f;

        float currentTime = 0f;
        while (true)
        {
            yield return null;
            currentTime += Time.fixedDeltaTime;

            progressFly += wspeed;

            _bullet.transform.position =
                Vector3.Slerp(startPoint, targetPosition , progressFly);

            if (currentTime >= LifeTime || progressFly > 1)
            {
                BulletsPool.Instance.TurnOfObject(_bullet);
                yield break;
            }
        }
    }
    
}