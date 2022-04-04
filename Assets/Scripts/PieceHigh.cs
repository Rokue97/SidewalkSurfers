using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHigh : MonoBehaviour
{
    [SerializeField] List<GameObject> visuals = new List<GameObject>();
    [SerializeField] GameObject firstCollider, secondCollider, thirdCollider;

    private void Start()
    {
        int obstacleVisualID = Random.Range(0, visuals.Count);

        if(obstacleVisualID == 2 || obstacleVisualID == 3 || obstacleVisualID == 4)
        {
            firstCollider.SetActive(false);
            secondCollider.SetActive(true);
            thirdCollider.SetActive(false);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            GetComponent<MeshFilter>().mesh = visuals[obstacleVisualID].GetComponent<MeshFilter>().sharedMesh;
        }
        else if(obstacleVisualID == 0)
        {
            firstCollider.SetActive(false);
            secondCollider.SetActive(false);
            thirdCollider.SetActive(true);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);
            GetComponent<MeshFilter>().mesh = visuals[obstacleVisualID].GetComponent<MeshFilter>().sharedMesh;
        }
        else
        {
            firstCollider.SetActive(true);
            secondCollider.SetActive(false);
            thirdCollider.SetActive(false);
            GetComponent<MeshFilter>().mesh = visuals[obstacleVisualID].GetComponent<MeshFilter>().sharedMesh;
        }
        
    }
}
