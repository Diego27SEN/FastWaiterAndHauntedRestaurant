using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "FastWaiterAndHauntedRestaurant/EnemyDataSO", order = 1)]
public class EnemyDataSO : ScriptableObject
{
    public string EnemyName;
    public ulong ID;

    public int Health;
    public int Damage;
    public float Speed;

    public Sprite sprite;
}