using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Doors;
    [SerializeField] public int CurrentDoor = 0;

    public void NewDay()
    {
        //increase currentdoor counter
        CurrentDoor++;
        Debug.Log(CurrentDoor);
    }
}
