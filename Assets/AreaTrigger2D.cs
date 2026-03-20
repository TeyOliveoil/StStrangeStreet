using UnityEngine;

public class AreaTrigger2D : MonoBehaviour
{

    [SerializeField] private Vector3 CamPosition;
    [SerializeField] private float camOrthoSize;

    [SerializeField] private Vector2 PlayerScaleMinMax;
    [SerializeField] private Vector2 PlayerSpeedMaxMin;
    [SerializeField] private float heightOffset;
    [SerializeField] private float Z_Offset;
   
    private bool isActive = false;
  
    private void OnTriggerEnter(Collider player)
    {
        Debug.Log("trigger");
        if (!isActive)
        {
            //Vector3 newPlayerZ = new Vector3(player.transform.position.x, player.transform.position.y, PlayerZ);
            //player.transform.position = newPlayerZ;
            player.GetComponent<AreaManager2D>().InitArea(PlayerScaleMinMax, PlayerSpeedMaxMin, CamPosition, camOrthoSize, heightOffset, Z_Offset);
            
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        isActive = false;
    }

}
