using System;
using Unity.VisualScripting;
using UnityEngine;

public class Deflection : MonoBehaviour
{
    [SerializeField] public GameObject shield;
    [SerializeField] public Transform player;
    private GameObject currentShield;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool multipleMovements = InputManager.Instance.MoveUp || InputManager.Instance.MoveDown || InputManager.Instance.MoveRight || InputManager.Instance.MoveLeft;

        if (InputManager.Instance.Blocking && !multipleMovements)
        {
            if (currentShield == null)
            {
                currentShield = Instantiate(shield, this.transform.position + new Vector3(0, 1.0f, 0), this.transform.rotation, player.transform);
            }
        }
        else
        {
            if (currentShield != null || multipleMovements)
            {
                Destroy(currentShield);
            }
        }
    }
}
