using UnityEngine;

[CreateAssetMenu(fileName ="NewWeapon",menuName ="Game Data/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float attackSpeed;
    public float range;

    public enum WeaponType { Mele,Range}
    public WeaponType weaponType;

    public GameObject attackPrefab;
    public Sprite icon;
    [TextArea] public string description;
}
