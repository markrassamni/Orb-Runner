using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlatformController : MonoBehaviour{

	[SerializeField] private Vector3 moveDistance;
	[SerializeField] private Vector3 moveSpeed;

	private Vector3 minPos;
	private Vector3 maxPos;
	private bool moveRight, moveUp, moveForward;
	
	void Start(){
		if (moveDistance.x != 0f || moveSpeed.x != 0f){
			Assert.AreNotEqual(0f, moveDistance.x);
			Assert.AreNotEqual(0f, moveSpeed.x);
		}
		if (moveDistance.y != 0f || moveSpeed.y != 0f){
			Assert.AreNotEqual(0f, moveDistance.y);
			Assert.AreNotEqual(0f, moveSpeed.y);
		}
		if (moveDistance.z != 0f || moveSpeed.z != 0f){
			Assert.AreNotEqual(0f, moveDistance.z);
			Assert.AreNotEqual(0f, moveSpeed.z);
		}
		CalculateMinMax();
		SetStartDirection();
	}
	
	void Update(){
		MoveX();
		MoveY();
		MoveZ();
	}

	private void MoveX(){
		if (moveRight){
			transform.Translate(Vector3.right * moveSpeed.x * Time.deltaTime);
			if (transform.position.x > maxPos.x){
				moveRight = false;
			}
		} else{
			transform.Translate(Vector3.left * moveSpeed.x * Time.deltaTime);
			if (transform.position.x < minPos.x){
				moveRight = true;
			}
		}
	}

	private void MoveY(){
		if (moveUp){
			transform.Translate(Vector3.up * moveSpeed.y * Time.deltaTime);
			if (transform.position.y > maxPos.y){
				moveUp = false;
			}
		} else{
			transform.Translate(Vector3.down * moveSpeed.y * Time.deltaTime);
			if (transform.position.y < minPos.y){
				moveUp = true;
			}
		}
	}

	private void MoveZ(){
		if (moveForward){
			transform.Translate(Vector3.forward * moveSpeed.z * Time.deltaTime);
			if (transform.position.z > maxPos.z){
				moveForward = false;
			}
		} else{
			transform.Translate(Vector3.back * moveSpeed.z * Time.deltaTime);
			if (transform.position.z < minPos.z){
				moveForward = true;
			}
		}
	}

	private void CalculateMinMax(){
		minPos.x = transform.position.x - moveDistance.x;
		maxPos.x = transform.position.x + moveDistance.x;
		minPos.y = transform.position.y - moveDistance.y;
		maxPos.y = transform.position.y + moveDistance.y;
		minPos.z = transform.position.z - moveDistance.z;
		maxPos.z = transform.position.z + moveDistance.z;
	}

	private void SetStartDirection(){
		if (Random.value < 0.5f){
			moveRight = true;
		}
		if (Random.value < 0.5f){
			moveUp = true;
		}
		if (Random.value < 0.5f){
			moveForward = true;
		}
	}
}
