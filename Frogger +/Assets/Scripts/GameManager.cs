using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int BaseBoatSpeed = 10;
    [SerializeField] private int boatSpawnInterval = 10;
    [SerializeField] private int obstacleSpeed = 10;

    // Game objects to spawn & spawn points
    public GameObject playerPrefab;
    public GameObject boatPrefab;
    public GameObject[] obstaclePrefabs;
    public Transform[] boatSpawnRight;
    public Transform[] boatSpawnLeft;
    public Transform[] obstacleSpawnRight;
    public Transform[] obstacleSpawnLeft;
    public Transform playerSpawn;
    public GameObject player;
    public GameObject leftWall;
    public GameObject rightWall;
    private float timeSince = 0f;


    public static GameManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this; // setup singleton
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.Respawn)
        {
            Respawn(playerPrefab, playerSpawn);
        }

        if (InputManager.Instance.SpawnObstacles)
        {
            SpawnObstacles(obstaclePrefabs, obstacleSpawnRight, obstacleSpawnLeft, obstacleSpeed);
        }

        if (InputManager.Instance.SpawnBoat)
        {
            SpawnPlatform(boatPrefab, boatSpawnRight, boatSpawnLeft, BaseBoatSpeed);
        }
        if (timeSince > boatSpawnInterval)
        {
            SpawnPlatform(boatPrefab, boatSpawnRight, boatSpawnLeft, BaseBoatSpeed);
            timeSince = 0;
        }
        timeSince += Time.deltaTime;
    }

    public void Respawn(GameObject playerPrefab, Transform spawnPoint)
    {
        GameObject oldPlayer = GameObject.FindGameObjectWithTag("Player");
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        player = newPlayer;
        Destroy(oldPlayer);
    }

    // Spawns obstacles at designated spawn points but gives a random car obstacle out of the array
    public void SpawnObstacles(GameObject[] obstaclePrefabs, Transform[] spawnPointsRight, Transform[] spawnPointsLeft, float velocity)
    {
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        for (int i = 0; i < spawnPointsRight.Length; i++)
        {
            GameObject obstacle = Instantiate(obstaclePrefabs[randomIndex], spawnPointsRight[i].position, spawnPointsRight[i].rotation);
            Physics.IgnoreCollision(leftWall.GetComponent<Collider>(), obstacle.GetComponent<Collider>());
            Physics.IgnoreCollision(rightWall.GetComponent<Collider>(), obstacle.GetComponent<Collider>());
            obstacle.GetComponent<Rigidbody>().linearVelocity = new Vector3(-velocity, 0, 0);
            Destroy(obstacle, 30);
        }

        randomIndex = Random.Range(0, obstaclePrefabs.Length);
        for (int i = 0; i < spawnPointsLeft.Length; i++)
        {
            GameObject obstacle = Instantiate(obstaclePrefabs[randomIndex], spawnPointsLeft[i].position, spawnPointsLeft[i].rotation);
            Physics.IgnoreCollision(leftWall.GetComponent<Collider>(), obstacle.GetComponent<Collider>());
            Physics.IgnoreCollision(rightWall.GetComponent<Collider>(), obstacle.GetComponent<Collider>());
            obstacle.GetComponent<Rigidbody>().linearVelocity = new Vector3(velocity, 0, 0);
            Destroy(obstacle, 30);
        }
    }

    public void SpawnPlatform(GameObject platformPrefab, Transform[] spawnPointsRight, Transform[] spawnPointsLeft, float velocity)
    {
        for(int i = 0; i < spawnPointsRight.Length; i++)
        {
            GameObject platform = Instantiate(platformPrefab, spawnPointsRight[i].position, spawnPointsRight[i].rotation);
            Physics.IgnoreCollision(leftWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
            Physics.IgnoreCollision(rightWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
            platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(-velocity, 0, 0);
            Destroy(platform, 30);
        }

        for(int i = 0; i < spawnPointsLeft.Length; i++)
        {
            GameObject platform = Instantiate(platformPrefab, spawnPointsLeft[i].position, spawnPointsLeft[i].rotation);
            Physics.IgnoreCollision(leftWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
            Physics.IgnoreCollision(rightWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
            platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(velocity, 0, 0);
            Destroy(platform, 30);
        }

        /* GameObject platform = Instantiate(platformPrefab, spawnPoint.position, spawnPoint.rotation);
        // ignore collisions with the walls only
        Physics.IgnoreCollision(leftWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
        Physics.IgnoreCollision(rightWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
        if (spawnPoint.position.x < -20)
        {
            platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(-velocity, 0, 0);
        }
        else if (spawnPoint.position.x > -20)
        {
            platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(velocity, 0, 0);
        }
        else
        Destroy(platform, 10);
 */    }
}
