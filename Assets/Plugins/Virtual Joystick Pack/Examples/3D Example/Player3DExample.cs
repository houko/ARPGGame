using UnityEngine;

public class Player3DExample : MonoBehaviour {

    public float moveSpeed = 8f;
    public Joystick joystick;

	void Update () 
	{
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
        }   
	}
}