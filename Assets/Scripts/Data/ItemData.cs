using UnityEngine;

[CreateAssetMenu(fileName ="NewItem",menuName ="Game Data/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;

    public enum ItemType { Consumble,Passive,Weapon }
    public ItemType itemType;

    public int healthRestore;
    public float speedBonus;
    public float damageReduction;
}
