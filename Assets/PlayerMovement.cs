using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    [SerializeField] private float moveSpeed;
    private bool updateRotation = false;
    [SerializeField] private float rotateAmount = 100f;

    [Tooltip("Smoothing time for rotation")]
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    private CharacterController charController;
    [SerializeField] private Animator charAnimator;
    [SerializeField] private Animator charHeadAnimator;
    private InteractableManager interactableManager;

    public enum State { wandering,inspecting}
    public State state = State.wandering;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        interactableManager = FindAnyObjectByType<InteractableManager>();
    }

    private void Update()
    {
        if (updateRotation) //of player
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

        //update rotation of object
        if (state == State.inspecting)
        {
            RotateObject();
            //check if door is on obj raycast
            //show text to enter door?
        }

    }
    private void RotateObject()
    {
        Vector3 _rotateOutput = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _rotateOutput = Vector3.down;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _rotateOutput = Vector3.up;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _rotateOutput = Vector3.left;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _rotateOutput = Vector3.right;
        }

        //apply rotation
        Transform ObjToRotate = interactableManager.currentRotatable.transform;
        ObjToRotate.RotateAround(ObjToRotate.position, _rotateOutput, rotateAmount * Time.deltaTime);
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (state == State.wandering)
        {
            updateRotation = true;
            _moveInput = context.ReadValue<Vector2>();
            _moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
            charAnimator.SetBool("isWalking", true);
            charHeadAnimator.SetBool("isWalking", true);

            //Debug.Log(_moveInput);
            if (context.canceled)
            {
                charAnimator.SetBool("isWalking", false);
                charHeadAnimator.SetBool("isWalking", false);
                updateRotation = false;
                //Debug.Log("stopped walking");
            }
        }        
    }

    public void Jump()
    {
        Debug.Log("jumping!");
    }

    

}
