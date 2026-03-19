using UnityEngine;

public class AreaManager2D : MonoBehaviour
{
    private Vector2 playerScreenPosition;
    private Vector2 playerScaleMaxMin;
    private Vector2 playerSpeedMaxMin;
    private float offset;

    [SerializeField] private Camera cam;
    private PlayerMovement2D playerMovement;
    void Awake()
    {
        //cam = GetComponent<Camera>();'
        playerMovement = GetComponent<PlayerMovement2D>();
    }


    public void InitArea(Vector2 newScaleMinMax, Vector2 newSpeedMinMax, Vector3 newCamPos, float newOffset)
    {
        //save new scale min max
        playerScaleMaxMin = newScaleMinMax;
        playerSpeedMaxMin = newSpeedMinMax;
        offset = newOffset;

        //update camera position
        if (cam != null)
        {
            cam.transform.position = newCamPos;
        }
        
        //update player scale to new min
        //should not work like this in the end!!!!!!!!!!!!!!!??????
        transform.localScale = new Vector3(playerScaleMaxMin.x, playerScaleMaxMin.x, playerScaleMaxMin.x);
    }

    //called if player moves, called from player movement 2D
    public void ScalePlayer2D()
    {
        //get player's position on screen, screen coordinates
        playerScreenPosition = cam.WorldToScreenPoint(transform.position);
        Debug.Log(playerScreenPosition);

        //figure how far up the screen player is (percentage)
        float playerScreenHeight = playerScreenPosition.y / (cam.pixelHeight-offset);

        //lerp percentage of screen y with two scale values
        float newPlayerScale = Mathf.SmoothStep(playerScaleMaxMin.x, playerScaleMaxMin.y, playerScreenHeight);
        float newPlayerSpeed = Mathf.SmoothStep(playerSpeedMaxMin.x, playerSpeedMaxMin.y, playerScreenHeight);

        //scale player
        transform.localScale = new Vector3(newPlayerScale, newPlayerScale, newPlayerScale);
        playerMovement.moveSpeed = newPlayerSpeed;

    }
}
