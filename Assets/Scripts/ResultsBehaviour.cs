using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsBehaviour : MonoBehaviour
{
	public Text trackName, trackDificulty;
	public Text totalNotes;
	public Text perfectNotes,goodNotes,missedNotes;
	public Text percentageHit;

	public Button replayButton,exitButton;

	private float percentageHitted;

    // Start is called before the first frame update
    void Start()
    {
		this.percentageHitted = 0;

		//Funcionality for the replay button
		replayButton.onClick.AddListener(
			delegate{
				SceneManager.LoadScene(Utilities.getCurrentTrackScene ());
			});

		//Funcionality for the exit button
		exitButton.onClick.AddListener(
			delegate{
				SceneManager.LoadScene("trackSelectionScene");
			});

		//We set the info regarding the track
		this.trackName.text = Utilities.getCurrentTrackName ();
		this.trackDificulty.text = Utilities.getCurrentTrackDificulty ();

		//We set the number of notes of the track
		float totalNotes = Utilities.getTotalNotes ();
		this.totalNotes.text = totalNotes.ToString();

		//We set the score of the track
		float perfectNotes = Utilities.getPerfectNotes ();
		this.perfectNotes.text = perfectNotes.ToString();
		float goodNotes = Utilities.getGoodNotes ();
		this.goodNotes.text = goodNotes.ToString();
		float missedNotes = Utilities.getMissedNotes ();
		this.missedNotes.text = missedNotes.ToString();

		//We set the data regarding the percentage of notes hitted
		this.percentageHitted = ((perfectNotes + goodNotes) / totalNotes) * 100;
		this.percentageHit.text = percentageHitted.ToString() + "%";

		//Depending of the percentage we change the color of the text
		if (this.percentageHitted >= 90) {
			this.percentageHit.color = Color.green;
		} else if (this.percentageHitted < 50) {
			this.percentageHit.color = Color.red;
		} else {
			this.percentageHit.color = Color.cyan;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
