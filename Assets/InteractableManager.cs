using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableManager : MonoBehaviour
{

    [SerializeField] private LayerMask triggerLayer;
    private InteractableTrigger currentInteractable;

    [SerializeField] private Animator charAnimator;
    [SerializeField] private Animator charHeadAnimator;
    private bool isActive = false;

    void Awake()
    {
        
    }

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
            if (context.started)
            {
                Debug.Log("start?");
                //trigger interaction
                if (!isActive)
                {
                    Debug.Log("interacting");
                    //activate
                    isActive = true;
                    currentInteractable.Trigger();
                    return;
                } else
                {
                    Debug.Log("deactivated interacting");
                    //deactivate
                    isActive = false;
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
