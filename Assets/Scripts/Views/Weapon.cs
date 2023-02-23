using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite Sprite;
    public WeaponType Type;
    public int Damage;
    public float LifeTime;
    public float BulletSpeed;
    public int AttackDelay;
}

public enum WeaponType
{
    Gun,
    MGUn
}
