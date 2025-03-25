using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRate;
    [SerializeField] private bool rightToLeft;

    [SerializeField] private int speed;
    private float spawnTimer;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.Reset)
            {
                Destroy(gameObject);
            }
            if (rightToLeft) speed = GameManager.Instance.obstacleSpeed * -1;
            else speed = GameManager.Instance.obstacleSpeed;
        }
        SpawnObstacle();
    }

    // Spawns obstacles at designated spawn points but gives a random car obstacle out of the array
    public void SpawnObstacle()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > 1/spawnRate)
        {
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);

            GameObject obstacle = Instantiate(obstaclePrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
            Physics.IgnoreCollision(leftWall.GetComponent<Collider>(), obstacle.GetComponent<Collider>());
            Physics.IgnoreCollision(rightWall.GetComponent<Collider>(), obstacle.GetComponent<Collider>());
            obstacle.GetComponent<Rigidbody>().linearVelocity = new Vector3(speed, 0, 0);
            Destroy(obstacle, 30);
            spawnTimer = 0f;
        }
    }
}
