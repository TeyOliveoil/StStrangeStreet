using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Doors;
    [SerializeField] public int CurrentDoor = 0;
    private Camera currentViewpoint;

    public void NewDay()
    {
        //increase currentdoor counter
        CurrentDoor++;
        Debug.Log(CurrentDoor);
    }

    public void CheckDoor()
    {
        Transform currentViewTransform = currentViewpoint.gameObject.transform;
        //raycast to check if door please
        //Ray ray = new Ray(currentViewTransform.position, currentViewTransform.forward);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * 10);
        //show door hint text
    }
}
