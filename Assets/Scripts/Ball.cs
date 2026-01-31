using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    public float Speed = 5;
    private Vector2 Direction = Vector2.one;
    public Transform PaddleLeft, PaddleRight;
    private bool CanMove = false;
    public GameManager gameManager;

    private void Start()
    {
        ResetBall();
    }
    
    public void Update()
    {
        if (!CanMove)
        {
            return;
        }

        Move();
        Collision();
        CollisionPaddle();
    }

    private void Move()
    {
        Vector3 moviment = Direction * Speed * Time.deltaTime;
        transform.Translate(moviment);
    }

    private void Collision()
    {
        float BottomScreen, TopScreen;

        TopScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        BottomScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        Vector3 position = transform.position;

        if (Direction.y > 0 && position.y >= (TopScreen - 0.25f))
        {
            Direction.y = -1;
            gameManager.ChangeColors();
        }

        if (Direction.y < 0 && position.y <= (BottomScreen + 0.25f))
        {
            Direction.y = 1;
            gameManager.ChangeColors();
        }
    }

    private void CollisionPaddle()
    {
        float PaddleWith = 0.5f, PaddleHeight = 2f, Ballsize = 0.5f;

        if (Direction.x > 0)
        {
            if ((transform.position.x + Ballsize / 2f) > (PaddleRight.position.x - PaddleWith / 2f) && (transform.position.x + Ballsize / 2f) < (PaddleRight.position.x + PaddleWith / 2f)
                && transform.position.y > (PaddleRight.position.y - PaddleHeight / 2f) && transform.position.y < (PaddleRight.position.y + PaddleHeight / 2f))
            {
                Direction.x = -1;
                gameManager.ChangeColors();
            }
        }
        else
        {
            if (Direction.x < 0)
            {
                if ((transform.position.x + Ballsize / 2f) > (PaddleLeft.position.x - PaddleWith / 2f) && (transform.position.x + Ballsize / 2f) < (PaddleLeft.position.x + PaddleWith / 2f)
                && transform.position.y > (PaddleLeft.position.y - PaddleHeight / 2f) && transform.position.y < (PaddleLeft.position.y + PaddleHeight / 2f))
                {
                    Direction.x = 1;
                    gameManager.ChangeColors();
                }
            }
        }
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        Direction.x = Direction.x * -1;
        CanMove = false;
    }

    public void StartBall()
    {
        CanMove = true;
    }
}