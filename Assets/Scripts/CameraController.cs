using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour {
	[SerializeField] private Vector3 offset;
	[SerializeField] private GameObject ball;

	private void Awake(){
		Assert.IsNotNull(ball);
	}

	void Update(){
		FollowBall();
	}

	private void FollowBall(){
		transform.position = ball.transform.position + offset;
	}
}
