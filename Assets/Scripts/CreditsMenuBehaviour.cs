using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsMenuBehaviour : MonoBehaviour {
	private Button backButton;

	// Use this for initialization
	void Start () {
		backButton = GameObject.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener (delegate {
			SceneManager.LoadScene ("mainMenu");
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
