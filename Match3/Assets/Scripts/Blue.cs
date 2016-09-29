using UnityEngine;
using System.Collections;

public class Blue : MonoBehaviour {

	public GameObject tileOn;

//	GameObject tileUp;
//	GameObject tileDown;
//	GameObject tileLeft;
//	GameObject tileRIght;
	GameObject gameManager;

	protected GameManagerScript gameManagerScript;
	 
	Color tileColor;
	public float colorChange;
	public float timerTIme;

	float timer = 0.0f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		tileColor = tileOn.GetComponent<SpriteRenderer> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= timerTIme){
			ColorChange ();
			timer = 0.0f;
		}
	}

	void ColorChange(){
		if(tileColor.b >= 0f){
			tileColor.b -= colorChange;
			tileColor.g += colorChange * 0.7f;
			tileOn.GetComponent<SpriteRenderer> ().color = tileColor;
		} else if (tileColor.b < 0f){
			//tileOn.GetComponent<SpriteRenderer> ().color = new Vector4(0f,0f,0f,0f);
			Destroy (tileOn);
		}
	}
}
