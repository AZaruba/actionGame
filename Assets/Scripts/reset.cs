﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour {

	public GameObject player;
	public GameObject coinGen;

	public void backToOne() {
		player.GetComponent<collectCollision> ().setCoinCount (0);
		coinGen.GetComponent<itemPlacementScript> ().genCoins ();
	}
}
