using UnityEngine;
using UnityEngine.SceneManagement; // <-- Importante

public class EstadoJuego : MonoBehaviour
{
    [SerializeField]private Cronometro cronometro;
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
            if (valorReputacion >= 1000)
            {
                SceneManager.LoadScene("Victoria");
            }
            else
            {
                SceneManager.LoadScene("Derrota");
            }
            resultadoMostrado = true;
        }
    }
}
