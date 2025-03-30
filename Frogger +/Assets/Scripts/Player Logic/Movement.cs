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
    private float directionTurn = 0;

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
            PlayerHeadController deadBodyController = player.GetComponent<PlayerHeadController>();
            deadBodyController.sinkBody();
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
            RaycastHit hit;
            float targetY = targetPosition.y;

            if(targetPosition.z > player.transform.position.z) directionTurn = 0f;
            else if(targetPosition.z < player.transform.position.z) directionTurn = -180f;
            else if(targetPosition.x > player.transform.position.x) directionTurn = 90f;
            else if(targetPosition.x < player.transform.position.x) directionTurn = -90f;
            
            if (Physics.Raycast(new Vector3(targetPosition.x, targetPosition.y + 10f, targetPosition.z), Vector3.down, out hit, 20f))
            {
                targetY = hit.point.y;
            }
            targetPosition.y = targetY;
            float baseJumpHeight = 2f;
            float heightDifference = targetY - player.transform.position.y;
            float jumpHeight = baseJumpHeight;

            if (heightDifference > 0) {
                jumpHeight += heightDifference * 0.5f;
            } else if (heightDifference < 0) {
                jumpHeight = Mathf.Max(baseJumpHeight * 0.7f, jumpHeight + heightDifference * 0.3f);
            }
            
            DOTween.Sequence()
                .Append(player.transform.DOJump(targetPosition, jumpHeight, 1, 0.1f))
                .Append(player.transform.DORotate(new Vector3(0, directionTurn, 0), 0.05f))
                .Append(player.transform.DOMove(targetPosition, 0.05f).SetEase(Ease.OutBounce))
                .AppendInterval(0.04f)
                //.AppendInterval(0.205f)
                .OnComplete(() => {
                    isPlayerMoving = false;
                });
        }
        
    }
}

//playerRb.AddForce(Vector3.down * 0.0000057f, ForceMode.Impulse);
