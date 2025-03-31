using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int lives = 3;
    [SerializeField] public int boatSpeed = 10;
    [SerializeField] public int obstacleSpeed = 25;
    [SerializeField] public float projectileSpeedMult = 1;

    // Game objects to spawn & spawn points
    public Transform playerSpawn;

    public GameObject playerPrefab;
    public GameObject player;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject credits;

    public Slider boatSlider;
    public Slider obstacleSlider;
    public Slider projectileSlider;

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
        boatSlider.value = boatSpeed;
        obstacleSlider.value = obstacleSpeed;
        projectileSlider.value = projectileSpeedMult;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBackgroundMusic();
        }

        UpdateBoatSpeed();
        UpdateObstacleSpeed();
        UpdateProjectileMult();
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
    public int getBoatSpeed()
    {
        return boatSpeed;
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
        if (player != null)
        {
            GameObject[] detachedHeads = GameObject.FindGameObjectsWithTag("DetachedHead");
            foreach (GameObject head in detachedHeads)
            {
                Destroy(head);
            }
            
            GameObject[] detachedBodies = GameObject.FindGameObjectsWithTag("DetachedBody");
            foreach (GameObject body in detachedBodies)
            {
                Destroy(body);
            }

            GameObject oldPlayer = GameObject.FindGameObjectWithTag("Player");
            GameObject newPlayer = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
            player = newPlayer;
            PlayerHeadController headController = player.GetComponent<PlayerHeadController>();
            headController.Reset();
            
            lives -= livesToDecrement;
            if (lives == 0)
            {
                TriggerGameOver();
            }
            Destroy(oldPlayer);
        }
    }

    public void ResetGame()
    {
        Respawn(1);
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
        AudioManager.Instance.PlayGameOver();
    }

    public void UpdateBoatSpeed()
    {
        boatSpeed = (int)boatSlider.value;
        Debug.Log("Set boat speed to:" + boatSpeed);
    }

    public void UpdateObstacleSpeed()
    {
        obstacleSpeed = (int)obstacleSlider.value;
        Debug.Log("Set obstacle speed to:" + obstacleSpeed);
    }

    public void UpdateProjectileMult()
    {
        projectileSpeedMult = projectileSlider.value;
        Debug.Log("Set projectile mult to:" + projectileSpeedMult);
    }
}