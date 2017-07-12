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

    public Color timerColor;
    public Color pauseColor;

    private bool finished;
    private bool paused;

    private float iterator;
    private float pauseTime;

    public void setFinished()
    {
        finished = true;
    }

    public void setPaused()
    {
        paused = true;
        pauseTime += 1.0f;
    }

    IEnumerator pause() {
        iterator = 0;

        while (iterator < pauseTime)
        {
            iterator += Time.deltaTime;
            yield return null;
        }
        iterator = 0;
        pauseTime = 0;
        paused = false;
        txt.color = timerColor;
        yield return null;
    }

    // Use this for initialization
    void Start () {
        time = 0;
        finished = false;
        paused = false;
        pauseTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (finished)
            enabled = false;
        if (!paused)
        {
            time += Time.deltaTime;

            minutes = (int)time / 60;
            seconds = (int)time % 60;
            millis = (int)(time * 100) % 100;

            txt.text = string.Format("{0:0} : {1:00} : {2:00}", minutes, seconds, millis);
        }
        else
        {
            txt.color = pauseColor;
            StartCoroutine(pause());
        }
	}
}
