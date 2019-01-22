using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnim : MonoBehaviour {
	
	public Text stepCounter; 
	public float pauseDuration;
	public int stepSize, foodLimit;
	public KeyCode walkUp, walkDown, walkLeft, walkRight;
    private Animator animator;
	

	private bool moving;
	private int food;
	private bool animate;
	private bool skip;

	private void Start() {

		// start with max food
		food = foodLimit;
		stepCounter.text = ""+food;

        animator = this.GetComponent<Animator>();
		animate = true;
		skip = false;
	}

	private void Update() {
		
		// allow movement input if not currently moving
		if (skip == false){	
			if(!moving) {
				Vector2 input = new Vector2(
					(Input.GetKey(walkRight) ? 1 : 0) -
					(Input.GetKey(walkLeft) ? 1 : 0),
					(Input.GetKey(walkUp) ? 1 : 0) -
					(Input.GetKey(walkDown) ? 1 : 0)
				);
					if (animate == true){

					if (input == new Vector2(1,0)) {//r
						if (animator.GetInteger("walkInt") == 1){
							animator.SetInteger("walkInt", 3);
						}
						else{
						animator.SetInteger("walkInt", 1);
						}
						} 
					if (input == new Vector2(-1,0)) {//l
						if (animator.GetInteger("walkInt") == -1){
							animator.SetInteger("walkInt", -3);
						}
						else{
						animator.SetInteger("walkInt", -1);
						}
						} 
					if (input == new Vector2(0,1)) {//u
						if (animator.GetInteger("walkInt") == 2){
							animator.SetInteger("walkInt", 4);
						}
						else{
						animator.SetInteger("walkInt", 2);
						}
						}
					if  (input == new Vector2(0,-1)) {//d
						if (animator.GetInteger("walkInt") == -2){
							animator.SetInteger("walkInt", -4);
						}
						else{
						animator.SetInteger("walkInt", -2);
						}
						} 
					}	
					// STUFF BELOW THIS CAUSES DROP TO idle WHEN BUTTON NOT PRESSED
				//if  (input == new Vector2(0,0)) {
					//animator.SetInteger("walkInt", 0);
					//} //i

				if(input != Vector2.zero) {
					StartCoroutine(move(input));
				}
			}
		}
	}

	private IEnumerator move(Vector2 movement) {

		skip= true;// prevent from running again while waiting one frame
		yield return 1; // wait one frame so animation does not jump
		
		skip = false;

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