using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableManager : MonoBehaviour
{

    //[SerializeField] private LayerMask triggerLayer;
    private InteractableTrigger currentInteractable;
    private PlayerMovement playerMovement;
    private Inventory inventory;

    [SerializeField] private Animator charAnimator;
    [SerializeField] private Animator charHeadAnimator;
    //private bool isActive = false;
    //private bool pickedUp = false;

    public GameObject currentRotatable;
    //[SerializeField] private GameObject playerVisual;

    private void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        inventory = FindAnyObjectByType<Inventory>();
    }

    private void OnTriggerEnter(Collider trigger)
    {
        //Debug.Log("triggered");
        GameObject triggerObject = trigger.gameObject;
        
        if (triggerObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            Debug.Log("saved interactable");
            //save interactable
            currentInteractable = triggerObject.GetComponent<InteractableTrigger>();
            currentInteractable.showWanderText();
        }

    }

    private void OnTriggerExit(Collider trigger)
    {
        //reset nearby interactable
        if (currentInteractable != null)
        {
            currentInteractable.resetText();
        }
        currentInteractable = null;
        currentRotatable = null;
        //isActive = false;
        Debug.Log("reset saved interactable");
        
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (currentInteractable != null) //if something nearby
        {
            if (context.started) //trigger interaction
            {
                if (currentInteractable.onlyAnimation)
                {
                    currentInteractable.TriggerAnimation();
                } else
                {
                    if (playerMovement.state == PlayerMovement.State.wandering) //at start of interaction with object
                    {
                        //activate
                        currentInteractable.Trigger();
                        currentInteractable.resetText();
                        return;
                    }
                    //add a case for if there's nothing to pick up!
                    if (playerMovement.state == PlayerMovement.State.looking) //if interacting, but not picked up yet
                    {
                        //pick up object
                        currentInteractable.PickUpObject();
                        currentInteractable.showInspectText();
                        return;
                    }
                    if (playerMovement.state == PlayerMovement.State.inspecting) //if picked up object
                    {
                        //cut?
                        if (currentInteractable.cutable)
                        {
                            Debug.Log("cut!");
                        }
                        if (currentInteractable.pickUp!=null)
                        {
                            //add to inventory
                            inventory.AddItem(currentInteractable.pickUp);
                            Debug.Log("add to inventory");
                        }

                    }
                }
                
            }
            
            
        } else //if nothing nearby
        {
            charHeadAnimator.SetTrigger("LookAround");
            Debug.Log("nothing nearby to interact with");
        }
        
        
    }

    public void Back(InputAction.CallbackContext context)
    {
        if (currentInteractable != null) //if something nearby
        {
            if (context.started) //trigger interaction
            {
                if (!currentInteractable.onlyAnimation)
                {
                    if (playerMovement.state == PlayerMovement.State.looking)
                    {
                        currentInteractable.Deactivate();
                        playerMovement.state = PlayerMovement.State.wandering;

                    }
                    else if (playerMovement.state == PlayerMovement.State.inspecting)
                    {
                        currentInteractable.PutDownObject();
                        playerMovement.state = PlayerMovement.State.looking;
                        
                    }
                }
            }
        }
    }
}
