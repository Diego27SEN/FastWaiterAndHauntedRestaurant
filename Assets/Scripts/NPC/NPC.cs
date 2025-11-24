using UnityEngine;
using TMPro;

public class NPC : MainNPC
{
    public GameManager gameManager;
    public FoodList foodList;
    private string pedidoActual;
    private int ultimoNivelMostrado = 0; // Guarda el último nivel mostrado
    private bool pedidoEntregado = false;
    public string PedidoActual
    {
        get { return pedidoActual; }
        set { pedidoActual = value; }
    }
    public bool PedidoEntregado
    {
        get { return pedidoEntregado; }
        set { pedidoEntregado = value; }
    }
    void Start()
    {
        GenerarPedido();
    }

    void Update()
    {
        ActualizarNivelPorReputacion();
    }

    public override void GenerarPedido()
    {
        if (foodList != null && foodList.Foods.Count > 0)
        {
            int index = Random.Range(0, foodList.Foods.Count);
            pedidoActual = foodList.Foods[index];
        }
    }

    private void GestionContador()
    {
        Contador += Time.deltaTime;
        Contador = Mathf.Min(Contador, 60f);
    }

    private void ActualizarNivelPorReputacion()
    {
        if (Contador <= 20f)
            nivel = 1;
        else if (Contador <= 40f)
            nivel = 2;
        else if (Contador <= 60f)
            nivel = 3;
        GestionarPorNivel();
        GestionContador();
    }

    private void GestionarPorNivel()
    {
        if (nivel != ultimoNivelMostrado)
        {
            switch (nivel)
            {
                case 1:
                    //Debug.Log("Reputacion: Feliz");
                    Reputacion = 100;
                    break;
                case 2:
                    //Debug.Log("Reputacion: Media");
                    Reputacion = 50;
                    break;
                case 3:
                    //Debug.Log("Reputacion: Baja");
                    Reputacion = 10;
                    break;
            }
            ultimoNivelMostrado = nivel;
        }
    }
}