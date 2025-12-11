using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ControllerMesero : MonoBehaviour
{
    public DialogSystemUI dialogSystemUI;
    public TextMeshProUGUI pedidoText;
    public InputSystem_Actions inputs;
    private Vector2 moveInput;
    private float range = 1.5f;
    private Vector2 dir;
    private float degree = 90;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Mesero mesero;
    private Animator playerAnimator;
    private bool isDialogVisible = false;

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

        playerAnimator = GetComponent<Animator>();
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

        // Ocultar el panel si te alejas de todos los NPC/CHEF
        if (dialogSystemUI != null && dialogSystemUI.IsDialogVisible)
        {
            bool npcOrChefCerca = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("NPC") || collider.CompareTag("CHEF"))
                {
                    npcOrChefCerca = true;
                    break;
                }
            }
            if (!npcOrChefCerca)
            {
                dialogSystemUI.HideDialog();
            }
        }
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

            if (!(collider.CompareTag("NPC") || collider.CompareTag("CHEF"))) continue;

            Vector2 objDir = (collider.transform.position - transform.position).normalized;
            float toDegrees = Mathf.Acos(Vector2.Dot(objDir, dir)) * Mathf.Rad2Deg;
            if (toDegrees > degree) continue;

            // Interacción con NPC
            if (collider.CompareTag("NPC"))
            {
                NPC npc = collider.GetComponent<NPC>();
                if (npc == null) continue;

                // Entregar pedido si corresponde
                if (platoListoParaEntregar == npc.PedidoActual && !npc.PedidoEntregado)
                {
                    npc.PedidoEntregado = true;
                    npc.GetComponent<ControllerNPC>().RecibioComida = true;
                    pedidoActualMesero = "";
                    //SoundManager.Instance.PlaySound("PedidoEntregado", 1f);
                    dialogSystemUI.ShowDialog("Pedido entregado al NPC: " + platoListoParaEntregar);
                    playerAnimator.SetBool("HasOrder", false);
                    playerAnimator.SetTrigger("Delivered");
                    reputationSystem?.AddReputation(npc.Reputacion);
                    platoListoParaEntregar = "";
                    pedidoActualMesero = "";
                    continue; // Ya se entregó, no hace falta seguir
                }

                // Tomar pedido si no hay uno pendiente
                if (pedidoActualMesero == null || pedidoActualMesero == "")
                {
                    pedidoActualMesero = npc.PedidoActual;
                    pedidoText.text = "El NPC pidio " + pedidoActualMesero;
                    //SoundManager.Instance.PlaySound("PedidoRecibido", 1f);

                    if (dialogSystemUI != null)
                        dialogSystemUI.ShowDialog("¡Hola! El NPC pidió " + pedidoActualMesero);
                }
            }
            // Interacción con CHEF
            else if (collider.CompareTag("CHEF"))
            {
                CHEF chef = collider.GetComponent<CHEF>();
                if (chef == null || pedidoActualMesero == null || pedidoActualMesero == "") continue;

                if (!chef.ComidaPreparada)
                {
                    pedidoText.text = "";
                    chef.PreparacionPedido(pedidoActualMesero);
                    if (dialogSystemUI != null)
                        dialogSystemUI.ShowDialog("El CHEF está preparando: " + pedidoActualMesero);
                }
                else
                {
                    chef.deliverFood(pedidoActualMesero);
                    platoListoParaEntregar = pedidoActualMesero;
                    pedidoActualMesero = "";
                    //SoundManager.Instance.PlaySound("PedidoRecibido", 1f);
                    playerAnimator.SetBool("HasOrder", true);
                    pedidoText.text = "Plato listo para entregar al NPC: " + platoListoParaEntregar;
                    if (dialogSystemUI != null)
                        dialogSystemUI.ShowDialog("¡El CHEF ha terminado! Plato listo: " + platoListoParaEntregar);
                }
            }
        }
    }
}