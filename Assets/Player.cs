using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public float stepDuration, pauseDuration;
	public int stepSize;
	public KeyCode walkUp, walkDown, walkLeft, walkRight;

	private bool moving;

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

		// get final position (will snap to this after animation)
		Vector2 targetPosition = (Vector2)transform.position + movement * stepSize;

		// animate movement (and by that I mean just slide over)
		float time = 0;
		while(time < stepDuration) {
			time += Time.deltaTime;
			transform.Translate((stepSize / stepDuration) * movement * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}

		// snap to the predetermined final position to remove rounding errors
		transform.position = targetPosition;

		// wait briefly before continuing
		yield return new WaitForSeconds(pauseDuration);

		// unlock movement input
		moving = false;
	}
}
