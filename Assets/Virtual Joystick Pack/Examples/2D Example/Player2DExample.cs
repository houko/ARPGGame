using UnityEngine;

public class Player2DExample : MonoBehaviour 
{
    public float moveSpeed = 8f;
    public Joystick joystick;

    private void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);

        if (moveVector != Vector3.zero)
        { 
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
