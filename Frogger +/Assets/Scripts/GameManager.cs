using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int lives = 2;
    [SerializeField] public int boatSpeed = 10;
    [SerializeField] public int obstacleSpeed = 25;

    // Game objects to spawn & spawn points
    public GameObject playerPrefab;
    public Transform playerSpawn;
    public GameObject player;



    public static GameManager Instance;
    private void Awake()
    {
        DisableSettingsMenu();
    }
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
            Respawn();
        }
    }

    public void Respawn()
    {
        GameObject oldPlayer = GameObject.FindGameObjectWithTag("Player");
        GameObject newPlayer = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        player = newPlayer;
        Destroy(oldPlayer);
        lives--;
        if (lives == 0)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        // Do something in UI (to be created)
        Debug.Log("Game over!");
    }
}
