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
    public ScoreCounterUI scoreCounter;

    private int gatesDestroyed;
    private int score;
    private bool settingsOpen;

    public int GatesDestroyed => gatesDestroyed;
    public bool IsSettingsOpen => settingsOpen;



    public static GameManager Instance;
    void Awake()
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
        if (InputManager.Instance.ToggleSettings)
        {
            if (settingsOpen) DisableSettingsMenu();
            else EnableSettingsMenu();
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
        Time.timeScale = 0f;
        settingsMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        settingsOpen = true;
    }

    public void DisableSettingsMenu()
    {
        Time.timeScale = 1f;
        settingsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        settingsOpen = false;
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