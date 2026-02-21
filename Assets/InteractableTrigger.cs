using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    [SerializeField] private string text;
    private Animator animator;
    private Camera mainCamera;
    private Camera newViewpoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        newViewpoint = GetComponentInChildren<Camera>();
        mainCamera = Camera.main;
    }

    public void Trigger()
    {
        Debug.Log("interaction triggered");

        if (animator != null)
        {
            //play animation, use trigger
        }

        if (newViewpoint != null)
        {
            
        }
    }

    public void Deactivate()
    {

    }
}
