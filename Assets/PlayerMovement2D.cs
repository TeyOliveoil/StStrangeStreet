using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : MonoBehaviour
{
    private Vector2 _moveInput;
    private Vector2 _moveDirection;
    [SerializeField] private float moveSpeed;

    private CharacterController charController;
    [SerializeField] private Animator charAnimator;
    [SerializeField] private Animator charHeadAnimator;
    private GameManager gameManager;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        gameManager = FindAnyObjectByType<GameManager>();

    }

    private void Update()
    {
        //update p position using movement direction + speed
        charController.Move(_moveDirection * moveSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        
        _moveInput = context.ReadValue<Vector2>();
        //_moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y); 
        _moveDirection = _moveInput;

        charAnimator.SetBool("isWalking", true);
        charHeadAnimator.SetBool("isWalking", true);

        //Debug.Log(_moveInput);
        if (context.canceled)
        {
            charAnimator.SetBool("isWalking", false);
            charHeadAnimator.SetBool("isWalking", false);
            //Debug.Log("stopped walking");
        }
        
    }
}
