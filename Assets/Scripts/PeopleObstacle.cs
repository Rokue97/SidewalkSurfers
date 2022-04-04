using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleObstacle : MonoBehaviour
{
    [SerializeField] List<Mesh> people;
    [SerializeField] int speed;
    [SerializeField] Rigidbody rb;
    Player player;

    int direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        int obstacleID = Random.Range(0, people.Count);
        direction = Random.Range(0, 2);
        GetComponent<SkinnedMeshRenderer>().sharedMesh = people[obstacleID];

        if(direction == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z - GameObject.FindWithTag("Player").transform.position.z < 10 && player.isGameStarted)
        {
            if(direction == 0)
                rb.velocity = new Vector3(0, 0, speed);
            else
                rb.velocity = new Vector3(0, 0, -speed);
        }            
    }
}
