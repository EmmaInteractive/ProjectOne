using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 0.25f;

    private Rigidbody2D _rb;
    private Vector2 _movementInput;
    private IInteractable _interactable;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;
        _rb.interpolation = RigidbodyInterpolation2D.None;
        
    }

    void Update()
    {
        HandleMovementInput();

        if (Input.GetKeyDown("e"))
            Interact();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponentInParent<IInteractable>();
        if (interactable is IInteractable) 
        {
            _interactable = interactable;
            Debug.Log("Can interact!");
        } else
        {
            Debug.Log("No. :C");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactable = collision.GetComponentInParent<IInteractable>();
        if (interactable is IInteractable)
        {
            _interactable = null;
        }
    }

    private void Interact() => _interactable?.Interact();

    private void HandleMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _movementInput = new Vector2(moveX, moveY).normalized;
    }

    private void MovePlayer()
    {
        _rb.velocity = _movementInput * _moveSpeed;
    }
}