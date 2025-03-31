using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private Transform projectileSpawn;
    public float maxFireRate = 0.4f;
    public float minFireRate = 0.1f;
    public float projectileSpeed = 10f;

    private float fireTimer;
    private float randTimer = 10;
    private float fireRate;

    // Update is called once per frame
    void Update()
    {
        Fire();
        UpdateFireRate();
    }

    private void Fire()
    {
        int randInt = Random.Range(0, projectilePrefabs.Length);
        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            GameObject cannonball = Instantiate(projectilePrefabs[randInt], projectileSpawn.position, projectileSpawn.rotation);
            cannonball.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, -(projectileSpeed * GameManager.Instance.projectileSpeedMult));
            fireTimer = 0f;
        }
    }

    private void UpdateFireRate()
    {
        randTimer += Time.deltaTime;
        if (randTimer > 10)
        {
            fireRate = Random.Range(minFireRate,  maxFireRate);
            Debug.Log("New fire rate: " + fireRate);
            randTimer = 0f;
        }
    }
}
