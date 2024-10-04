using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 0.25f;

    private Rigidbody2D _rb;
    private Vector2 _movementInput;
    private IInteractable _interactable;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
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
            _interactable = interactable;
        
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
        SetAnimation(_movementInput);
        _rb.velocity = _movementInput * _moveSpeed;
    }

    private void SetAnimation(Vector2 direction)
    {
        if (direction == Vector2.zero)
            _animator.Play("idle");
        else if (direction.x > 0)
            _animator.Play("Walk_right");
        else if (direction.x < 0)
            _animator.Play("Walk_left");
        else if (direction.y < 0)
            _animator.Play("Walk_down");
        else if (direction.y > 0)
            _animator.Play("Walk_up");
    }
}