using UnityEngine;

public class EnemyMoveable : MoveableBase
{
    public Vector2 Distance;
    public Vector3 SpawnPoint;

    private float _minX, _minZ, _maxX, _maxZ;

    public void SetParameters(float movingSpeed,Vector2 distance, Vector3 spawnPoint)
    {
        MoveSpeed = movingSpeed;
        Distance = distance;
        SpawnPoint = spawnPoint;
    }

    public void SetExtremePoints()
    {
        _minX = SpawnPoint.x - Distance.x;
        _maxX = SpawnPoint.x + Distance.x;
        _minZ = SpawnPoint.z - Distance.y;
        _maxZ = SpawnPoint.z + Distance.y;
    }

    public override Vector3 Move(Vector3 direction)
    {
        Vector3 targetPos = Vector3.zero;

        if (direction.x > _maxX)
        {
            targetPos.x = _maxX;
        }
        else if (direction.x < _minX)
        {
            targetPos.x = _minX;
        }
        else
        {
            targetPos.x = direction.x;
        }
        
        if (direction.z > _maxZ)
        {
            targetPos.z = _maxZ;
        }
        else if (direction.z < _minZ)
        {
            targetPos.z = _minZ;
        }
        else
        {
            targetPos.z = direction.z;
        }
        targetPos.y = SpawnPoint.y;
        return targetPos;
    }
}
