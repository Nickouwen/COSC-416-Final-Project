using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawn;
    public float fireRate = 1f;
    public float projectileSpeed = 10f;

    private float fireTimer;

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            GameObject cannonball = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            cannonball.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, projectileSpeed);
            fireTimer = 0f;
        }
    }
}
