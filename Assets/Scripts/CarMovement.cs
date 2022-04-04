using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] GameObject wheel1, wheel2, wheel3, wheel4;
    [SerializeField] int turnSpeed, speed;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);

        wheel1.transform.Rotate(new Vector3(turnSpeed * Time.deltaTime, 0, 0), Space.Self);
        wheel2.transform.Rotate(new Vector3(turnSpeed * Time.deltaTime, 0, 0), Space.Self);
        wheel3.transform.Rotate(new Vector3(turnSpeed * Time.deltaTime, 0, 0), Space.Self);
        wheel4.transform.Rotate(new Vector3(turnSpeed * Time.deltaTime, 0, 0), Space.Self);
    }
}
