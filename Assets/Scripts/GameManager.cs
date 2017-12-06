using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>{

	[SerializeField] private GameObject obstacleParent;
	[SerializeField] private float fireballSpawnDelay;
	[SerializeField] private Fireball fireballPrefab;
	
	void Start(){
		StartCoroutine(SpawnFireball());
	}

	private IEnumerator SpawnFireball(){
		yield return new WaitForSeconds(fireballSpawnDelay);
		Fireball fireball = Instantiate(fireballPrefab);
		fireball.transform.parent = obstacleParent.transform;
		StartCoroutine(SpawnFireball());
		yield return new WaitForSeconds(5f);
	}

	public void WinGame(){
		print("Won Game!");
	}

	public void LoseGame(){
		SceneController.Instance.LoadGame();
	}
}
