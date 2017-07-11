using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerUpdate : MonoBehaviour {

    public Text txt;
    public float time;
    private int minutes;
    private int seconds;
    private int millis;

    private bool finished;

    public void setFinished()
    {
        finished = true;
    }

	// Use this for initialization
	void Start () {
        time = 0;
        finished = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (finished)
            enabled = false;
        time += Time.deltaTime;

        minutes = (int) time / 60;
        seconds = (int) time % 60;
        millis = (int)(time * 100) % 100;

        txt.text = string.Format("{0:0} : {1:00} : {2:00}", minutes, seconds, millis);
	}
}
