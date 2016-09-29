using UnityEngine;
using System.Collections;

public class Yellow : MonoBehaviour {

	public GameObject tileOn;
	Color tileColor;
	public float colorChange;
	public float timerTIme;

	float timer = 0.0f;

	// Use this for initialization
	void Start () {
		tileColor = tileOn.GetComponent<SpriteRenderer> ().color;
	}

	// Update is called once per frame
	void Update () {
		//change tileOn color once in a while
		timer += Time.deltaTime;
		if (timer >= timerTIme){
			ColorChange ();
			timer = 0.0f;
		}
	}

	void ColorChange(){
		// get tile color and change it
		tileColor = tileOn.GetComponent<SpriteRenderer> ().color;
			tileColor.g -= colorChange;
			tileColor.r += colorChange * 0.7f;
			tileOn.GetComponent<SpriteRenderer> ().color = tileColor;
		//kill tile if one of rgb runs out
		if (tileColor.r < 0f || tileColor.g < 0f || tileColor.b < 0f ){
			Destroy (tileOn);
			//tileOn.GetComponent<SpriteRenderer> ().color = new Vector4(0f,0f,0f,0f);
		}
	}
}

