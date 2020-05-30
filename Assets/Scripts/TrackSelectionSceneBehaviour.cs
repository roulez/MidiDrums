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
				/*var test = MidiFile.Read("Assets/Resources/Sounds/Music/DANCE3.mid");
				var notes = test.GetNotes();
				var tempoMap = test.GetTempoMap();

				foreach (var item in notes)
				{
					var metric = item.TimeAs<MetricTimeSpan>(tempoMap);
					var musical = item.TimeAs<MusicalTimeSpan>(tempoMap);

					Debug.Log("Note: " + item.NoteNumber + " | Metric:  " + metric.Seconds);
					//Debug.Log("Nota: " + item.NoteNumber);
					//Debug.Log("Tiempo: " + item.Time + "| Nota: " + item.NoteNumber + "| Longitud: " + item.Length);
				}*/

				Utilities.setCurrentTrack("Sounds/Music/FirstTrack");
				Utilities.setCurrentTrackName("First Track");
				Utilities.setCurrentTrackDificulty("Beginner");
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
