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

    private void Awake()
    {
        Instance = this; // setup singleton
    }


    void Update()
    {
        MoveUp = Input.GetKeyDown(KeyCode.W);
        MoveDown = Input.GetKeyDown(KeyCode.S);
        MoveRight = Input.GetKeyDown(KeyCode.D);
        MoveLeft = Input.GetKeyDown(KeyCode.A);
        Respawn = Input.GetKeyDown(KeyCode.R);
        SpawnBoat = Input.GetKeyDown(KeyCode.Space);
    }
}