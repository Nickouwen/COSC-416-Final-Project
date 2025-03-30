using UnityEngine;
using System.Collections;
public class PlayerHeadController : MonoBehaviour
{
    public static PlayerHeadController Instance;
    public GameObject headPrefab;
    public Transform headAttachPoint;
    public float respawnDelay = 2.0f;
    public GameObject realHead;
    public GameObject bodyPrefab;
    public Transform body;
    public Transform bodyAttachPoint;
    private bool isDead = false;

    void Awake()
    {
        Instance = this;
    }
    public void PopHead()
    { 
        if(isDead) return;
        isDead = true;     
        realHead.SetActive(false);
        GameObject detachedHead = Instantiate(headPrefab, headAttachPoint.position, headAttachPoint.rotation);

        AudioManager.Instance.PlayTurretHit();

        Rigidbody rb = detachedHead.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 10f, ForceMode.Impulse);

        Destroy(detachedHead, respawnDelay + 2f);

        StartCoroutine(RespawnAfterDelay());
    }

    public void throwBody(int direction)
    {
        if(isDead) return;
        isDead = true;
        body.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
        body.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
        GameObject detachedBody = Instantiate(bodyPrefab, bodyAttachPoint.position, bodyAttachPoint.rotation);

        AudioManager.Instance.PlayCarHit();

        Rigidbody rb = detachedBody.GetComponent<Rigidbody>();
        rb.AddForce(direction * 5.0f, 1f, 0, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);

        Destroy(detachedBody, respawnDelay + 1f);

        StartCoroutine(RespawnAfterDelay());
    }
    
    public void sinkBody()
    {
        if(isDead) return;
        isDead = true;
        body.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
        body.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
        GameObject detachedBody = Instantiate(bodyPrefab, bodyAttachPoint.position, bodyAttachPoint.rotation);
        detachedBody.transform.GetComponent<CapsuleCollider>().enabled = false;
        Rigidbody rb = detachedBody.GetComponent<Rigidbody>();
        rb.AddForce(0, -1.0f, 0, ForceMode.Force);
        rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);

        Destroy(detachedBody, respawnDelay + 1f);

        StartCoroutine(RespawnAfterDelay());
    }
    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (GameManager.Instance != null)
        {
            isDead = false;
            GameManager.Instance.Respawn(1);
        }
    }
    
    public void Reset()
    {
        realHead.SetActive(true);
    }

    public bool isPlayerDead()
    {
        return isDead;
    }
}