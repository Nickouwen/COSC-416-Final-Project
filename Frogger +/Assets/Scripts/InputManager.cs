using System.Collections.Concurrent;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance;
    public bool MoveUp { get; private set; }
    public bool MoveDown { get; private set; }
    public bool MoveRight { get; private set; }
    public bool MoveLeft { get; private set; }
    public bool Respawn { get; private set; }
    public bool SpawnBoat { get; private set; }
    public bool Blocking { get; private set; }
    public bool SpawnObstacles { get; private set; }
    public bool ToggleSettings { get; private set; }

    private void Awake()
    {
        Instance = this; // setup singleton
    }


    void Update()
    {
        MoveUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.I);
        MoveDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.K);
        MoveRight = Input.GetKey(KeyCode.D ) || Input.GetKey(KeyCode.L);
        MoveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.J);
        Respawn = Input.GetKey(KeyCode.R);
        Blocking = Input.GetKey(KeyCode.LeftShift);
        ToggleSettings = Input.GetKeyDown(KeyCode.P);
    }
}