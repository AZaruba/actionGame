using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public RenderTexture renderTexture;
    public float xSensitivity;
    public float ySensitivity;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
		renderTexture.width = Screen.width / 4;
		renderTexture.height = Screen.height / 4;
		Camera.main.ResetAspect ();
		Camera.main.targetTexture = renderTexture;
    }

    void OnGUI()
    { 
        GUI.depth = 20;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), renderTexture);
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
        transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("RHorizontal") * xSensitivity * Time.deltaTime);
        //transform.RotateAround(player.transform.position, transform.right, Input.GetAxis("RVertical") * ySensitivity * Time.deltaTime);
    }    
}