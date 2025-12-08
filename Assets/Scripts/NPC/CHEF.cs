using TMPro;
using UnityEngine;

public class CHEF : MainNPC, IDeliverFood
{
    public TextMeshProUGUI pedidoText;
    public FoodList foodList;
    private bool comidaPreparada = false;
    private string pedidoEnPreparacion;
    private float tiempoPreparacion = 3f; // segundos
    private float contadorPreparacion = 0f;
    private bool preparando = false;

    public bool ComidaPreparada
    {
        get { return comidaPreparada; }
        set { comidaPreparada = value; }
    }

    // Método sobrescrito de la clase base
    public override void GenerarPedido()
    {
        // Aquí podrías poner la lógica de entrega general del chef
        if (comidaPreparada && pedidoEnPreparacion != null)
        {
            Debug.Log("El chef entregó: " + pedidoEnPreparacion);
            comidaPreparada = false;
            pedidoEnPreparacion = null;
        }
    }

    // Método específico para preparar el pedido solicitado
    public void PreparacionPedido(string pedidoSolicitado)
    {
        if (foodList != null && foodList.Foods.Contains(pedidoSolicitado))
        {
            pedidoEnPreparacion = pedidoSolicitado;
            preparando = true;
            comidaPreparada = false;
            contadorPreparacion = 0f;
            pedidoText.text = "";
            pedidoText.text = "El chef está preparando: " + pedidoEnPreparacion;
        }
    }

    private void platoTerminado()
    {
        if (preparando)
        {
            contadorPreparacion += Time.deltaTime;
            if (contadorPreparacion >= tiempoPreparacion)
            {
                comidaPreparada = true;
                preparando = false;
                Debug.Log("El chef terminó de preparar: " + pedidoEnPreparacion);
            }
        }
    }

    void Update()
    {
        platoTerminado();
    }

    public void deliverFood(string pedido)
    {
        if (comidaPreparada && pedidoEnPreparacion == pedido)
        {
            Debug.Log("El chef entregó: " + pedido);
            comidaPreparada = false;
            pedidoEnPreparacion = null;
        }
        else if (preparando)
        {
            Debug.Log("El chef aún está preparando el plato.");
        }
        else
        {
            Debug.Log("El chef no tiene ese plato disponible.");
        }
    }
}
