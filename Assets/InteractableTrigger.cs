using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    [SerializeField] private bool isBed;
    [SerializeField] private string text;
    [SerializeField] private GameObject rotatable;
    [SerializeField] private Transform idleTransform;
    [SerializeField] private Transform activeTransform;

    private Animator animator;
    private GameObject newViewpoint;
    private PlayerMovement playerMovement;
    private InteractableManager interactableManager;
    private GameManager gameManager;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        newViewpoint = GetComponentInChildren<Camera>().gameObject;
        newViewpoint.SetActive(false);
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        interactableManager = FindAnyObjectByType<InteractableManager>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void Trigger()
    {
        Debug.Log("interaction triggered");
        playerMovement.state = PlayerMovement.State.inspecting;

        if (animator != null)
        {
            //play animation, use trigger
        }

        if (newViewpoint != null)
        {
            newViewpoint.SetActive(true);
            
        }

        if (rotatable != null)
        {
            rotatable.transform.position = activeTransform.position;
            //pass rotatable to int manager
            interactableManager.currentRotatable = rotatable;
        }

        if (isBed)
        {
            gameManager.NewDay();
        }
    }

    public void Deactivate()
    {
        playerMovement.state = PlayerMovement.State.wandering;
        rotatable.transform.position = idleTransform.position;
        newViewpoint.SetActive(false);
    }

}
