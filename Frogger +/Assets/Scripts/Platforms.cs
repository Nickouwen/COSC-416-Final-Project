using UnityEngine;

public class Platforms : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SpawnPlatform(GameObject platformPrefab, Transform spawnPoint, float velocity)
    {
        GameObject platform = Instantiate(platformPrefab, spawnPoint.position, spawnPoint.rotation);
        platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, velocity);
        Destroy(platform, 10);
    }
}
