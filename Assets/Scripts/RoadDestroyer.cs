using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDestroyer : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Road" || other.tag == "Object" || other.tag == "Building" || other.tag == "Coin")
        {            
            Destroy(other.gameObject);
        }
    }
}
