using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> cars;
    [SerializeField] Transform carSpawnLoc, carSpawnLoc2;
    [SerializeField] GameObject carParent;
    [SerializeField] List<Material> carColors;

    public bool isGameStarted = false;
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(CarSpawnerController());
        StartCoroutine(CarSpawnerController2());
    }

    void CarSpawner()
    {
        int itemNo = Random.Range(0, cars.Count);
        int colorNo = Random.Range(0, carColors.Count);
        GameObject car = Instantiate(cars[itemNo], new Vector3(carSpawnLoc.transform.position.x, 0.091f, carSpawnLoc.transform.position.z), Quaternion.identity);
        if (itemNo > 1)
            car.GetComponent<MeshRenderer>().material = carColors[colorNo];
        car.transform.SetParent(carParent.transform);
        StartCoroutine(CarSpawnerController());
    }

    void CarSpawner2()
    {
        int itemNo = Random.Range(0, cars.Count);
        int colorNo = Random.Range(0, carColors.Count);
        GameObject car = Instantiate(cars[itemNo], new Vector3(carSpawnLoc2.transform.position.x, 0.091f, carSpawnLoc2.transform.position.z), Quaternion.identity);
        if (itemNo > 1)
            car.GetComponent<MeshRenderer>().material = carColors[colorNo];
        car.transform.SetParent(carParent.transform);
        StartCoroutine(CarSpawnerController2());
    }

    IEnumerator CarSpawnerController()
    {
        int waitingTime = Random.Range(5, 9);
        yield return new WaitForSeconds(waitingTime);
        if (!isGameStarted)
            CarSpawner();
    }

    IEnumerator CarSpawnerController2()
    {
        int waitingTime = Random.Range(5, 9);
        yield return new WaitForSeconds(waitingTime);
        if(!isGameStarted)
            CarSpawner2();
    }
}
