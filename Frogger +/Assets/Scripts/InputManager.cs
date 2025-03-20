using UnityEngine;

public class InputManager : MonoBehaviour
{
    // These will need to be moved into a separate script; This is just for figuring things out.
    public Transform platformSpawnRef;
    public GameObject platformPrefab;
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed");
            Platforms.SpawnPlatform(platformPrefab, platformSpawnRef, 10);
        }
    }
}
