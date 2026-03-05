using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableManager : MonoBehaviour
{

    //[SerializeField] private LayerMask triggerLayer;
    private InteractableTrigger currentInteractable;

    [SerializeField] private Animator charAnimator;
    [SerializeField] private Animator charHeadAnimator;
    private bool isActive = false;
    private bool pickedUp = false;

    public GameObject currentRotatable;
    //[SerializeField] private GameObject playerVisual;

    private void OnTriggerEnter(Collider trigger)
    {
        //Debug.Log("triggered");
        GameObject triggerObject = trigger.gameObject;
        
        if (triggerObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            //Debug.Log("saved interactable");
            //save interactable
            currentInteractable = triggerObject.GetComponent<InteractableTrigger>();
        }
    }

    private void OnTriggerExit(Collider trigger)
    {
        //reset nearby interactable
        
        currentInteractable = null;
        isActive = false;
        //Debug.Log("saved interactable reset");
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (currentInteractable != null) //if something nearby
        {
            if (context.started) //trigger interaction
            {
                if (!isActive && !pickedUp) //at start of interaction with object
                {
                    Debug.Log("interacting");
                    //activate
                    isActive = true;
                    currentInteractable.Trigger();
                    
                    return;
                } 
                //add a case for if there's nothing to pick up!
                if (isActive && !pickedUp) //if interacting, but not picked up yet
                {
                    //pick up object
                    Debug.Log("pick up object here");
                    pickedUp = true;
                    currentInteractable.PickUpObject();
                    
                    return;
                }
                if (isActive && pickedUp) //if picked up object, now leaving or entering door (leaving should be own button - escape?)
                {
                    Debug.Log("deactivated interacting");
                    //deactivate
                    isActive = false;
                    pickedUp = false;
                    currentInteractable.Deactivate();
                    
                }
            }
            
            
        } else //if nothing nearby
        {
            charHeadAnimator.SetTrigger("LookAround");
            Debug.Log("nothing nearby to interact with");
        }
        
        
    }

   
}
