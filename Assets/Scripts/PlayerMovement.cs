using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxHealth = 20;
    [SerializeField] private GameObject targetObject;

    private Rigidbody rb;
    private Animator animator;

    private bool isGrounded;

    public HealthBarScript healthBarScript;
    public int currentHealth;
    public QuestBookBehaviour questBookBehaviour;

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;

        ToastNotification.Show("Hey, Civic Guardian!");
        ToastNotification.Show("Your time is ticking!");
        ToastNotification.Show("Meet, the police officer to know more");
    }

    void Update()
    {
        MovePlayer();
        HandleAnimations();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            questBookBehaviour.OpenQuestBook();
        }

        if (Input.GetKeyDown(KeyCode.Slash) && Input.GetKey(KeyCode.LeftShift))
        {
            ToggleObject();
        }
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

    void HandleAnimations()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isMoving = (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f);
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

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarScript.setHealth(currentHealth);
        ToastNotification.Show("Oops! You took in some damage");
    }
}