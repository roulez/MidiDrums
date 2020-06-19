using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

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
				//We save the data of the track in the Utilities class so we can have acces to it in the different scenes
				Utilities.setCurrentTrack("Sounds/Music/FirstTrack");
				Utilities.setCurrentTrackName("First Track");
				Utilities.setCurrentTrackDificulty("Beginner");
				Utilities.setCurrentTrackScene("firstTrackScene");
				//We load the scene thaat the user has picked
				SceneManager.LoadScene("firstTrackScene");
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
