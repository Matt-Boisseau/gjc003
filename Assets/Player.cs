using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	public Text stepCounter; 
	public float pauseDuration;
	public int stepSize, foodLimit;
	public KeyCode walkUp, walkDown, walkLeft, walkRight;

	private bool moving;
	private int food;

	private void Start() {

		// start with max food
		food = foodLimit;
		stepCounter.text = ""+food;
	}

	private void Update() {
		
		// allow movement input if not currently moving
		if(!moving) {
			Vector2 input = new Vector2(
				(Input.GetKey(walkRight) ? 1 : 0) -
				(Input.GetKey(walkLeft) ? 1 : 0),
				(Input.GetKey(walkUp) ? 1 : 0) -
				(Input.GetKey(walkDown) ? 1 : 0)
			);
			if(input != Vector2.zero) {
				StartCoroutine(move(input));
			}
		}
	}

	private IEnumerator move(Vector2 movement) {
		
		// lock movement input
		moving = true;

		// snap to the predetermined final position to remove rounding errors
		transform.position = (Vector2)transform.position + movement * stepSize;

		// undo movement if collision
		if(Physics2D.OverlapPoint(transform.position)) {
			transform.position -= (Vector3)movement * stepSize;
		}
		else {
			// wait briefly before continuing
			yield return new WaitForSeconds(pauseDuration);

			// lose 1 food
			food--;
			stepCounter.text = ""+food;
		}

		// unlock movement input
		moving = false;
	}
}
