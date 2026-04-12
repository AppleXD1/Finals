using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;


public class Player : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction dashAction;
    public Transform cameraTransform;

    public float moveSpeed = 6f;
    public bool canDash;
    public Rigidbody rb;
    public Animator animator;

    public float Health;
    private float MaxHealth = 100;
    public float Stamina;
    private float MaxStamina = 50;

    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Health = MaxHealth;
        Stamina = MaxStamina;
        moveAction = playerInput.actions.FindAction("Move");
        dashAction = playerInput.actions.FindAction("Dash");
        canDash = true;
    }


    void Update()
    {
            moveInput = moveAction.ReadValue<Vector2>();
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
           

    }

    void FixedUpdate()
    {
        MovePlayer();
        if (Input.GetKey(KeyCode.LeftShift) && Stamina > 10 && canDash)
        {
            Dash();
        }
    }

    void MovePlayer()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
        moveDirection.Normalize();

        Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    void Dash()
    {
        canDash = false;
        StartCoroutine(DashWait());

    }

    IEnumerator DashWait()
    {
        moveSpeed = moveSpeed * 5;
        yield return new WaitForSeconds(0.3f);
        Stamina = Stamina - 5;
        moveSpeed = 6f;
        canDash = true;

    }

    public void TakenDamage(float damage)
    {
        Health -= damage;
        if(Health < 0 )
        {
            Debug.Log("ded");
        }
    }
}
