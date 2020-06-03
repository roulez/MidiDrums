using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehaviour : MonoBehaviour
{
    //Note that is associated with the drum sprite
    public Utilities.DrumParts drumNote;
    //Sprites of the drum part
    public Sprite normalImage;
    public Sprite pressedImage;
    private SpriteRenderer spriteRenderer;
    //Time that the sprite remains in the "pressed" state
    private float timeOnScreen = 0.2f;

    // Use this for initialization
	void Start () {
		this.spriteRenderer = GetComponent<SpriteRenderer>();
	}

    /*
     * Changes the image to the "pressed" status
    */
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
