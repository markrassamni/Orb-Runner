using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Obstacle : MonoBehaviour{

	[SerializeField] private Vector3 rotateSpeed;
	[SerializeField] private Vector3 rotateAngle;

	private Vector3 minRotation;
	private Vector3 maxRotation;
	private bool rotatePosX, rotatePosY, rotatePosZ;
	
	void Start(){
		if (rotateSpeed.x != 0f || rotateAngle.x != 0f){
			Assert.AreNotEqual(0f, rotateAngle.x);
			Assert.AreNotEqual(0f, rotateSpeed.x);
		}
		if (rotateSpeed.y != 0f || rotateAngle.y != 0f){
			Assert.AreNotEqual(0f, rotateAngle.y);
			Assert.AreNotEqual(0f, rotateSpeed.y);
		}
		if (rotateSpeed.z != 0f || rotateAngle.z != 0f){
			Assert.AreNotEqual(0f, rotateAngle.z);
			Assert.AreNotEqual(0f, rotateSpeed.z);
		}
		CalculateMinMax();
	}
	
	void Update(){
		Rotate();
	}
	
	private void Rotate(){
		if (rotatePosX){
			transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed.x);
			if (transform.rotation.x >= maxRotation.x){
				rotatePosX = false;
			}
		} else{
			transform.Rotate(Vector3.left * Time.deltaTime * rotateSpeed.x);
			if (transform.rotation.x <= minRotation.x){
				rotatePosX = true;
			}
		}
		if (rotatePosY){
			transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed.y);
			if (transform.rotation.y >= maxRotation.y){
				rotatePosY = false;
			}
		} else{
			transform.Rotate(Vector3.down * Time.deltaTime * rotateSpeed.y);
			if (transform.rotation.y <= minRotation.y){
				rotatePosY = true;
			}
		}
		
		if (rotatePosZ){
			transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed.z);
			if (transform.rotation.z >= maxRotation.z){
				rotatePosZ = false;
			}
		} else{
			transform.Rotate(Vector3.back * Time.deltaTime * rotateSpeed.z);
			if (transform.rotation.z <= minRotation.z){
				rotatePosZ = true;
			}
		}
	}
	
	private void CalculateMinMax(){
		if (rotateAngle.x < transform.eulerAngles.x){
			minRotation.x = (Quaternion.Euler(rotateAngle.x, transform.rotation.y, transform.rotation.z) * transform.rotation).x;
			maxRotation.x = transform.rotation.x;
			rotatePosX = false;
		} else if (rotateAngle.x > transform.eulerAngles.x){
			maxRotation.x = (Quaternion.Euler(rotateAngle.x, transform.rotation.y, transform.rotation.z) * transform.rotation).x;
			minRotation.x = transform.rotation.x;
			rotatePosX = true;
		}
		if (rotateAngle.y < transform.eulerAngles.y){
			minRotation.y = (Quaternion.Euler(transform.rotation.x, rotateAngle.y, transform.rotation.z) * transform.rotation).y;
			maxRotation.y = transform.rotation.y;
			rotatePosY = false;
		} else if (rotateAngle.y > transform.eulerAngles.y){
			minRotation.y = transform.rotation.y;
			maxRotation.y = (Quaternion.Euler(transform.rotation.x, rotateAngle.y, transform.rotation.z) * transform.rotation).y;
			rotatePosZ = true;
		}
		if (rotateAngle.z < transform.eulerAngles.z){
			minRotation.z = (Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotateAngle.z) * transform.rotation).z;
			maxRotation.z = transform.rotation.z;
			rotatePosZ = false;
		} else if (rotateAngle.z > transform.eulerAngles.z){
			minRotation.z = transform.rotation.z;
			maxRotation.z = (Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotateAngle.z) * transform.rotation).z;
			rotatePosZ = true;
		}
	}
}
