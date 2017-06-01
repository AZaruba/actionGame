using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeed;
	public float jumpSpeed;
    public float doubleJumpSpeed;
	public float dashSpeed;
	public float gravity;
	public float terminalVelocity;
	public float dashDistance;

    private Camera mainCam;
	private float currentJumpSpeed;

	private bool grounded;
	private bool falling;
    private bool attacking;
	private bool dashing;
    private bool doubleJump;

    private RaycastHit groundOut; // gets information for objects below the player
    private RaycastHit wallOut; // checks for walls
    private float colliderX;
    private float colliderY;
    private float colliderZ;

	private Vector3 downRay = new Vector3(0, -1, 0); // the ray casting downward in global space
    private int envMask = 1 << 9;

    private collectCollision collectCol;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
        grounded = false;
        attacking = false;
		dashing = false;
        doubleJump = false;
        Vector3 colliderInfo = GetComponent<Collider>().bounds.size;
        colliderX = colliderInfo.x / 2;
        colliderY = colliderInfo.y / 2;
        colliderZ = colliderInfo.z / 2;

        collectCol = this.gameObject.GetComponent<collectCollision>();
    }

    Vector3 wallNormal(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out wallOut, 0.7f, envMask))
            return wallOut.normal;
        else
            return new Vector3(0, 0, 0);
    }

    Vector3 walkInput()
    {

        Vector3 direction = new Vector3();
		Vector3 cameraDirection = new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z);
		float rotationDegree;
        
		rotationDegree = Mathf.Atan2 (Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"));
		direction.x = -cameraDirection.z * Mathf.Cos (rotationDegree) + cameraDirection.x * Mathf.Sin (rotationDegree);
		direction.z = cameraDirection.z * Mathf.Sin (rotationDegree) + cameraDirection.x * Mathf.Cos (rotationDegree);

        //direction.Normalize();
        //cameraDirection.Normalize();

        direction *= walkSpeed * Time.deltaTime;
        if (Physics.Raycast(transform.position, direction, out wallOut, 0.2f, envMask))
        {
            direction.x = 0;
            direction.z = 0;
        }
		mainCam.transform.position += direction;
		transform.rotation = Quaternion.LookRotation(direction);
        return direction;
    }

	Vector3 jumpInput(Vector3 currentPosition) {
		if (currentJumpSpeed > -terminalVelocity && !grounded) {
			currentJumpSpeed -= gravity * Time.deltaTime;
		}
		if (currentJumpSpeed < 0 && !grounded) {
			falling = true;
		}
		if (Physics.Raycast(transform.position, downRay, out groundOut, 0.2f, envMask) && !grounded && falling) {
            Vector3 currentCameraPos = mainCam.transform.position;
			transform.position = new Vector3 (groundOut.point.x, groundOut.point.y + colliderY, groundOut.point.z);
            mainCam.transform.position = new Vector3(currentCameraPos.x, groundOut.point.y + colliderY + 2, currentCameraPos.z);
			currentJumpSpeed = 0;
			grounded = true;
			falling = false;
            doubleJump = false;
        }
		if (grounded && !Physics.Raycast(transform.position, downRay, 0.1f + colliderY)) {
			grounded = false;
			falling = true;
            doubleJump = false;
            currentJumpSpeed = 0;
		}

		if (currentPosition.y < -5.0f) {
			transform.position = new Vector3 (0, 0, 6);
            mainCam.transform.position = new Vector3(0, 2, 0);
			currentJumpSpeed = 0;
			grounded = true;
			falling = false;
            doubleJump = false;
			GetComponent<reset> ().backToOne ();
		}

		return new Vector3 (0, currentJumpSpeed, 0);

	}

	void dashInput(Vector3 direction, float delta) {
		if (delta == dashDistance) {
			dashing = false;
		} else {

		}
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
		{
			transform.position += walkInput();
        }
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && grounded && !attacking)
		{
			currentJumpSpeed = jumpSpeed;
			grounded = false;

		}
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && !grounded && falling && !doubleJump && !attacking)
        {
            currentJumpSpeed = doubleJumpSpeed;
            doubleJump = true;
        }
            if ((Input.GetKeyDown (KeyCode.B) || Input.GetKeyDown(KeyCode.JoystickButton14))&& !attacking)
        {
            attacking = true;
            transform.Rotate(45, 0, 0);
            transform.Rotate(-45, 0, 0);
            attacking = false;

        }
        Vector3 jumpResult = jumpInput(transform.position);
		transform.position += jumpResult;
        mainCam.transform.position += jumpResult;

        collectCol.coinCollision();
        collectCol.goalItemCollision();


        if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
    }
}
