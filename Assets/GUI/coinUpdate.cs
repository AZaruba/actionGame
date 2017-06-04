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
    Coroutine fadeReference;
    bool displayingScore;

	// Use this for initialization
	void Start () {
        coinsInLevel = GameObject.FindGameObjectWithTag("coinGen").GetComponent<itemPlacementScript>().getNumCoins();
		txt.text = "0/" + coinsInLevel.ToString();
		txt.color = new Color (Color.blue.r, Color.blue.g, Color.blue.b, 0);
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
            GameObject.FindGameObjectWithTag("goalEntity").GetComponent<goalItemInteraction>().scoreText.color = Color.white;
        }
        if (displayingScore)
        {
            StopCoroutine(fadeReference);
        }
        txt.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 1);
        fadeReference = StartCoroutine (fadeText ());
	}
}
