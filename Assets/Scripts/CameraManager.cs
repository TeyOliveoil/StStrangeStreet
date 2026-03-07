using UnityEngine;
using UnityEngine.Rendering.Universal;  

public class CameraManager : MonoBehaviour
{
    public Camera CurrentCamera;
    public GameObject LatestCameraObject;
    [SerializeField] private Camera InventoryCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Camera[] cameras = GetComponents<Camera>(); //find in whole scene!!!!
        //Camera[] cameras =  Object.FindObjectsByType<Camera>();
        Debug.Log(cameras.Length);

        foreach (Camera cam in cameras)
        {
            var cameraData = cam.GetUniversalAdditionalCameraData();
            if(cameraData.renderType != CameraRenderType.Overlay)
            {
                cameraData.cameraStack.Add(InventoryCamera);
            }
        }

        InventoryCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
