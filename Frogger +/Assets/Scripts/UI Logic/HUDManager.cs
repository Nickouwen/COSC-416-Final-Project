using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] public GameObject[] hearts;

    void Update()
    {
        if (GameManager.Instance != null && hearts != null)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < GameManager.Instance.lives)
                {
                    hearts[i].SetActive(true); // Show hearts for remaining lives
                }
                else
                {
                    hearts[i].SetActive(false); // Hide hearts for lost lives
                }
            }
        }
    }
}