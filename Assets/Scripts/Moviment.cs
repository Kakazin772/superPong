using System.Net.Sockets;
using UnityEngine;

public class Moviment : MonoBehaviour
{
    public float Speed = 8;
    public KeyCode KeyDown, KeyUp;

    private void Update()
    {
        float moviment = ProcessInput();

        Move(moviment);

        ClampPosition();
    }

    private float ProcessInput()
    {
        float moviment = 0;

        if (Input.GetKey(KeyUp))
        {
            moviment = 1;
        }

        if (Input.GetKey(KeyDown))
        {
            moviment = -1;
        }

        return moviment;
    }

    private void Move(float movement)
    {
        transform.Translate(0, movement * Speed * Time.deltaTime, 0);
    }

    private void ClampPosition()
    {
        float MaxPosition, MinPosition;
        
        MaxPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        MinPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        Vector3 position = transform.position;

        position.y = Mathf.Clamp(position.y, MinPosition + 1, MaxPosition - 1);

        transform.position = position;
    }
}
