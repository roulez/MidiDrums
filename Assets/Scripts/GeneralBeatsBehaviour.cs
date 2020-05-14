using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBeatsBehaviour : MonoBehaviour
{
	//Controls the speed of the notes
	public float notesSpeed;
	//Control if the application is paused
	private bool isPaused;
	public GameObject mainCamera;
	public GuidedModeBehaviour mainScript;

    // Start is called before the first frame update
    void Start()
    {
		this.isPaused = false;
		this.notesSpeed = this.notesSpeed / 60f;

		this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		this.mainScript = this.mainCamera.GetComponent<GuidedModeBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!this.isPaused) {
			transform.position -= new Vector3 (0f, this.notesSpeed * Time.deltaTime, 0f); 
		}
    }
}
