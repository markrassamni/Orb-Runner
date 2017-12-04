using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	[SerializeField] private float speedX;
	[SerializeField] private float speedZ;
	[SerializeField] private float jumpForce;
	private Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate(){
		Move();
	}

	private void Move(){
		if (Input.GetKey("s") || Input.GetKey("down")){
			rb.velocity = new Vector3(0f, rb.velocity.y, speedZ/2f);
		} else{
			rb.velocity = new Vector3(0f, rb.velocity.y, speedZ);
		}
		if (Input.GetKey("left") || Input.GetKey("a")){
			rb.velocity = new Vector3(-speedX, rb.velocity.y, rb.velocity.z);
		} 
		if (Input.GetKey("right") || Input.GetKey("d")){
			rb.velocity = new Vector3(speedX, rb.velocity.y, rb.velocity.z);
		}
		if(Input.GetKey(KeyCode.Space) && rb.velocity.y == 0f){
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	private void OnTriggerEnter(Collider other){
		if (other.CompareTag("GameOver")){
			SceneController.Instance.LoadGame();
		}
	}
}
