using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeed;
    private Camera mainCam;

	// Use this for initialization
	void Start () {

    }

    Vector3 walkInput()
    {
        mainCam = Camera.main;
        Vector3 direction = new Vector3();
        Vector3 cameraDirection = new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z);
        direction.z = Input.GetAxis("Vertical");
        direction.x = Input.GetAxis("Horizontal");

        direction.Normalize();
        cameraDirection.Normalize();

        direction *= walkSpeed * Time.deltaTime;
        return direction;
    }
	
	// Update is called once per frame
	void Update () {
        
        transform.position += walkInput();
    }
}
