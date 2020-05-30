using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float timeOnScreen = 0.2f;
    public Utilities.DrumParts drumNote;
    public Sprite normalImage;
    public Sprite pressedImage;

    // Use this for initialization
	void Start () {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
	}

    public void changeImage(){
        StartCoroutine(ManageImagery());
    }

    /*
	 * Shows the image of the drum part that has been hited
	*/
	IEnumerator ManageImagery(){
		this.spriteRenderer.sprite = pressedImage;
		yield return new WaitForSeconds(this.timeOnScreen);
		this.spriteRenderer.sprite = normalImage;
	}
}
