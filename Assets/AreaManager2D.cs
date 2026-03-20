using UnityEngine;

public class AreaManager2D : MonoBehaviour
{
    private Vector2 playerScreenPosition;
    private Vector2 playerScaleMaxMin;
    private Vector2 playerSpeedMaxMin;
    private float offset; //used for scaling, offset from top of screen (accounts for dead space where the player should not be scaled)
    private Rigidbody rb;

    [SerializeField] private Camera cam;
    private PlayerMovement2D playerMovement;
    void Awake()
    {
        //cam = GetComponent<Camera>();'
        playerMovement = GetComponent<PlayerMovement2D>();
        rb = GetComponent<Rigidbody>();
    }


    public void InitArea(Vector2 newScaleMinMax, Vector2 newSpeedMinMax, Vector3 newCamPos, float newOrthoSize, float newOffset, float newZ)
    {
        //save new scale min max
        playerScaleMaxMin = newScaleMinMax;
        playerSpeedMaxMin = newSpeedMinMax;
        offset = newOffset;
        
        //update camera position
        if (cam != null)
        {
            cam.transform.position = newCamPos;
            cam.orthographicSize = newOrthoSize;
        }
        
        //update player scale to new min
        transform.localScale = new Vector3(playerScaleMaxMin.x, playerScaleMaxMin.x, playerScaleMaxMin.x);
        //update player position (Z)
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, newZ);
        //rb.Sleep();
        rb.position = newPos;
        //rb.WakeUp();
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
