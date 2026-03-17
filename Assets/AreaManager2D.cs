using UnityEngine;

public class AreaManager2D : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    private Vector2 PlayerScreenPosition;
    private Vector2 PlayerScaleMinMax;

    private Camera cam;
    private float screenX1, screenX2, screenY1, screeny2;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }


    public void InitArea(Vector2 NewMinMax)
    {
        //save new scale min max
        PlayerScaleMinMax = NewMinMax;

        //update player scale to new min 
        //should not work like this in the end!!!!!!!!!!!!!!!
        PlayerTransform.localScale = new Vector3(PlayerScaleMinMax.x, PlayerScaleMinMax.x, PlayerScaleMinMax.x);
    }

    //called if player moves, called from player movement 2D
    public void ScalePlayer2D()
    {
        //player screen position
        PlayerScreenPosition = cam.WorldToScreenPoint(PlayerTransform.position);
        Debug.Log(PlayerScreenPosition);

        //where on the screen, lerp percentage of screen y with two scale values
        //scale player
    }
}
