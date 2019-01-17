using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
    
	public Transform objectToFollow;

	private void Update() {
		Vector3 targetPosition = new Vector3(objectToFollow.position.x, objectToFollow.position.y, 0);
		Vector3 cameraPosition = new Vector3(this.transform.position.x, this.transform.position.y, 0);
		transform.position += (targetPosition - cameraPosition) * Time.deltaTime;
	}
}
