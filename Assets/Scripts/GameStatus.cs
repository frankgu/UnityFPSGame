using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {

	public Texture2D GameOver;

	// Use this for initialization
	void Start () {
	
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.F1))
		   Application.LoadLevel (0); 

	}

	void OnGUI() {
		if (GameObject.FindWithTag ("Target").transform.childCount == 0) 
		{
			GUI.DrawTexture(new Rect(Screen.width / 2 - GameOver.width /2, 
			                         Screen.height / 2 - GameOver.height /2, 
			                         GameOver.width,GameOver.height),
			                GameOver);
		}

	}

	
}
