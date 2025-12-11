using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator playerAnimator;


    [SerializeField] private float Speed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;


        playerAnimator.SetFloat("Horizontal", moveInput.x);
        playerAnimator.SetFloat("Vertical", moveInput.y );
        playerAnimator.SetFloat("Speed", Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y));
    }
    void FixedUpdate()
    {
       
        rb.MovePosition(rb.position + moveInput * Speed * Time.fixedDeltaTime);
    }
}