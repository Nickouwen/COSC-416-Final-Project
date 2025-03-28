using UnityEngine;
using System.Collections;
public class PlayerHeadController : MonoBehaviour
{
    public GameObject headPrefab;
    public Transform headAttachPoint;
    public float respawnDelay = 2.0f;
    public GameObject realHead;
    public GameObject bodyPrefab;
    public Transform body;
    public Transform bodyAttachPoint;

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

    public void throwBody(int direction)
    {
        body.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
        body.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
        GameObject detachedBody = Instantiate(bodyPrefab, bodyAttachPoint.position, bodyAttachPoint.rotation);

        Rigidbody rb = detachedBody.GetComponent<Rigidbody>();
        rb.AddForce(direction * 5.0f, 1f, 0, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);

        Destroy(detachedBody, respawnDelay + 1f);

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