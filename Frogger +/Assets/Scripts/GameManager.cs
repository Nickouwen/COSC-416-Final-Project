using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int BaseBoatSpeed = 10;

    // Game objects to spawn & spawn points
    public GameObject playerPrefab;
    public GameObject boatPrefab;
    public Transform boatSpawn;
    public Transform playerSpawn;

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
            SpawnPlatform(boatPrefab, boatSpawn, BaseBoatSpeed);
        }
    }

    public static void Respawn(GameObject playerPrefab, Transform spawnPoint)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public static void SpawnPlatform(GameObject platformPrefab, Transform spawnPoint, float velocity)
    {
        GameObject platform = Instantiate(platformPrefab, spawnPoint.position, spawnPoint.rotation);
        platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, velocity);
        Destroy(platform, 10);
    }
}
