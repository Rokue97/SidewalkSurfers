using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuElementDestroyer : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
 
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {       
        if(other.tag == "Car")
        {
            Destroy(other.gameObject);
        }
    }
}
