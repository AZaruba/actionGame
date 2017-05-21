using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeed;
	public float jumpSpeed;
	public float gravity;
	public float terminalVelocity;

    private Camera mainCam;
	private float currentJumpSpeed;
	private bool grounded;
	private bool falling;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
    }

    Vector3 walkInput()
    {

        Vector3 direction = new Vector3();
		Vector3 cameraDirection = new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z);
		float rotationDegree;

		rotationDegree = Mathf.Atan2 (Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"));
		direction.x = -cameraDirection.z * Mathf.Cos (rotationDegree) + cameraDirection.x * Mathf.Sin (rotationDegree);
		direction.z = cameraDirection.z * Mathf.Sin (rotationDegree) + cameraDirection.x * Mathf.Cos (rotationDegree);

        direction.Normalize();
        cameraDirection.Normalize();

        direction *= walkSpeed * Time.deltaTime;
		mainCam.transform.position += direction;
		transform.rotation = Quaternion.LookRotation(direction);
        return direction;
    }

	Vector3 jumpInput(Vector3 currentPosition) {
		if (currentJumpSpeed > -terminalVelocity && !grounded) {
			currentJumpSpeed -= gravity;
		}
		if (currentJumpSpeed < 0 && !grounded) {
			falling = true;
		}
		if (currentPosition.y < 0 && !grounded && falling) {
			transform.position = new Vector3 (currentPosition.x, 0, currentPosition.z);
			currentJumpSpeed = 0;
			grounded = true;
			falling = false;
		}
		return new Vector3 (0, currentJumpSpeed, 0);

	}

	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
		{
			transform.position += walkInput();
        }
		if (Input.GetKeyDown (KeyCode.B) && grounded)
		{
			currentJumpSpeed = jumpSpeed;
			grounded = false;

		}
		transform.position += jumpInput (transform.position);

		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
    }
}
