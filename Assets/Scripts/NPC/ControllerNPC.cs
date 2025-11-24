using UnityEngine;

public class ControllerNPC : MonoBehaviour
{
    public Transform PUNTOA;
    public Transform PUNTOB;
    public Transform PUNTOFinal;
    public float speed = 1f;
    private float distanciaMinima = 0.1f;

    private bool llegoAlPuntoA = false;
    private bool llegoAlPuntoB = false;
    private bool recibioComida = false;
    private bool saliendo = false;
    private bool llegoAlPuntoFinal = false;

    public bool RecibioComida
    {
        get { return recibioComida; }
        set { recibioComida = value; }
    }

    void Update()
    {
        MovimientoGeneral();
    }

    public void MovimientoGeneral()
    {
        if (!recibioComida)
        {
            AccionMovimientoInicial();
        }
        else
        {
            AccionMovimientoSalida();
        }
    }
    public void AccionMovimientoInicial()
    {
        if (!llegoAlPuntoA && PUNTOA != null)
        {
            llegoAlPuntoA = MoverHaciaPunto(PUNTOA);
        }
        else if (!llegoAlPuntoB && llegoAlPuntoA && PUNTOB != null)
        {
            llegoAlPuntoB = MoverHaciaPunto(PUNTOB);
        }
    }

    public void AccionMovimientoSalida()
    {
        // Primero regresa a PUNTOA
        if (!saliendo)
        {
            saliendo = MoverHaciaPunto(PUNTOA);
        }
        // Cuando llega a PUNTOA, va a PUNTOFinal
        else if (!llegoAlPuntoFinal && PUNTOFinal != null)
        {
            llegoAlPuntoFinal = MoverHaciaPunto(PUNTOFinal);
            if (llegoAlPuntoFinal)
            {
                Destroy(gameObject); // Elimina el NPC al llegar al punto final
            }
        }
    }

    private bool MoverHaciaPunto(Transform destino)
    {
        return MoverHaciaPunto(destino, speed);
    }

    private bool MoverHaciaPunto(Transform destino, float velocidad)
    {
        Vector3 direccion = (destino.position - transform.position).normalized;
        float distancia = Vector3.Distance(transform.position, destino.position);

        if (distancia > distanciaMinima)
        {
            transform.position += direccion * velocidad * Time.deltaTime;
            return false;
        }
        else
        {
            return true;
        }
    }
}
