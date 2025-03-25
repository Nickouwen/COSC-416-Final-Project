using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
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
                GameManager.Instance.Respawn();
            }
            Debug.Log("Hit player!");
            Destroy(gameObject);
        }
        else Destroy(gameObject, 15);
    }
}
