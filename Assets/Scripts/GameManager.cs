using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public int ScorePlayer1 = 0, ScorePlayer2 = 0, TotalScore = 0;
    public Text Score, WinnerText;
    private bool StartedGame = false;
    public SpriteRenderer BallRender, PaddleRigthRender, PaddleLeftRender;
    public Transform PaddleRight, PaddleLeft;

    private void Start()
    {
        WinnerText.text = "";

        float altura = Camera.main.orthographicSize;
        float largura = altura * Camera.main.aspect;

        float margem = 0.5f;

        PaddleLeft.position = new Vector3(-largura + margem, 0, 0);
        PaddleRight.position = new Vector3(largura - margem, 0, 0);
    }

    private void Update()
    {
        if (!StartedGame && Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
            StartedGame = true;
            ball.StartBall();
        }
        
        float screenLeft, screenRight;

        screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        if (ball.transform.position.x + 0.25f < screenLeft)
        {
            AddScore(1);
            ball.ResetBall();
        }
        else
        {
            if (ball.transform.position.x - 0.25f > screenRight)
            {
                AddScore(2);
                ball.ResetBall();
            }
        }

        MaxScore();
    }

    private void AddScore(int player)
    {
        if (player == 1)
        {
            ScorePlayer1 = ScorePlayer1 + 1;
        }
        else
        {
            if (player == 2)
            {
                ScorePlayer2 = ScorePlayer2 + 1;
            }
        }

        TotalScore = TotalScore + 1;

        if (TotalScore % 2 == 0)
        {
            ball.Speed = ball.Speed + 2f;
        }

        Score.text = $"{ScorePlayer2} x {ScorePlayer1}";

        Invoke(nameof(ReleaseBall), 2f);
    }

    private void MaxScore()
    {
        if (ScorePlayer1 == 5)
        {
            ShowWinner("Player 1 Venceu!!");
        }
        else
        {
            if (ScorePlayer2 == 5)
            {
                ShowWinner("Player 2 Venceu!!");
            }
        }

        Score.text = $"{ScorePlayer2} x {ScorePlayer1}";
    }

    private void ResetGame()
    {
        ScorePlayer1 = 0;
        ScorePlayer2 = 0;
        TotalScore = 0;

        ball.Speed = 10;
        ball.ResetBall();

        WinnerText.text = "";
    }

    private void ShowWinner(string winner)
    {
        WinnerText.text = winner;

        StartedGame = false;
    }

    private void ReleaseBall()
    {
        ball.StartBall();
    }

    public void ChangeColors()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value), randomColorBackground = new Color(Random.value, Random.value, Random.value);

        BallRender.color = randomColor;
        PaddleLeftRender.color = randomColor;
        PaddleRigthRender.color = randomColor;

        Camera.main.backgroundColor = randomColorBackground * 0.5f;

    }
}