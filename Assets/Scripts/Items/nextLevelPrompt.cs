using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextLevelPrompt : MonoBehaviour {
	public string promptText;
	public GameObject completeText;

	void OnDestroy() {
		completeText.GetComponent<UnityEngine.UI.Text> ().text = promptText;
	}
}
