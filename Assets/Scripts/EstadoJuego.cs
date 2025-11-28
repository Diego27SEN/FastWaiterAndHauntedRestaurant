using UnityEngine;

public class EstadoJuego : MonoBehaviour
{
    private Cronometro cronometro;
    private Reputation reputacion;
    private bool resultadoMostrado = false;

    void Start()
    {
        cronometro = GetComponent<Cronometro>();
        reputacion = GetComponent<Reputation>();
    }

    void Update()
    {
        GestionEstado();
    }

    public void GestionEstado()
    {
        if (resultadoMostrado) return; 

        float tiempoActual = cronometro.Tiempo;
        if (tiempoActual >= 420f)
        {
            int valorReputacion = reputacion.GetReputation();
            if (valorReputacion < 1000)
            {
                Debug.Log("¡Has perdido! Tu reputación es menor a 1000.");
            }
            else
            {
                Debug.Log("¡Has ganado! Tu reputación es 1000 o mayor.");
            }
            resultadoMostrado = true;
        }
    }
}
