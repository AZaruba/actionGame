using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectCollision : MonoBehaviour {

	private RaycastHit coinHit;
	private Collider[] hitArray;
	private int layerMask;

	// Use this for initialization
	void Start () {
		layerMask = 1 << 8;
	}

    public void coinCollision()
    {
        hitArray = Physics.OverlapSphere(transform.position, 1.0f, layerMask);
        if (hitArray.Length > 0)
        {
            Destroy(hitArray[0].gameObject);
        }
    }
}
