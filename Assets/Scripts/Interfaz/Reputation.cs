using TMPro;
using UnityEngine;

public class Reputation : MonoBehaviour
{
    public TextMeshProUGUI ReputationText;
    [SerializeField] private int reputation;

    private void Start()
    {
        ActualizarTexto();
    }

    public int GetReputation()
    {
        return reputation;
    }

    public void AddReputation(int amount)
    {
        reputation += amount;
        ActualizarTexto();
    }

    public void SubtractReputation(int amount)
    {
        reputation -= amount;
        reputation = Mathf.Max(reputation, 0);
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        if (ReputationText != null)
            ReputationText.text = "Reputación: " + reputation;
    }
}
