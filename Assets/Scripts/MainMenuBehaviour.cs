using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour {
	private Button playButton,playFree, creditsButton, exitButton;
	//Name of the Arduino port, to check if the port exists
	public string portName = "COM3";
	private bool existsPort = false;
	//Game Objects related to the error panel
	public GameObject errorPanel;
	public Button errorButton;

	// Use this for initialization
	void Start () {
		//Button to start the training mode of the App
		creditsButton = GameObject.Find("GuidedMode").GetComponent<Button>();
		creditsButton.onClick.AddListener(
			delegate{
				if(this.existsPort){
					SceneManager.LoadScene("trackSelectionScene");
				}
				else{
					this.errorPanel.SetActive (true);
				}
			});
		
		//Button to start the free mode of the App
		creditsButton = GameObject.Find("FreeMode").GetComponent<Button>();
		creditsButton.onClick.AddListener(
			delegate{
				if(this.existsPort){
					SceneManager.LoadScene("freeModeScene");
				}
				else{
					this.errorPanel.SetActive (true);
				}
			});
		
		//Button for showing the credits
		creditsButton = GameObject.Find("Credits").GetComponent<Button>();
		creditsButton.onClick.AddListener(
			delegate{
				SceneManager.LoadScene("creditsScene");
			});

		//Button to close the error popup
		this.errorButton.onClick.AddListener(
			delegate{
				this.errorPanel.SetActive (false);
			});

		//Button for exiting the app
		exitButton = GameObject.Find("Exit").GetComponent<Button>();
		exitButton.onClick.AddListener(delegate{Application.Quit();});
	}
	
	// Update is called once per frame
	void Update () {
		//We update if the port exists or not
		this.existsPort = Array.Exists(SerialPort.GetPortNames(), x => x == this.portName);
	}
}
