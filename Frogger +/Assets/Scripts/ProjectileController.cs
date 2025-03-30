using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

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
            PlayerHeadController headController = other.gameObject.GetComponent<PlayerHeadController>();
            headController.PopHead();
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
    private IEnumerator popPlayerHead()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.Respawn(1);
    }
}
