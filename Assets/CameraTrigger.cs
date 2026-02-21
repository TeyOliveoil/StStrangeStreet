using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private bool isMainCamera = false;
    private GameObject cameraObject;
    CameraManager camManager;

    private void Awake()
    {
        cameraObject = GetComponentInChildren<Camera>().gameObject;
        camManager = FindAnyObjectByType<CameraManager>();

        if (isMainCamera)
        {
            cameraObject.SetActive(true);
            camManager.LatestCameraObject = cameraObject;
        } else
        {
            cameraObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider trigger)
    { 
        GameObject triggerObject = trigger.gameObject;
        Debug.Log("camera trigged");

        if (triggerObject.layer == LayerMask.NameToLayer("Player"))
        {
            //activate current camera
            cameraObject.SetActive(true);
            //deactivate latest camera
            camManager.LatestCameraObject.SetActive(false);
            //update latest camera with this one
            camManager.LatestCameraObject = cameraObject;


        }
    }
}
