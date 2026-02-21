using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    [SerializeField] private float moveSpeed;
    private bool updateRotation = false;

    [Tooltip("Smoothing time for rotation")]
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    private CharacterController charController;
    [SerializeField] private Animator charAnimator;
    [SerializeField] private Animator charHeadAnimator;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (updateRotation)
        {
            //update target angle to movement direction
            var targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg;
            //smooth
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
            //apply angle to p objects
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        //update p position using movement direction + speed
        charController.Move(_moveDirection * moveSpeed * Time.deltaTime);

    }

    public void Move(InputAction.CallbackContext context)
    {
        updateRotation = true;
        _moveInput = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
        charAnimator.SetBool("isWalking", true);
        charHeadAnimator.SetBool("isWalking", true);

        Debug.Log(_moveInput);
        if (context.canceled)
        {
            charAnimator.SetBool("isWalking", false);
            charHeadAnimator.SetBool("isWalking", false);
            updateRotation = false;
            Debug.Log("stopped walking");
        }
    }

    public void Jump()
    {
        Debug.Log("jumping!");
    }

    public void Interact(InputAction.CallbackContext context)
    {
        charHeadAnimator.SetTrigger("LookAround");
        Debug.Log("interacting");
    }

}
