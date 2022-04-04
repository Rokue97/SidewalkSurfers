using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPeople : MonoBehaviour
{
    [SerializeField] List<Mesh> people;
    [SerializeField] Rigidbody rb;
    [SerializeField] int speed;
    public int direction = 0;
    public int code;
    [SerializeField] bool standing = false;

    // Start is called before the first frame update
    void Start()
    {
        int obstacleID = Random.Range(0, people.Count);
        GetComponent<SkinnedMeshRenderer>().sharedMesh = people[obstacleID];

        if (!standing)
        {
            if (direction == 1)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (!standing)
        {
            if (transform.position.z - GameObject.FindWithTag("Player").transform.position.z < 10)
            {
                if (direction == 0)
                    rb.velocity = new Vector3(0, 0, speed);
                else
                    rb.velocity = new Vector3(0, 0, -speed);
            }
        }       
    }
}
