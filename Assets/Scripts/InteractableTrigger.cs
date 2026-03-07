using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider))]
public class InteractableTrigger : MonoBehaviour
{
    [SerializeField] private bool isBed;
    [SerializeField] public bool cutable;
    [SerializeField] public bool onlyAnimation;
    private bool animationActive = false;
    [SerializeField] private string wanderText;
    [SerializeField] private string inspectText;
    private TMP_Text textSpace;
    [SerializeField] public InventoryItem pickUp;
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
        textSpace = GameObject.FindWithTag("TextSpace").GetComponent<TMP_Text>();
        Debug.Log(textSpace.text);
        animator = GetComponentInChildren<Animator>();
        
        //newViewpoint = GetComponentInChildren<Camera>().gameObject;
        if (newViewpoint != null)
        {
            newViewpoint.SetActive(false);
        }
        
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        interactableManager = FindAnyObjectByType<InteractableManager>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void showWanderText()
    {
        textSpace.text = wanderText;
    }
    public void showInspectText()
    {
        textSpace.text = inspectText;
    }

    public void resetText()
    {
        textSpace.text = "";
    }

    public void Trigger()
    {
        //limit player movement!
        playerMovement.state = PlayerMovement.State.looking;

        //if there is an animation here then
        if (animator != null)
        {
            //play animation, use trigger "StartInteraction" + "EndInteraction" 
            animator.SetTrigger("StartInteraction");
            //SetInteractableView is called in the actual animation timeline itself so the timing fits :-)

            if (isBed)
            {
                gameManager.NewDay(); //increase day, might not be necessary!!
            }
        }
        else
        {
            SetInteractableView();
        }
    }

    public void TriggerAnimation()
    {
        if (!animationActive) //if animation is not "active"
        {
            animator.SetTrigger("StartInteraction"); //start animation
        }
        else
        {
            animator.SetTrigger("EndInteraction"); //end animation
        }
        animationActive = !animationActive;
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


    public void PutDownObject()
    {
        rotatable.transform.position = idleTransform.position;
        hands.SetActive(false);
        resetText();
    }

    public void Deactivate()
    {
        Debug.Log("deactivating interaction");
        
        //reset view
        playerVisual.SetActive(true);
        newViewpoint.SetActive(false);
        showWanderText();

        //reset animation
        if (animator != null)
        {
            animator.SetTrigger("EndInteraction");
        }
    }

}
