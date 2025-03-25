using System.Data;
using UnityEngine;

public class EndGateController : MonoBehaviour
{
    [SerializeField] public Transform[] endGates;
    [SerializeField] public GameObject endGatePrefab;

    void Awake()
    {
        SpawnEndGates();
    }

    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.GatesDestroyed == 5)
            {
                SpawnEndGates();
            }
            if (GameManager.Instance.Reset)
            {
                ClearEndGates();
                SpawnEndGates();
            }
        }
    }
    public void SpawnEndGates()
    {
        for (int i = 0; i < endGates.Length; i++)
        {
            if (endGates[i] != null)
            {
                Instantiate(endGatePrefab, endGates[i].position, endGates[i].rotation);
            }
        }
    }

    public void ClearEndGates()
    {
        GameObject[] endGate = GameObject.FindGameObjectsWithTag("EndGate");
        for (int i = 0; i < endGate.Length; i++)
        {
            Destroy(endGate[i]);
        }
    }
}
