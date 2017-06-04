using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalItemInteraction : MonoBehaviour {

    private int playerMask = 1 << 10;
    private bool playerNear;
    private coinUpdate coinMetric;

    public TextMesh scoreText;
    public string metricName;

    private void Start()
    {
        playerNear = false;
        scoreText.color = Color.red;
        foreach (GameObject metric in GameObject.FindGameObjectsWithTag("metrics"))
        {
            if (metric.name.Equals(metricName))
                coinMetric = metric.GetComponent<coinUpdate>();
        }
    }

	void Update()
    {
        scoreText.transform.rotation = Camera.main.transform.rotation;
        Collider[] checker = Physics.OverlapSphere(transform.position, 4.0f, playerMask);
        // if there's a character near...
        if (checker.Length > 0)
        {
            playerNear = true;
            // overlay text
            scoreText.text = coinMetric.txt.text;

        } else if (playerNear)
        {
            scoreText.text = "";
            playerNear = false;
        }
    }
}
