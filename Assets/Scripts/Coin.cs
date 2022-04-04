using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator anim;
    GameManager gameManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * 200));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameManager.GetCoin();
            anim.SetTrigger("Collected");
            Destroy(transform.parent.gameObject, 0.5f);
        }

    }
}
