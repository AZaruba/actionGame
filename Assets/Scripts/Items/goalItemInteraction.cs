using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalItemInteraction : MonoBehaviour {

    public string success;
    public string failure;
    private int playerMask = 1 << 10;
	
	public void nearItem()
    {
        // if there's a character near...
        if (Physics.OverlapSphere(transform.position, 4.0f, playerMask).Length > 0)
        {

        }
    }
}
