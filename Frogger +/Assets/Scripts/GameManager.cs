using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int lives = 3;
    [SerializeField] public int boatSpeed = 10;
    [SerializeField] public int obstacleSpeed = 25;

    // Game objects to spawn & spawn points
    public GameObject playerPrefab;
    public Transform playerSpawn;
    public GameObject player;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject credits;
    public ScoreCounterUI scoreCounter;

    private GameObject previousMenu;
    private int gatesDestroyed;
    private int score;
    private bool settingsOpen;
    [SerializeField] private bool gameOver;

    public int GatesDestroyed => gatesDestroyed;
    public bool IsSettingsOpen => settingsOpen;
    public bool IsGameOver => gameOver;



    public static GameManager Instance;
    void Awake()
    {
        DisableMenu(settingsMenu);
        DisableMenu(gameOverMenu);
        DisableMenu(pauseMenu);
        DisableMenu(credits);
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this; // setup singleton
        EnableMenu(mainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.Respawn)
        {
            Respawn(0);
        }
        if (InputManager.Instance.ToggleSettings)
        {
            if (settingsOpen) DisableMenu(pauseMenu);
            else EnableMenu(pauseMenu);
        }
        if (gatesDestroyed == 5)
        {
            EndGateManager.Instance.SpawnEndGates();
            gatesDestroyed = 0;
        }
        if (gameOver)
        {
            if (!gameOverMenu.activeSelf)
            {
                EnableMenu(gameOverMenu);
            }
        }
    }
    public void IncrementScore()
    {
        score++;
        gatesDestroyed++;
        scoreCounter.UpdateScore(score);
    }

    public void EnableMenu(GameObject menu)
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisableMenu(GameObject menu)
    {
        previousMenu = menu;
        Time.timeScale = 1f;
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisableNestedMenu(GameObject menu)
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToPreviousMenu()
    {
        if (previousMenu != null)
        {
            EnableMenu(previousMenu);
        }
    }

    public void Respawn(int livesToDecrement)
    {
        GameObject oldPlayer = GameObject.FindGameObjectWithTag("Player");
        GameObject newPlayer = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        player = newPlayer;
        Destroy(oldPlayer);
        
        lives -= livesToDecrement;
        if (lives == 0)
        {
            Debug.Log("Game Over!");
            TriggerGameOver();
        }
    }

    public void ResetGame()
    {
        Respawn(0);
        lives = 3;
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("ship");
        foreach (GameObject platform in platforms)
        {
            Destroy(platform);
        }
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
        GameObject[] endGates = GameObject.FindGameObjectsWithTag("EndGate");
        foreach (GameObject gate in endGates)
        {
            Destroy(gate.gameObject);
        }
        gatesDestroyed = 5;
        score = 0;
        scoreCounter.UpdateScore(score);
        gameOver = false;
        DisableMenu(gameOverMenu);
    }

    public void TriggerGameOver()
    {
        gameOver = true;
    }
}