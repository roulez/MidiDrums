using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour {
	private Button playButton,playFree, creditsButton, exitButton;

	// Use this for initialization
	void Start () {
		//Button to start the free mode of the App
		creditsButton = GameObject.Find("FreeMode").GetComponent<Button>();
		creditsButton.onClick.AddListener(
			delegate{
				SceneManager.LoadScene("freeModeScene");
			});
		
		//Button for showing the credits
		creditsButton = GameObject.Find("Credits").GetComponent<Button>();
		creditsButton.onClick.AddListener(
			delegate{
				SceneManager.LoadScene("creditsScene");
			});

		//Button for exiting the app
		exitButton = GameObject.Find("Exit").GetComponent<Button>();
		exitButton.onClick.AddListener(delegate{Application.Quit();});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
