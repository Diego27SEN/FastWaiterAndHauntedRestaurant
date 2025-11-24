using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerMesero : MonoBehaviour
{
    public InputSystem_Actions inputs;
    private Vector2 moveInput;
    private float range = 1.5f;
    private Vector2 dir;
    private float degree = 90;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Mesero mesero;

    private string pedidoActualMesero;

    [SerializeField] private string platoListoParaEntregar = "";

    private Reputation reputationSystem;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    private void Awake()
    {
        inputs = new InputSystem_Actions();
        // Busca el objeto GameManager y obtiene el componente Reputation
        var gm = GameObject.Find("GameManager");
        if (gm != null)
            reputationSystem = gm.GetComponent<Reputation>();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Move.started += OnMove;
        inputs.Player.Move.performed += OnMove;
        inputs.Player.Move.canceled += OnMove;
        //////////////////////////////////////////////////////////////////
        inputs.Player.Interact.performed += OnInteract;
        inputs.Player.Interact.started += OnInteract;
    }
    private void OnDisable()
    {
        inputs.Player.Move.started -= OnMove;
        inputs.Player.Move.performed -= OnMove;
        inputs.Player.Move.canceled -= OnMove;
        /////////////////////////////////////////////////////////////////
        inputs.Player.Interact.performed -= OnInteract;
        inputs.Player.Interact.started -= OnInteract;
        inputs.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.state == GameManager.GameState.Paused)
            return;

        moveInput = context.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
            dir = moveInput.normalized;
    }
    private void Update()
    {
        // bloquear movimiento si está pausado
        if (GameManager.Instance != null &&
            GameManager.Instance.state == GameManager.GameState.Paused)
            return;

        MovePlayer();
    }

    public void MovePlayer()
    {
        transform.position += (Vector3)moveInput * moveSpeed * Time.deltaTime;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (GameManager.Instance != null &&
           GameManager.Instance.state == GameManager.GameState.Paused)
            return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("NPC") || collider.CompareTag("CHEF"))
            {
                Vector2 objDir = (collider.transform.position - transform.position).normalized;
                float producto = Vector2.Dot(objDir, dir);
                float toRadians = Mathf.Acos(producto);
                float toDegrees = toRadians * Mathf.Rad2Deg;
                if (toDegrees <= degree)
                {
                    if (collider.CompareTag("NPC"))
                    {
                        // Solo acepta un pedido si no tiene uno pendiente
                        if (pedidoActualMesero == null || pedidoActualMesero == "") // Solo toma un nuevo pedido si no hay uno actual
                        {
                            NPC npc = collider.GetComponent<NPC>();
                            if (npc != null)
                            {
                                pedidoActualMesero = npc.PedidoActual;
                                Debug.Log("Pedido recibido: " + pedidoActualMesero);
                            }
                        }
                        else
                        {
                            Debug.Log("Debes entregar el pedido actual antes de tomar uno nuevo.");
                        }
                    }
                    else if (collider.CompareTag("CHEF"))
                    {
                        CHEF chef = collider.GetComponent<CHEF>();
                        if (chef != null && pedidoActualMesero != null && pedidoActualMesero != "")
                        {
                            if (!chef.ComidaPreparada)
                            {
                                chef.PreparacionPedido(pedidoActualMesero);
                            }
                            else
                            {
                                chef.deliverFood(pedidoActualMesero);
                                platoListoParaEntregar = pedidoActualMesero; // Guarda el plato listo
                                pedidoActualMesero = "";
                                Debug.Log("Plato listo para entregar al NPC: " + platoListoParaEntregar);
                            }
                        }
                    }
                }
            }
            if (collider.CompareTag("NPC"))
            {
                NPC npc = collider.GetComponent<NPC>();
                if (npc != null && platoListoParaEntregar == npc.PedidoActual && !npc.PedidoEntregado)
                {
                    npc.PedidoEntregado = true;
                    npc.GetComponent<ControllerNPC>().RecibioComida = true;
                    Debug.Log("Pedido entregado al NPC: " + platoListoParaEntregar);

                    // Actualiza la reputación global usando la reputación del NPC
                    if (reputationSystem != null)
                        reputationSystem.AddReputation(npc.Reputacion);

                    platoListoParaEntregar = "";
                    pedidoActualMesero = "";
                }
            }
        }
    }
}