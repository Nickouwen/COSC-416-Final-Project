using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private bool isPlayerMoving = false;
    public Rigidbody playerRb;
    private bool onLeftWall = false;
    private bool onRightWall = false;
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
            if(InputManager.Instance.MoveRight && !onRightWall)
            {
                onLeftWall = false;
                MovePlayer(Vector3.right);
            }
            if(InputManager.Instance.MoveLeft && !onLeftWall)
            {
                onRightWall = false;
                MovePlayer(Vector3.left);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Left-Wall"))
        {
            onLeftWall = true;
        }
        else if (collision.gameObject.CompareTag("Right-Wall"))
        {
            onRightWall = true;
        }
    }
    // Moves player only when the previous animation is over
    void MovePlayer(Vector3 direction){
        isPlayerMoving = true;
        player.transform.DOMove(player.transform.position + direction, bounceDuration).SetEase(Ease.OutBounce).OnComplete(() => isPlayerMoving = false);
        player.transform.DOJump(player.transform.position + direction, 1, 1, bounceDuration, false).OnComplete(() => isPlayerMoving = false);
    }
}
