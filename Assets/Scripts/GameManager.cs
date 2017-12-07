using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>{

	[SerializeField] private GameObject obstacleParent;
	[SerializeField] private float fireballSpawnDelay;
	[SerializeField] private Fireball fireballPrefab;
	[SerializeField] private float fireWallSpawnTime;
	[SerializeField] private GameObject fireWallPrefab;
	private Ball player;
	
	IEnumerator Start(){
		player = FindObjectOfType<Ball>();
		StartCoroutine(SpawnFireWall());
		yield return new WaitForSeconds(2f);
		StartCoroutine(SpawnFireball());
	}

	private IEnumerator SpawnFireball(){
		Fireball fireball = Instantiate(fireballPrefab);
		fireball.transform.parent = obstacleParent.transform;
		yield return new WaitForSeconds(fireballSpawnDelay);
		StartCoroutine(SpawnFireball());
	}

	private IEnumerator SpawnFireWall(){
		yield return new WaitForSeconds(fireWallSpawnTime);
		Vector3 spawnPoint = new Vector3(0f, 0f, player.transform.position.z + 25f + Random.Range(-5f, 5f));
		Instantiate(fireWallPrefab, spawnPoint, fireballPrefab.transform.rotation, obstacleParent.transform);
		StartCoroutine(SpawnFireWall());
	}

	public void WinGame(){
		print("Won Game!");
	}

	public void LoseGame(){
		SceneController.Instance.LoadGame();
	}
}
