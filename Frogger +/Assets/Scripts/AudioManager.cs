using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static AudioManager Instance;
    public AudioSource src;
    public AudioSource backgroundMusicSource;
    public AudioClip backgroundMusic;
    public AudioClip splash, carHit, turretHit, gameOver, levelWin, jump, buttonClick, sliderChange;

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
        ReduceMusicVolume();
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

    public void buttonClickSound()
    {
        src.clip = buttonClick;
        src.Play();
    }

    public void sliderChangeSound()
    {
        src.clip = sliderChange;
        src.Play();
    }

    public void ReduceMusicVolume()
    {
        backgroundMusicSource.volume -= Time.deltaTime / 100;
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusicSource.PlayOneShot(backgroundMusic);
    }

}
