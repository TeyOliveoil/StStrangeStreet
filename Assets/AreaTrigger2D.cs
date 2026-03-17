using UnityEngine;

public class AreaTrigger2D : MonoBehaviour
{

    [SerializeField] private Vector3 CamPosition;

    [SerializeField] private Vector2 PlayerScaleMinMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        collision.GetComponent<AreaManager2D>().InitArea(PlayerScaleMinMax);
    }
}
