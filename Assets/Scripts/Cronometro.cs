using UnityEngine;

public class Cronometro : MonoBehaviour, IGestionCronometro
{
    [SerializeField] private float Tiempo;

     void Update()
    {
        GestionCronometro();
    }
    public void GestionCronometro()
    {
        Tiempo += Time.deltaTime;
        Tiempo = Mathf.Min(Tiempo, 420f);
        if (Tiempo >= 420f)
        {
            Debug.Log("El Juego Termino");
        }
    }
}
