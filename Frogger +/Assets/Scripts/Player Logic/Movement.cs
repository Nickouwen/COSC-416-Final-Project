using UnityEngine;
using DG.Tweening;
using System.Threading;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveAmount = 1;
    private bool isPlayerMoving = false;
    public Rigidbody playerRb;
    private bool onLeftWall = false;
    private bool onRightWall = false;
    public GameObject player;
    // how long bounce animation is
    

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
                MovePlayer(new Vector3(0, 0, moveAmount));
            }
            if(InputManager.Instance.MoveDown)
            {
                MovePlayer(new Vector3(0, 0, -moveAmount));
            }
            if(InputManager.Instance.MoveRight && !onRightWall)
            {
                onLeftWall = false;
                MovePlayer(new Vector3(moveAmount, 0, 0));
            }
            if(InputManager.Instance.MoveLeft && !onLeftWall)
            {
                onRightWall = false;
                MovePlayer(new Vector3(-moveAmount, 0, 0));
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            GameManager.Instance.Respawn(1);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.Respawn(1);
        }
        else if (collision.gameObject.CompareTag("EndGate"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.IncrementScore();
            GameManager.Instance.Respawn(0);
        }
        else if (collision.gameObject.CompareTag("Left-Wall"))
        {
            onLeftWall = true;
        }
        else if (collision.gameObject.CompareTag("Right-Wall"))
        {
            onRightWall = true;
        }
    }
    // Moves player only when the previous animation is over
    void MovePlayer(Vector3 direction)
    {
        if (player != null)
        {
            isPlayerMoving = true;
            Vector3 targetPosition = player.transform.position + direction;
            DOTween.Sequence()
                .Append(player.transform.DOJump(targetPosition, 1, 1, 0.1f))
                .Append(player.transform.DOMove(targetPosition, 0.009f).SetEase(Ease.OutBounce))
                .AppendInterval(0.205f)
                .OnComplete(() => {
                    isPlayerMoving = false;
                    //playerRb.AddForce(Vector3.down * 0.5f, ForceMode.Impulse);
                });
        }
    }
}

//playerRb.AddForce(Vector3.down * 0.0000057f, ForceMode.Impulse);
