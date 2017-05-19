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
	private uint playerState; // efficient storage of playerstate
	                          /*
	                           * 00000000
	                           * |||||||L0th: 0 - Jumping,    1 - Grounded
	                           * ||||||L 1st: 0 - n/Attack,   1 - Attacking
	                           * |||||L  2nd: 0 - Ready,      1 - Not Ready (attack animations begin when ready)
	                           * ||||L   3rd: 0 - Vulnerable, 1 - iframes
	                           * |||L    4th: 0 - Vulnerable, 1 - Blocking/dodging
	                           * ||L     5th: 0 - Healhty,    1 - Critical (0HP remaining)
	                           * |L      6th: 0 - Charging,   1 - Charged
	                           * |       7th: 0 - Ready,      1 - Stunned
	                           */

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
		if (currentJumpSpeed > -terminalVelocity) {
			currentJumpSpeed -= gravity;
		} else {
			transform.position = new Vector3(currentPosition.x, 0, currentPosition.z);
			return new Vector3 (0, 0, 0);
		}

		return new Vector3 (0, currentJumpSpeed, 0);

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
		{
			transform.position += walkInput();
        }
		if (Input.GetKeyDown (KeyCode.B))
		{
			currentJumpSpeed = jumpSpeed;

		}
		transform.position += jumpInput (transform.position);

		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
    }
}
