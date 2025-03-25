using UnityEngine;

public class EndGateManager : MonoBehaviour
{
    [SerializeField] Transform[] endGateSpawns;
    [SerializeField] GameObject endGatePrefab;

    public static EndGateManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates.
            return;
        }
        SpawnEndGates();
    }
    public void SpawnEndGates()
    {
        // Destroy existing end gates.
        GameObject[] existingEndGates = GameObject.FindGameObjectsWithTag("EndGate");
        foreach (GameObject gate in existingEndGates)
        {
            Destroy(gate);
        }

        // Spawn new end gates.
        if (endGateSpawns != null && endGatePrefab != null)
        {
            foreach (Transform gateSpawn in endGateSpawns)
            {
                if (gateSpawn != null)
                {
                    Instantiate(endGatePrefab, gateSpawn.position, gateSpawn.rotation);
                }
            }
        }
        else
        {
            Debug.LogError("EndGateSpawns or EndGatePrefab is not assigned!");
        }
    }
}
