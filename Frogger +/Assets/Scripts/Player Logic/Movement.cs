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
    private bool isPlayerDead = false;
    private bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDead = PlayerHeadController.Instance.isPlayerDead();
        isGameOver = GameManager.Instance.IsGameOver;
        if (!isPlayerMoving && !isPlayerDead && !isGameOver){
            if(InputManager.Instance.MoveUp)
            {
                AudioManager.Instance.PlayJump();
                MovePlayer(new Vector3(0, 0, moveAmount));
            }
            if(InputManager.Instance.MoveDown)
            {
                AudioManager.Instance.PlayJump();
                MovePlayer(new Vector3(0, 0, -moveAmount));
            }
            if(InputManager.Instance.MoveRight && !onRightWall)
            {
                AudioManager.Instance.PlayJump();
                onLeftWall = false;
                MovePlayer(new Vector3(moveAmount, 0, 0));
            }
            if(InputManager.Instance.MoveLeft && !onLeftWall)
            {
                AudioManager.Instance.PlayJump();
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
            AudioManager.Instance.PlayWaterSplash();
        }
        else if (collision.gameObject.CompareTag("EndGate"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.IncrementScore();
            GameManager.Instance.Respawn(0);
            AudioManager.Instance.PlayLevelWin();
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
                .AppendInterval(0.005f)
                //.AppendInterval(0.205f)
                .OnComplete(() => {
                    isPlayerMoving = false;
                });
        }
        
    }
}

//playerRb.AddForce(Vector3.down * 0.0000057f, ForceMode.Impulse);
