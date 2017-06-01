using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalItemInteraction : MonoBehaviour {

    public string success;
    public string failure;
    private int playerMask = 1 << 10;
    private bool playerNear;

    public TextMesh scoreText;

    private void Start()
    {
        playerNear = false;
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
            scoreText.text = GameObject.FindGameObjectWithTag("metrics").GetComponent<coinUpdate>().txt.text;

        } else if (playerNear)
        {
            scoreText.text = "";
            playerNear = false;
        }
    }
}
