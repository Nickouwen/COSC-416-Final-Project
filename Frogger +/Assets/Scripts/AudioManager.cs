using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static AudioManager Instance;
    public AudioSource src;
    public AudioClip splash, carHit, turretHit, gameOver, levelWin, jump;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWaterSplash()
    {
        src.clip = splash;
        src.Play();
    }

    public void PlayCarHit()
    {
        src.clip = carHit;
        src.Play();
    }

    public void PlayTurretHit()
    {
        src.clip = turretHit;
        src.Play();
    }

    public void PlayGameOver()
    {
        src.clip = gameOver;
        src.Play();
    }

    public void PlayLevelWin()
    {
        src.clip = levelWin;
        src.Play();
    }

    public void PlayJump()
    {
        src.clip = jump;
        src.Play();
    }

}
