using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	[SerializeField] private float speedX;
	[SerializeField] private float speedZ;
	[SerializeField] private float jumpForce;
	private Rigidbody rb;
	private bool stopMoving;
	private float timeToMove;
	private const float max_move_delay = 2f;

	void Awake(){
		rb = GetComponent<Rigidbody>();
		SoundController.Instance.SetSfxSource(GetComponent<AudioSource>());
	}

	void Update(){
		if (stopMoving){
			timeToMove -= Time.deltaTime;
			if (timeToMove <= 0f){
				stopMoving = false;
				timeToMove = 0f;
			}
		}
	}

	void FixedUpdate(){
		if(!stopMoving)
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
		if(Input.GetKey(KeyCode.Space) && rb.velocity.y < .05f && rb.velocity.y > -.05f){
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	private void Collide(float moveDelay){
		stopMoving = true;
		if (timeToMove + moveDelay < max_move_delay){
			timeToMove += moveDelay;
		} else{
			timeToMove = max_move_delay;
		}
	}

	private void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag("Weapon")){
			Hammer hammer = other.gameObject.GetComponentInParent<Hammer>();
			Collide(hammer.MoveDelay);
		}
		if (other.gameObject.CompareTag("Ring")){
			rb.velocity = Vector3.zero;
			Collide(1.5f);
		}
	}

	private void OnTriggerEnter(Collider other){
		if (other.CompareTag("GameOver")){
			GameManager.Instance.LoseGame();
		}
		if (other.CompareTag("Fireball")){
			Fireball fireball = other.GetComponent<Fireball>();
			Collide(fireball.MoveDelay);
			rb.velocity = Vector3.zero;
			rb.AddForce(Vector3.up * fireball.HitForce.y, ForceMode.Impulse);
			if (Random.value < 0.5){
				rb.AddForce(Vector3.left * fireball.HitForce.x, ForceMode.Impulse);
			} else{
				rb.AddForce(Vector3.right * fireball.HitForce.x, ForceMode.Impulse);
			}
			if (Random.value < 0.5){
				rb.AddForce(Vector3.forward * fireball.HitForce.z, ForceMode.Impulse);
			} else{
				rb.AddForce(Vector3.back * fireball.HitForce.z, ForceMode.Impulse);
			}
		}
		if (other.CompareTag("GameWon")){
			GameManager.Instance.WinGame();
		}
		if (other.CompareTag("WallOfFire")){
			FireWall fireWall = other.GetComponentInParent<FireWall>();
			Collide(fireWall.MoveDelay);
			rb.velocity = Vector3.zero;
			rb.AddForce(Vector3.up * fireWall.HitForce.y, ForceMode.Impulse);
			if (Random.value < 0.5){
				rb.AddForce(Vector3.left * fireWall.HitForce.x, ForceMode.Impulse);
			} else{
				rb.AddForce(Vector3.right * fireWall.HitForce.x, ForceMode.Impulse);
			}
			if (Random.value < 0.5){
				rb.AddForce(Vector3.forward * fireWall.HitForce.z, ForceMode.Impulse);
			} else{
				rb.AddForce(Vector3.back * fireWall.HitForce.z, ForceMode.Impulse);
			}
		}
	}
}
