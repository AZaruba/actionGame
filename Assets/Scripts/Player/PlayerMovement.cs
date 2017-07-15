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
	public float dashTime;

    private Camera mainCam;
	private float currentJumpSpeed;
	private float currentWalkSpeed;

	private bool grounded;
	private bool falling;
    private bool attacking;
	private bool dashing;
    private bool doubleJump;

    private RaycastHit groundOut; // gets information for objects below the player
    private RaycastHit wallOut; // checks for walls
	private RaycastHit slopeOut; // checks/compensates for slopes
    private float colliderX;
    private float colliderY;
    private float colliderZ;

	private Vector3 downRay = new Vector3(0, -1, 0); // the ray casting downward in global space
	private Vector3 upRay = new Vector3(0, 1, 0);
    private int envMask = 1 << 9;

    private collectCollision collectCol;
	private KeyCode[] controllerKeys;

	void SetControls() {
		controllerKeys = new KeyCode[16];
		if (Application.platform.Equals (RuntimePlatform.OSXEditor) || Application.platform.Equals(RuntimePlatform.OSXPlayer)) {
			controllerKeys [0] = KeyCode.JoystickButton14;
			controllerKeys [1] = KeyCode.JoystickButton15;
		} else {
			controllerKeys [0] = KeyCode.JoystickButton0;
			controllerKeys [1] = KeyCode.JoystickButton2;
		}
	}
	// Use this for initialization
	void Start () {
		SetControls ();
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
		currentWalkSpeed = walkSpeed;
		SetControls ();
    }

    Vector3 wallNormal(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out wallOut, 0.7f, envMask))
            return wallOut.normal;
        else
            return new Vector3(0, 0, 0);
    }

	Vector3 detectSlope() {
		/* this vector should have the same origin as the wall one (as if there's anything "forward"
		   that collides with it, that would be considered a wall! */
		if (Physics.Raycast (transform.position, downRay, out slopeOut, colliderY,  envMask) && grounded && mainCam) {
			mainCam.transform.position = new Vector3 (mainCam.transform.position.x, slopeOut.point.y + colliderY + 2, mainCam.transform.position.z);
			return new Vector3 (transform.position.x, slopeOut.point.y + colliderY, transform.position.z);
		}
		// Downward sloping (we use the distance for this one (currently 0.4) to set the angle limit)
		Vector3 bottomVec = new Vector3 (transform.position.x, transform.position.y - colliderY, transform.position.z);
		if (Physics.Raycast (bottomVec, downRay, out slopeOut, 0.4f, envMask) && grounded && mainCam) {
			mainCam.transform.position = new Vector3 (mainCam.transform.position.x, slopeOut.point.y + colliderY + 2, mainCam.transform.position.z);
			return new Vector3 (transform.position.x, slopeOut.point.y + colliderY, transform.position.z);
		}
		return transform.position;
	}

    Vector3 walkInput()
    {

        Vector3 direction = new Vector3();
		Vector3 cameraDirection;
		if (mainCam) {
			cameraDirection = new Vector3 (mainCam.transform.forward.x, 0, mainCam.transform.forward.z);
		} else {
			cameraDirection = new Vector3 (0, 0, 1);
		}
		float rotationDegree;
        
		rotationDegree = Mathf.Atan2 (Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"));
		direction.x = -cameraDirection.z * Mathf.Cos (rotationDegree) + cameraDirection.x * Mathf.Sin (rotationDegree);
		direction.z = cameraDirection.z * Mathf.Sin (rotationDegree) + cameraDirection.x * Mathf.Cos (rotationDegree);

        direction.Normalize();
        cameraDirection.Normalize();

        // if we collide with a wall
		// NOTE: we can hack in the valid slope angles here by moving the origin of this raycast
        if (Physics.Raycast(transform.position, direction, out wallOut, 0.7f, envMask))
        {
            direction.x += wallOut.normal.normalized.x;
            direction.z +=  wallOut.normal.normalized.z;
            direction *= currentWalkSpeed * Time.deltaTime;
        }
        else
        {
			transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            direction *= currentWalkSpeed * Time.deltaTime;
        }
		if (mainCam) {
			mainCam.transform.position += direction;
		}

        return direction;
    }

	Vector3 jumpInput(Vector3 currentPosition) {
		if (currentJumpSpeed > -terminalVelocity && !grounded) {
			currentJumpSpeed -= gravity * Time.deltaTime;
		}
		if (currentJumpSpeed < 0 && !grounded) {
			falling = true;
		}

		if (Physics.Raycast(transform.position, upRay, 0.2f, envMask) && !falling) {
			falling = true;
			currentJumpSpeed = 0;
		}

		// collided with ground
		if (Physics.Raycast(transform.position, downRay, out groundOut, 0.7f, envMask) && !grounded && falling) {
            Vector3 currentCameraPos = mainCam.transform.position;
			transform.position = new Vector3 (groundOut.point.x, groundOut.point.y + colliderY, groundOut.point.z);
			if (mainCam) {
				mainCam.transform.position = new Vector3 (currentCameraPos.x, groundOut.point.y + colliderY + 2, currentCameraPos.z);
			}
			currentJumpSpeed = 0;
			grounded = true;
			falling = false;
            doubleJump = false;
			if (dashing) {
				currentWalkSpeed = walkSpeed;
				dashing = false;
			}
        }
		if (grounded && !Physics.Raycast(transform.position, downRay, 0.1f + colliderY)) {
			grounded = false;
			falling = true;
            doubleJump = false;
            currentJumpSpeed = 0;
		}

		if (currentPosition.y < -10.0f) {
			mainCam = null;
		}

		if (currentPosition.y < -50.0f) {
			GameObject.Find ("levelCompleteText").GetComponent<moveToNextLevel> ().loseLevel();
			GameObject.Find ("levelCompleteText").GetComponent<moveToNextLevel> ().enabled = true;
			this.enabled = false;
		}

		return new Vector3 (0, currentJumpSpeed * Time.deltaTime, 0);

	}

	void reset() {
		mainCam = Camera.main;
		transform.position = new Vector3 (0, 0, 6);
		mainCam.transform.position = new Vector3(0, 2, 0);
		currentJumpSpeed = 0;
		grounded = true;
		falling = false;
		doubleJump = false;
		if (dashing) {
			currentWalkSpeed = walkSpeed;
			dashing = false;
		}
		GetComponent<reset> ().backToOne ();
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
		{
			transform.position += walkInput();
			transform.position = detectSlope();
        }
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(controllerKeys[0])) && grounded && !attacking)
		{
			currentJumpSpeed = jumpSpeed;
			grounded = false;

		}
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(controllerKeys[0])) && !grounded && currentJumpSpeed < doubleJumpSpeed && !doubleJump && !attacking)
        {
            currentJumpSpeed = doubleJumpSpeed;
            doubleJump = true;
            falling = false;
        }
		if ((Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown(controllerKeys[1])) && grounded)
		{
			StartCoroutine (dash ());
		}

        Vector3 jumpResult = jumpInput(transform.position);
		transform.position += jumpResult;
		if (mainCam) {
			mainCam.transform.position += jumpResult;
		}

        collectCol.coinCollision();
        collectCol.goalItemCollision();


        if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
    }

	IEnumerator dash()
	{
		dashing = true;
		float time = 0;
		currentWalkSpeed = dashSpeed;
		while (time < dashTime)
		{
			time += Time.deltaTime;
			yield return null;
		}
		if (grounded) {
			currentWalkSpeed = walkSpeed;
			dashing = false;
		}
		yield return null;
	}
}
