using UnityEngine;

public class Reputation : MonoBehaviour
{
    [SerializeField] private int reputation;
    public int GetReputation()
    {
        return reputation;
    }

    public void AddReputation(int amount)
    {
        reputation += amount;
    }

    public void SubtractReputation(int amount)
    {
        reputation -= amount;
        reputation = Mathf.Max(reputation, 0);
    }
}
