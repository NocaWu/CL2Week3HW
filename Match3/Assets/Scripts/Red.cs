using UnityEngine;
using System.Collections;

public class Red : MonoBehaviour {

	public GameObject tileOn;
	Color tileColor;
	public float colorChange;
	public float timerTime;

	float timer = 9.9f;
	float timeSurvive = 0.0f;

	// Use this for initialization
	void Start () {
		//gameManager = GameObject.Find ("GameManager");
		tileColor = tileOn.GetComponent<SpriteRenderer> ().color;
	}

	// Update is called once per frame
	void Update () {
		//change tileOn color once in a while
		timer += Time.deltaTime;
		if (timer >= timerTime){
			ColorChange ();
			timer = 0.0f;
		}

//		timeSurvive += Time.deltaTime;
//		if (timeSurvive >= timerTime * 10){
//			ColorPlus ();
//			timeSurvive = 0.0f;
//		}
	}

	void ColorChange(){
		// get tile color and change it
		tileColor = tileOn.GetComponent<SpriteRenderer> ().color;
			tileColor.r -= colorChange;
			tileColor.b += colorChange * 0.7f;
			tileOn.GetComponent<SpriteRenderer> ().color = tileColor;
		//kill tile if one of rgb runs out
		if (tileColor.r < 0f || tileColor.g < 0f || tileColor.b < 0f ){
			Destroy (tileOn);
			//tileOn.GetComponent<SpriteRenderer> ().color = new Vector4(0f,0f,0f,0f);
		}
	}


	void ColorPlus(){
		tileColor = tileOn.GetComponent<SpriteRenderer> ().color;
		tileColor.r += 2 * colorChange;
		tileColor.g += 2 * colorChange;
		tileColor.b += 2 * colorChange;
		tileOn.GetComponent<SpriteRenderer> ().color = tileColor;
	}
}
