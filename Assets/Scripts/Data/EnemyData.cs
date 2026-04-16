using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game Data/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int damage;
    public float moveSpeed;
    public float detectionRange;
    public float attackRange;
    public float attackCooldown;
    public int goldReward;
    public GameObject prefab;
}