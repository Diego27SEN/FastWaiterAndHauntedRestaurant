using UnityEngine;

public class Cronometro : MonoBehaviour, IGestionCronometro
{
    [SerializeField] private float tiempo;

    public float Tiempo
    {
        get => tiempo;
        set => tiempo = value;
    }

    void Update()
    {
        GestionCronometro();
    }
    public void GestionCronometro()
    {
        tiempo += Time.deltaTime;
        tiempo = Mathf.Min(tiempo, 420f);
    }
}
