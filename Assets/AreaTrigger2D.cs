using UnityEngine;

public class AreaTrigger2D : MonoBehaviour
{

    [SerializeField] private Vector3 CamPosition;

    [SerializeField] private Vector2 PlayerScaleMinMax;
    [SerializeField] private Vector2 PlayerSpeedMaxMin;
    [SerializeField] private float offset;

    private bool isActive = false;
  
    private void OnTriggerEnter(Collider player)
    {
        Debug.Log("trigger");
        if (!isActive)
        {
            player.GetComponent<AreaManager2D>().InitArea(PlayerScaleMinMax, PlayerSpeedMaxMin, CamPosition, offset);
            isActive = true;
        }
    }

}
