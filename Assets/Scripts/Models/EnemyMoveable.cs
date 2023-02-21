using UnityEngine;

public class EnemyMoveable : MoveableBase
{
    public Vector2 Distance;
    public Vector3 SpawnPoint;

    private float _minX, _minZ, _maxX, _maxZ;

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
        targetPos.y = SpawnPoint.y;
        
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
        
        return targetPos;
    }
}
