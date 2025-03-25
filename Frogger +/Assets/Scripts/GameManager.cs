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
    public GameObject settingsMenu;
    public GameObject gameOverMenu;
    public ScoreCounterUI scoreCounter;

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
        DisableSettingsMenu();
        DisableEndScreen();
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
            Respawn(0);
        }
        if (InputManager.Instance.ToggleSettings)
        {
            if (settingsOpen) DisableSettingsMenu();
            else EnableSettingsMenu();
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
                EnableEndScreen();
            }
        }
    }
    public void IncrementScore()
    {
        score++;
        gatesDestroyed++;
        scoreCounter.UpdateScore(score);
    }
    public void EnableSettingsMenu()
    {
        if (gameOver) return;
        Time.timeScale = 0f;
        settingsMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        settingsOpen = true;
    }

    public void DisableSettingsMenu()
    {
        if (gameOver) return;
        Time.timeScale = 1f;
        settingsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        settingsOpen = false;
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
        DisableEndScreen();
    }

    public void TriggerGameOver()
    {
        gameOver = true;
    }
    public void EnableEndScreen()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void DisableEndScreen()
    {
        Time.timeScale = 1f;
        gameOverMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}