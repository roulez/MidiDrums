using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBeatsBehaviour : MonoBehaviour
{
	//Controls the speed of the notes
	public float notesSpeed;

    // Start is called before the first frame update
    void Start()
    {
		this.notesSpeed = this.notesSpeed / 60f;
    }

    // Update is called once per frame
    void Update()
    {
		if (!GuidedModeBehaviour.instance.getIsPaused()) {
			transform.position -= new Vector3 (0f, this.notesSpeed * Time.deltaTime, 0f); 
		}
    }
}
