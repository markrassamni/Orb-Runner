using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireWall : MonoBehaviour {
	
	[SerializeField] private Vector3 hitForce;
	[SerializeField] private float moveDelay;
	[SerializeField] private bool sideFire;

	public Vector3 HitForce{
		get{ return hitForce; }
	}

	public float MoveDelay{
		get{ return moveDelay; }
	}
	
	void Start(){
		//Bug: looks bad moving with platform, particles move slower than transform
//		MoveWithPlatform();
	}
	
	void Update(){
		if (sideFire){
			transform.Translate(Vector3.left * Time.deltaTime * 8.5f);
		}
	}

	private void MoveWithPlatform(){
		RaycastHit hit;
		Physics.Raycast(transform.position, Vector3.down, out hit, 3f);
		if (hit.transform.CompareTag("Platform")){
			transform.parent = hit.transform;
			transform.position = new Vector3(0f, transform.position.y, transform.position.z);
		}
	}
}
