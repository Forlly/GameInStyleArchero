using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableCharacter : AttackableBase
{
    public Weapon Weapon;
    public Transform SpawnBulletPos;

    private List<Bullet> _bullets = new List<Bullet>();
    private LayerMask _bulletsLayer;
    public override void Attack(Vector3 targetEnemy)
    {
        BulletsPool.Instance.StartCoroutine(Shot(targetEnemy));
    }

    public void SetParameters(Weapon weapon,  Transform spawnBulletPos, LayerMask layerMask)
    {
        Weapon = weapon;
        SpawnBulletPos = spawnBulletPos;
        _bulletsLayer = layerMask;
    }
    
    private void SetGameLayerRecursive(GameObject _go, int _layer)
    {
        _go.layer = _layer;
        foreach (Transform child in _go.transform)
        {
            child.gameObject.layer = _layer;
 
            Transform _HasChildren = child.GetComponentInChildren<Transform>();
            if (_HasChildren != null)
                SetGameLayerRecursive(child.gameObject, _layer);
        }
    }
    
    private IEnumerator Shot(Vector3 targetPosition)
    {
        Vector3 startPoint = SpawnBulletPos.position;

        Bullet _bullet = BulletsPool.Instance.GetPooledObject();
        _bullet.BulletCollision += () => TurnOnBulletInPool(_bullet);
        _bullets.Add(_bullet);
        
        SetGameLayerRecursive(_bullet.gameObject, Mathf.RoundToInt(Mathf.Log(_bulletsLayer.value, 2)));
        
        _bullet.Damage = Weapon.Damage;
        float speed = Weapon.BulletSpeed * Time.fixedDeltaTime;
        Vector3 direction = targetPosition - startPoint;
        direction.Normalize();
        Vector3 step = direction * speed;
            
        float lifeTime = Weapon.LifeTime;

        _bullet.transform.position = startPoint;
        while (lifeTime > 0f && !_bullet.InPull)
        {
            lifeTime -= Time.fixedDeltaTime;
            _bullet.transform.position += step;

            yield return null;
        }

        if (!_bullet.InPull)
        {
            BulletsPool.Instance.TurnOfObject(_bullet);
            _bullets.Remove(_bullet);
        }
    }

    private void TurnOnBulletInPool(Bullet bullet)
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (bullet == _bullets[i])
            {
                BulletsPool.Instance.TurnOfObject(bullet);
                _bullets.Remove(bullet);
            }
        }
    }
}
