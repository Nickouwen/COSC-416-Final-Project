using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int BaseBoatSpeed = 10;

    // Game objects to spawn & spawn points
    public GameObject playerPrefab;
    public GameObject boatPrefab;
    public Transform[] boatSpawnRight;

    public Transform[] boatSpawnLeft;
    public Transform playerSpawn;
    public GameObject player;
    public GameObject leftWall;
    public GameObject rightWall;

    public static GameManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.Respawn)
        {
            Respawn(playerPrefab, playerSpawn);
        }

        if (InputManager.Instance.SpawnBoat)
        {
            SpawnPlatform(boatPrefab, boatSpawnRight, boatSpawnLeft, BaseBoatSpeed);
        }
    }

    public static void Respawn(GameObject playerPrefab, Transform spawnPoint)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnPlatform(GameObject platformPrefab, Transform[] spawnPointsRight, Transform[] spawnPointsLeft, float velocity)
    {
        for(int i = 0; i < spawnPointsRight.Length; i++)
        {
            GameObject platform = Instantiate(platformPrefab, spawnPointsRight[i].position, spawnPointsRight[i].rotation);
            Physics.IgnoreCollision(leftWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
            Physics.IgnoreCollision(rightWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
            platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(-velocity, 0, 0);
            Destroy(platform, 10);
        }

        for(int i = 0; i < spawnPointsLeft.Length; i++)
        {
            GameObject platform = Instantiate(platformPrefab, spawnPointsLeft[i].position, spawnPointsLeft[i].rotation);
            Physics.IgnoreCollision(leftWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
            Physics.IgnoreCollision(rightWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
            platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(velocity, 0, 0);
            Destroy(platform, 10);
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
