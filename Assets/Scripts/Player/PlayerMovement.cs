using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeed;
    private Camera mainCam;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
    }

    Vector3 walkInput()
    {

        Vector3 direction = new Vector3();
        Vector3 cameraDirection = new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z);
        direction.z = Input.GetAxis("Vertical");
        direction.x = Input.GetAxis("Horizontal");

        //direction.Normalize();
        //cameraDirection.Normalize();

        direction *= walkSpeed * Time.deltaTime;
        mainCam.transform.position += direction;
        transform.forward = cameraDirection - direction;
        return direction;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            transform.position += walkInput();
        }
    }
}
