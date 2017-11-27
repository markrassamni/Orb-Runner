using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	[SerializeField] private Vector3 offset;
	[SerializeField] private GameObject ball;
	
	void Update(){
		FollowBall();
	}

	private void FollowBall(){
		transform.position = ball.transform.position + offset;
	}
}
