using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public RenderTexture renderTexture;

    void Start()
    {

    }

    void OnGUI()
    { 
        GUI.depth = 20;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), renderTexture);
    }

    void Update()
    {
        transform.LookAt(GameObject.Find("Player").transform);
    }    
}