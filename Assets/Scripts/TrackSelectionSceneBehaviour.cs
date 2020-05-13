using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrackSelectionSceneBehaviour : MonoBehaviour
{
	private Button backButton;

	// Use this for initialization
	void Start () {
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
