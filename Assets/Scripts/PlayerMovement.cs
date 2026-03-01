using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Health")]
    public int maxHealth = 20;
    public HealthBarScript healthBarScript;

    [Header("References")]
    public QuestBookBehaviour questBookBehaviour;
    [SerializeField] private GameObject targetObject;

    private Rigidbody rb;
    private Animator animator;

    private bool isGrounded;

    public bool canControl = true;   // ✅ MASTER LOCK

    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;

        ToastNotification.Show("Hey, Civic Guardian!");
        ToastNotification.Show("Your time is ticking!");
        ToastNotification.Show("Meet the police officer to know more");
    }

    void Update()
    {
        // 🚫 HARD LOCK — Nothing works when false
        if (!canControl)
        {
            StopMovement();
            return;
        }

        // =====================
        // INPUT SECTION
        // =====================

        MovePlayer();
        HandleAnimations();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();

        if (Input.GetKeyDown(KeyCode.O) && questBookBehaviour != null)
            questBookBehaviour.OpenQuestBook();

        if (Input.GetKeyDown(KeyCode.Slash) && Input.GetKey(KeyCode.LeftShift))
            ToggleObject();
    }

    void ToggleObject()
    {
        if (targetObject == null) return;
        targetObject.SetActive(!targetObject.activeSelf);
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        Vector3 velocity = new Vector3(
            move.x * moveSpeed,
            rb.linearVelocity.y,
            move.z * moveSpeed
        );

        rb.linearVelocity = velocity;
    }

    void StopMovement()
    {
        // Prevent sliding during dialogue
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);

        animator.SetBool("Walking", false);
        animator.SetBool("Running", false);
    }

    void HandleAnimations()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        animator.SetBool("Walking", isMoving && !isRunning);
        animator.SetBool("Running", isRunning);
    }

    void Jump()
    {
        isGrounded = false;

        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBarScript != null)
            healthBarScript.setHealth(currentHealth);

        ToastNotification.Show("Your emotions were hurt! (-" + damage + " HP)");

        if (currentHealth <= 0)
        {
            Debug.Log("Player emotionally overwhelmed.");
            // Game over logic here
        }
    }
}