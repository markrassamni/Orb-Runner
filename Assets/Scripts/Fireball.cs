using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour{

	[SerializeField] private float moveSpeed;
	[SerializeField] private float shootDelay;
	[SerializeField] private float alphaChangeSpeed;
	[SerializeField] private Vector3 spawnOffset;
	[SerializeField] private Vector3 hitForce;
	[SerializeField] private float moveDelay;
	private GameObject player;
	private ExplosionMat explosionMat;
	private bool shooting;
	private Vector3 target;

	public float MoveDelay{
		get{ return moveDelay; }
	}

	public Vector3 HitForce{ get{ return hitForce; } }

	void Start(){
		player = FindObjectOfType<Ball>().gameObject;
		explosionMat = GetComponent<ExplosionMat>();
		explosionMat._alpha = 0f;
		SetSpawnPoint();
		StartCoroutine(Shoot());
	}

	private IEnumerator Shoot(){
		yield return new WaitForSeconds(shootDelay);
		shooting = true;
		target = new Vector3(player.transform.position.x + Random.Range(-2f, 2f), -.5f, transform.position.z + 5f);
		yield return new WaitForSeconds(6f);
		Destroy(gameObject);
	}

	private void SetSpawnPoint(){
		if (Random.value < 0.5){
			transform.position = player.transform.position + new Vector3(-spawnOffset.x, spawnOffset.y, spawnOffset.z);
		} else{
			transform.position = player.transform.position + new Vector3(spawnOffset.x, spawnOffset.y, spawnOffset.z);
		}
	}
	
	void Update(){
		if (shooting){
			transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
		}
	}
}
