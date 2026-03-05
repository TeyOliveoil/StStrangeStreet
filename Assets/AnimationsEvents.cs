using UnityEngine;

public class AnimationsEvents : MonoBehaviour
{
    [SerializeField] private InteractableTrigger interactableTrigger;

    
    public void StartInteraction()
    {
        Debug.Log("animation trigger");
        interactableTrigger.SetInteractableView();
    }
}
