using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawn;
    public float maxFireRate = 4f;
    public float minFireRate = 1.2f;
    public float projectileSpeed = 10f;

    private float fireTimer;
    private float randTimer;
    private float fireRate;

    // Update is called once per frame
    void Update()
    {
        Fire();
        UpdateFireRate();
    }

    private void Fire()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            GameObject cannonball = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            cannonball.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, -projectileSpeed);
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
