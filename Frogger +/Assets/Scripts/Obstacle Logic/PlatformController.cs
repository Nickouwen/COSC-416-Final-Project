using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRate;
    [SerializeField] private bool rightToLeft;

    private int speed;
    private float spawnTimer;

    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.Reset)
            {
                Destroy(gameObject);
            }
            if (rightToLeft) speed = GameManager.Instance.boatSpeed * -1;
            else speed = GameManager.Instance.boatSpeed;
        }
        SpawnPlatform();

    }

    void SpawnPlatform()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 1f / spawnRate)
        {
            GameObject platform = Instantiate(platformPrefab, spawnPoint.position, spawnPoint.rotation);
            Physics.IgnoreCollision(leftWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
            Physics.IgnoreCollision(rightWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
            platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(speed, 0, 0);
            Destroy(platform, 30);
            spawnTimer = 0f;
        }

        /* GameObject platform = Instantiate(platformPrefab, spawnPoint.position, spawnPoint.rotation);
        // ignore collisions with the walls only
        Physics.IgnoreCollision(leftWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
        Physics.IgnoreCollision(rightWall.GetComponent<Collider>(), platform.GetComponent<Collider>());
        if (spawnPoint.position.x < -20)
        {
            platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(-velocity, 0, 0);
        }
        else if (spawnPoint.position.x > -20)
        {
            platform.GetComponent<Rigidbody>().linearVelocity = new Vector3(velocity, 0, 0);
        }
        else
        Destroy(platform, 10);
 */
    }
}
