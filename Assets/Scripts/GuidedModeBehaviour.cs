﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuidedModeBehaviour : MonoBehaviour
{
	private MidiDrumScript midiCrontoller;

	//Array with the inputs where the notes can be played
	private InputBehaviour[] drumParts;

	//Score of the player whith the notes pressed
	private int missedNotes;
	private int onBeatNotes;
	private int offBeatNotes;

	//Number of notes in total
	private int totalNotes;

	//Varible to know if the track is paused
	private bool isPaused;
	private bool isStarted = false;

	//We create an instance of the class so the notes can acces the code
	public static GuidedModeBehaviour instance;

	//Audio clip of the background music for the track
	public AudioSource musicTrack;

	//Time before the music starts and between scene change
	public float waitingTime = 3.5f;
	public float endTime = 1.5f;

	//Pàuse menu items
	public GameObject pauseMenu;
	public Button resumeButton, restartButton, exitButton, pauseButton;

    // Start is called before the first frame update
    void Start()
    {
		//Images that represent the differnt parts of the Drum
		this.drumParts = FindObjectsOfType<InputBehaviour> ();
		
		this.midiCrontoller = new MidiDrumScript (this.gameObject);
		this.missedNotes = 0;
		this.onBeatNotes = 0;
		this.offBeatNotes = 0;

		this.totalNotes = FindObjectsOfType<NoteBehaviour> ().Length;

		this.isPaused = false;

		//Funcionality for the resume button in the pause menu
		resumeButton.onClick.AddListener(
			delegate{
				this.musicTrack.Play ();
				this.pauseMenu.SetActive (false);
				this.isPaused = false;
			});

		//Funcionality for the exit button in the pause menu
		exitButton.onClick.AddListener(
			delegate{
				//Before changing the scenes we need to close the port so we can open it later
				this.midiCrontoller.closePort();
				SceneManager.LoadScene("trackSelectionScene");
			});

		//Funcionality for the restart button in the pause menu
		restartButton.onClick.AddListener(
			delegate{
				//Before changing the scenes we need to close the port so we can open it later
				this.midiCrontoller.closePort();
				SceneManager.LoadScene(Utilities.getCurrentTrackScene ());
			});

		//Funcionality for the button to pause the play mode
		pauseButton.onClick.AddListener(
			delegate{
				if(!this.isPaused){
					this.musicTrack.Pause ();
					this.pauseMenu.SetActive (true);
					this.isPaused = true;
				}
			});

		StartCoroutine(PlaySong());

		/*//We load the audio from the track that is selected and we play it
		this.musicTrack.clip = Resources.Load (Utilities.getCurrentTrack()) as AudioClip;
		this.musicTrack.Play ();

		this.isStarted = true;*/
		//We set the value of the instance
		instance = this;
    }

    // Update is called once per frame
    void Update()
    {
		try{
			//When the user presses the escape key, we pause the game
			if (Input.GetKeyDown (KeyCode.Escape)) {
				this.musicTrack.Pause ();
				this.pauseMenu.SetActive (true);
				this.isPaused = true;
			}

			//If the music is not paused and the track is not paused, the the game is finished
			if (this.isStarted && !this.isPaused && !this.musicTrack.isPlaying) {
				//Before changing the scenes we need to close the port so we can open it later
				this.midiCrontoller.closePort();

				//We save the score earned to show it in the result scene
				Utilities.setTotalNotes(this.totalNotes);
				Utilities.setPerfectNotes(this.onBeatNotes);
				Utilities.setGoodNotes(this.offBeatNotes);
				Utilities.setMissedNotes(this.missedNotes);

				//We change the scene to show the results
				StartCoroutine(EndTrack());
			}

		}catch(System.Exception ex){
			Debug.Log (ex);
		}
    }


	/*
	 * We read the data from the arduino and we return to the note if the note has been pressed
	*/
	public bool readArduino(int sensor){
		try{
			var arduinoInput = this.midiCrontoller.readMultiplePorts(sensor);

			//If is not empty a note has been pressed
			if (arduinoInput != "") {
				var aux = arduinoInput.Split ('-');

				int note = int.Parse(aux [0]);
				int volume = int.Parse(aux [1]);

				for(int i = 0; i < this.drumParts.Length; i++){
					if((int)this.drumParts[i].drumNote == note){
						this.drumParts[i].changeImage();
					}
				}

				//If the note pressed is the correct one, we play the note sound and we return that is has been pressed
				if (sensor == note) {
					this.midiCrontoller.playSound (sensor);
					return true;
				}
			}
			return false;
		}
		catch(System.Exception ex){
			Debug.Log (ex);
			return false;
		}
	}

	/*
	 * We call this method when a note is been hit with perfect timing
	*/
	public void noteOnBeat(){
		this.onBeatNotes++;
		Debug.Log ("Note On Beat: " + this.onBeatNotes);
	}

	/*
	 * We call this method when a note is been hit with a slightly off timing
	*/
	public void noteOffBeat(){
		this.offBeatNotes++;
		Debug.Log ("Note Off Beat: " + this.offBeatNotes);
	}

	/*
	 * We call this method when a note is not played in the correct time
	*/
	public void noteMissed(){
		this.missedNotes++;
		Debug.Log ("Note Missed: " + this.missedNotes);
	}

	public bool getIsPaused(){
		return this.isPaused;
	}

	/*
	 * Plays the song with a specified retard
	*/
	IEnumerator PlaySong(){
		yield return new WaitForSeconds(this.waitingTime);
		this.musicTrack.clip = Resources.Load (Utilities.getCurrentTrack()) as AudioClip;
		this.musicTrack.Play ();

		this.isStarted = true;
	}

	/*
	 * A few seconds after the song is over brings up the result screen
	*/
	IEnumerator EndTrack(){
		yield return new WaitForSeconds(this.endTime);
		SceneManager.LoadScene("resultsScene");
	}
}
