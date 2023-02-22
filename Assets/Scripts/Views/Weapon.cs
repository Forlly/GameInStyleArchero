using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite Sprite;
    public WeaponType Type;
    public int Damage;
    public int SpeedAttack;
    public int AttackDelay;
}

public enum WeaponType
{
    Gun,
    MGUn
}
