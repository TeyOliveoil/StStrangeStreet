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
    private AreaManager2D areaManager2D;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        gameManager = FindAnyObjectByType<GameManager>();
        areaManager2D = FindAnyObjectByType<AreaManager2D>();

    }

    private void Update()
    {
        //update p position using movement direction + speed
        charController.Move(_moveDirection * moveSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        
        _moveInput = context.ReadValue<Vector2>();
        _moveDirection = _moveInput;

        if(context.started){
            charAnimator.SetBool("isWalking", true);
            charHeadAnimator.SetBool("isWalking", true);
        }

        if (context.canceled)
        {
            charAnimator.SetBool("isWalking", false);
            charHeadAnimator.SetBool("isWalking", false);
        }

        areaManager2D.ScalePlayer2D();


    }
}
