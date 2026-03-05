using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    [SerializeField] private bool isBed;
    [SerializeField] private string text;
    [SerializeField] private GameObject rotatable;
    [SerializeField] private Transform idleTransform;
    [SerializeField] private Transform activeTransform;
    [SerializeField] private GameObject hands;

    private Animator animator;
    [SerializeField] private GameObject newViewpoint;
    private PlayerMovement playerMovement;
    [SerializeField] private GameObject playerVisual;
    private InteractableManager interactableManager;
    private GameManager gameManager;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
        //newViewpoint = GetComponentInChildren<Camera>().gameObject;
        newViewpoint.SetActive(false);
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        interactableManager = FindAnyObjectByType<InteractableManager>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void Trigger()
    {
        //Debug.Log("interaction triggered");
        playerMovement.state = PlayerMovement.State.looking;
        
        if (animator != null)
        {
            //play animation, use trigger "StartInteraction" + "EndInteraction"
            animator.SetTrigger("StartInteraction");

            if (isBed)
            {
                gameManager.NewDay();
            }
        } else
        {
            SetInteractableView();
        }

        
    }

    public void SetInteractableView()
    {
        Debug.Log("setting interaction view");

        //change gamemode to inspecting object
        playerMovement.state = PlayerMovement.State.looking;
        //update viewpoint
        gameManager.currentViewpoint = newViewpoint.GetComponent<Camera>();
        //hide player
        playerVisual.SetActive(false);

        if (newViewpoint != null)
        {
            newViewpoint.SetActive(true);
        }
    }

    public void PickUpObject()
    {
        //move rotatable to active position
        rotatable.transform.position = activeTransform.position;
        //pass rotatable to interaction manager
        interactableManager.currentRotatable = rotatable;
        //activate hands ui element
        hands.SetActive(true);

        playerMovement.state = PlayerMovement.State.inspecting;
        //add case for if nothing to pick up!?
    }

    public void Deactivate()
    {
        Debug.Log("deactivating interaction");
        playerMovement.state = PlayerMovement.State.wandering;
        rotatable.transform.position = idleTransform.position;
        //reset view
        hands.SetActive(false);
        playerVisual.SetActive(true);
        newViewpoint.SetActive(false);
        interactableManager.currentRotatable = null;

        //reset animation
        if (animator != null)
        {
            animator.SetTrigger("EndInteraction");
        }
    }

}
