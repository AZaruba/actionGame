using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinUpdate : MonoBehaviour {

	public Text txt;
	public GameObject scoreSource;
	public int coinsInLevel;
	private int lastCount;

	public float stayTime;
	public float fadeTime;
	public Color textColor;
	public string coinGenerator;
    Coroutine fadeReference;
    bool displayingScore;

	// Use this for initialization
	void Start () {
        coinsInLevel = GameObject.FindGameObjectWithTag(coinGenerator).GetComponent<itemPlacementScript>().getNumCoins();
		txt.text = "0/" + coinsInLevel.ToString();
		txt.color = textColor;
		lastCount = 0;
        displayingScore = false;
	}

    public void resetCount()
    {
        lastCount = 0;
        txt.text = "0/" + coinsInLevel.ToString();
    }

	IEnumerator fadeText() {
        displayingScore = true;
		float time = 0;
		float alphaTime = 0;
		while (time < (fadeTime + stayTime)) {
			time += Time.deltaTime;
			if (time > 1) {
				alphaTime += Time.deltaTime / fadeTime;
				txt.color = new Color (txt.color.r, txt.color.g, txt.color.b, Mathf.Lerp(1,0,alphaTime));
			}
			yield return null;
		}
        displayingScore = false;
        yield return null;
	}

	public void collectCoin () {
        int coinCount = scoreSource.GetComponent<collectCollision> ().getCoinCount();
		if (coinCount != lastCount) {
			txt.GetComponent<UnityEngine.UI.Text> ().text = coinCount.ToString() + "/" + coinsInLevel.ToString();
			lastCount = coinCount;
		}
        // change color of goal item text
        if (coinCount == coinsInLevel)
        {
			foreach (GameObject goalItem in GameObject.FindGameObjectsWithTag("goalEntity")) {
				if (goalItem.name.Contains("blue"))
				  goalItem.GetComponent<goalItemInteraction>().scoreText.color = Color.white;
		    }
        }
        if (displayingScore)
        {
            StopCoroutine(fadeReference);
        }
		txt.color = new Color(textColor.r, textColor.g, textColor.b, 1);
        fadeReference = StartCoroutine (fadeText ());
	}
}
