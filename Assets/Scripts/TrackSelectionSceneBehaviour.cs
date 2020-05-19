using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrackSelectionSceneBehaviour : MonoBehaviour
{
	private Button backButton, firstTrack;

	// Use this for initialization
	void Start () {
		//Buttons for the track selection
		//First Track of the app
		firstTrack = GameObject.Find("FirstTrack").GetComponent<Button>();
		firstTrack.onClick.AddListener(
			delegate{
				Utilities.setCurrentTrack("Sounds/Music/FirstTrack");
				SceneManager.LoadScene("guidedModeScene");
			});
		
		//Button to start the training mode of the App
		backButton = GameObject.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(
			delegate{
				SceneManager.LoadScene("mainMenu");
			});
	}

	// Update is called once per frame
	void Update () {

	}
}
