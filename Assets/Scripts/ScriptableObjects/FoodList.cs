using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    None,
    Ceviche,
    Huancaina,
    Seco,
    ArrozConPollo
}

[CreateAssetMenu(fileName = "FoodList", menuName = "Game/FoodList")]
public class FoodList : ScriptableObject
{
    public List<string> Foods = new List<string>()
    {
        "Ceviche",
        "Anticucho",
        "Arroz con pollo",
        "Aji de gallina",
        "Pollo ala brasa",
        "inka cola",
        "lomo saltado",
        "Calodo de gallina",
        "Causa rellena",
        "Chaufa de pollo",
    };
}