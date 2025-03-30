using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float spinSpeed = 90f;

    private void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward, spinSpeed/2 *  Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shield"))
        {
            Debug.Log("Hit shield");
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Respawn(1);
            }
            Debug.Log("Hit player!");
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Spawn"))
        {
            Destroy(gameObject);
        }
        else Destroy(gameObject, 15);
    }
}
