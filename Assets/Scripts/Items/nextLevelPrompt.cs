using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextLevelPrompt : MonoBehaviour {
	public string promptText;
	public GameObject completeText;

	void OnDestroy() {
		if (completeText != null)
		    completeText.GetComponent<UnityEngine.UI.Text> ().text = promptText;
	}
}
