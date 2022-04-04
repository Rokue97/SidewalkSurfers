using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { set; get; }

    private const float distanceBeforeSpawn = 150;
    private const int initialSegments = 20;
    private Transform cameraContanier;
    public float currentSpawnZ;

    [SerializeField] GameObject roadParent, obstacleParent, buildingsParent, coinParent, people, peopleCarParent;
    [SerializeField] List<GameObject> segments = new List<GameObject>();
    [SerializeField] List<GameObject> buildings = new List<GameObject>();
    [SerializeField] float currentSpawnZForBuilding;
    [SerializeField] GameObject road;
    [SerializeField] List<GameObject> coins = new List<GameObject>();
    public List<bool> locs; 
    [SerializeField] float currentSpawnZForRoads = 0;
    [SerializeField] Transform lastBuildingPos; 
    float distnaceBeforeSpawnForObstacle = 70;

    int segmentCount = 0;
    int obstacleID;
    int holder = 0;
    int loc;

    Player player;

    public List<GameObject> parts = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        cameraContanier = Camera.main.transform;
        currentSpawnZ = 0;
        currentSpawnZForBuilding = -20;
        currentSpawnZForRoads = -10;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Start()
    {
        for (int i = 0; i < initialSegments; i++)
        {
            AddBuildings();
            SpawnRoad();
        }
    }

    private void Update()
    {
        if (currentSpawnZ - cameraContanier.position.z < distnaceBeforeSpawnForObstacle)        
            if (player.isGameStarted)
                SpawnObstacles();       

        if (currentSpawnZForBuilding - cameraContanier.position.z < distanceBeforeSpawn + 15)        
              if (player.isGameStarted)                                     
                AddBuildings();    
        
        if (currentSpawnZForRoads - cameraContanier.position.z < distanceBeforeSpawn)        
            if (player.isGameStarted)            
                SpawnRoad();                         
    } 

    void SpawnObstacles()
    {
        segmentCount++;
        obstacleID = Random.Range(0, segments.Count);
        if(holder == obstacleID)
            obstacleID = Random.Range(0, segments.Count);
        holder = obstacleID;
        GameObject obstacle = Instantiate(segments[obstacleID], new Vector3(segments[obstacleID].transform.position.x, segments[obstacleID].transform.position.y, currentSpawnZ), Quaternion.identity) as GameObject;
        currentSpawnZ += segments[obstacleID].GetComponent<Segment>().lenght * (player.runSpeed / 7.5f);
        obstacle.transform.SetParent(obstacleParent.transform);

        
        int coinRandom = Random.Range(0, 7);
        if(coinRandom == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                parts.Add(segments[obstacleID].transform.GetChild(i).gameObject);
            }

            int coinPos = Random.Range(0, 3);
            while (parts[coinPos].tag == "High" || parts[coinPos].tag == "People")
            {
                coinPos = Random.Range(0, 3);
            }

            int coinPathNumber = 0;
            if (parts[coinPos].tag == "Jump")
                coinPathNumber = 1;
            else if (parts[coinPos].tag == "Slide")
                coinPathNumber = Random.Range(0, 2);

            GameObject coinPath = Instantiate(coins[coinPathNumber], new Vector3(parts[coinPos].transform.position.x, 0, obstacle.transform.position.z), Quaternion.identity) as GameObject;
            coinPath.transform.SetParent(coinParent.transform);
            parts.Clear();
        }                 
        
    }

    public void SpawnRoad()
    {
        GameObject roads = Instantiate(road, new Vector3(road.transform.position.x, road.transform.position.y, currentSpawnZForRoads), Quaternion.identity) as GameObject;
        currentSpawnZForRoads += 7;
        roads.transform.SetParent(roadParent.transform);
    }

    public void AddBuildings()
    {
        int r = Random.Range(0, buildings.Count);
        GameObject building = Instantiate(buildings[r], new Vector3(buildings[r].GetComponent<Buildings>().buildingX, 0.1f, currentSpawnZForBuilding + buildings[r].GetComponent<Buildings>().lenght), Quaternion.identity);
        building.transform.rotation = Quaternion.Euler(building.transform.rotation.x, buildings[r].GetComponent<Buildings>().rotationY, building.transform.rotation.z);
        currentSpawnZForBuilding += buildings[r].GetComponent<Buildings>().lenght;
        building.transform.SetParent(buildingsParent.transform);
    }

    public void ResetGame(bool playAgain)
    {
        currentSpawnZForRoads = player.transform.position.z - 10;
        currentSpawnZForBuilding = player.transform.position.z - 20;
        if(playAgain)
            currentSpawnZ = player.transform.position.z + 40;
        else
            currentSpawnZ = player.transform.position.z + 20;

        for(int i = 0; i < obstacleParent.transform.childCount; i++)
        {
            Destroy(obstacleParent.transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < roadParent.transform.childCount; i++)
        {
            Destroy(roadParent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < buildingsParent.transform.childCount; i++)
        {
            Destroy(buildingsParent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < coinParent.transform.childCount; i++)
        {
            Destroy(coinParent.transform.GetChild(i).gameObject);
        }

        MainMenuReset();
    }

    public void ResumeGame()
    {
        for (int i = 0; i < 3; i++)
        {
            Destroy(obstacleParent.transform.GetChild(i).gameObject);
        }
    }

    public void MainMenuReset()
    {
        for (int i = 0; i < initialSegments; i++)
        {
            AddBuildings();
            SpawnRoad();
        }
    }
}
