using UnityEditor.Rendering.Analytics;
using UnityEngine;

public class PlayerMovement_BulletHell : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float life = 10;

    [SerializeField] private float playerSpeed = 10;
    private Vector3 playerInput;

    [Header("Animation")]
    [SerializeField] private Animator charAnimator;
    [SerializeField] private Animator charHeadAnimator;
    private bool isWalking = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //update player input
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = 0;
        playerInput.z = Input.GetAxis("Vertical");

        //update player position 
        transform.position += playerInput * Time.deltaTime * playerSpeed;
        
        AnimateCharacter();
    }

    public void Damage(float amount = 1)
    {
        life -= amount;
        if (life <= 0)
        {
            life = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("dead");
    }

    void AnimateCharacter()
    {
        Vector3 lookAtPos = transform.position + playerInput;
        transform.LookAt(lookAtPos);

        if (playerInput!=Vector3.zero)
        {
            charAnimator.SetBool("isWalking", true);
            charHeadAnimator.SetBool("isWalking", true);
        } else 
        {
            charAnimator.SetBool("isWalking", false);
            charHeadAnimator.SetBool("isWalking", false);
        }
    }
}
