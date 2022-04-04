using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceJump : MonoBehaviour
{
    [SerializeField] List<GameObject> visuals = new List<GameObject>();
    [SerializeField] GameObject firstCollider, secondCollider;

    private void Start()
    {
        int obstacleVisualID = Random.Range(0, visuals.Count);
        if(obstacleVisualID == 2 || obstacleVisualID == 3)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            firstCollider.SetActive(false);
            secondCollider.SetActive(true);
            GetComponent<MeshFilter>().mesh = visuals[obstacleVisualID].GetComponent<MeshFilter>().sharedMesh;
        }
        else
        {
            firstCollider.SetActive(true);
            secondCollider.SetActive(false);
            GetComponent<MeshFilter>().mesh = visuals[obstacleVisualID].GetComponent<MeshFilter>().sharedMesh;
        }
        
    }
}
