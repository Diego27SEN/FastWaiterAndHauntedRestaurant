using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodList", menuName = "Game/FoodList")]
public class FoodList : ScriptableObject
{
    public List<string> Foods = new List<string>()
    {
        "Ceviche",
        "Anticucho",
        "Pollo a la Brasa",
        "Arroz con pollo",
        "Picarones",
        "Lomo Saltado",
        "Causa Limeña",
    };
}