using UnityEngine;
using System.Collections;
public class PlayerHeadController : MonoBehaviour
{
    public GameObject headPrefab;
    public Transform headAttachPoint;
    public float respawnDelay = 2.0f;
    public GameObject realHead;

    public void PopHead()
    {      
        realHead.SetActive(false);
        GameObject detachedHead = Instantiate(headPrefab, headAttachPoint.position, headAttachPoint.rotation);

        Rigidbody rb = detachedHead.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 10f, ForceMode.Impulse);

        Destroy(detachedHead, respawnDelay + 2f);

        StartCoroutine(RespawnAfterDelay());
    }
    
    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.Respawn(1);
        }
    }
    
    public void Reset()
    {
        realHead.SetActive(true);
    }
}