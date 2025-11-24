using UnityEngine;

public abstract class MainNPC : MonoBehaviour
{
    [SerializeField] protected int nivel;
    [SerializeField] protected int reputacion;
    [SerializeField] protected float speed = 2.0f;
    [SerializeField] protected float Contador;
    [SerializeField] protected bool llegoAlPuntoA = false;
    [SerializeField] protected bool llegoAlPuntoB = false;
    [SerializeField] protected bool llegoAlPuntoFinal = false;
    public abstract void GenerarPedido();
    public int Reputacion
    {
        get { return reputacion; }
        set { reputacion = value; }
    }
}