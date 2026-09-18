using UnityEngine;
using System.Collections;

public class HazardDamage : MonoBehaviour
{


    void OnCollisionEnter(Collision other)
    {
        Debug.Log("A collider has made contact with the DoorObject Collider");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("A trigger has made contact with the DoorObject Collider");
    }
}
