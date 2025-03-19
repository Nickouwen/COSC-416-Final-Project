using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private bool isPlayerMoving = false;
    public GameObject player;
    // how long bounce animation is
    public float bounceDuration = .15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerMoving){
            if(InputManager.Instance.MoveUp)
            {
                MovePlayer(Vector3.forward);
            }
            if(InputManager.Instance.MoveDown)
            {
                MovePlayer(Vector3.forward*-1);
            }
            if(InputManager.Instance.MoveRight)
            {
                MovePlayer(Vector3.right);
            }
            if(InputManager.Instance.MoveLeft)
            {
                MovePlayer(Vector3.left);
            }
        }
    }
    // Moves player only when the previous animation is over
    void MovePlayer(Vector3 direction){
        isPlayerMoving = true;
        player.transform.DOMove(player.transform.position + direction, bounceDuration).SetEase(Ease.OutBounce).OnComplete(() => isPlayerMoving = false);
    }
}
