﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>{

	[SerializeField] private GameObject obstacleParent;
	[SerializeField] private float fireballSpawnDelay;
	[SerializeField] private Fireball fireballPrefab;
	[SerializeField] private float fireWallSpawnTime;
	[SerializeField] private GameObject fireWallPrefab;
	[SerializeField] private GameObject ring;
	[SerializeField] private GameObject pausePanel;
	private Ball player;
	private bool gameOver;
	private bool gameWon;
	private bool paused;


	protected override void Awake(){
		base.Awake();
		Assert.IsNotNull(obstacleParent);
		Assert.IsNotNull(fireballPrefab);
		Assert.IsNotNull(fireballPrefab);
		Assert.IsNotNull(ring);
		Assert.AreNotEqual(0f, fireballSpawnDelay);
		Assert.AreNotEqual(0f, fireWallSpawnTime);
	}
	
	IEnumerator Start(){
		player = FindObjectOfType<Ball>();
		if (fireWallSpawnTime < Mathf.Infinity){
			StartCoroutine(SpawnFireWall());
		}
		yield return new WaitForSeconds(2f);
		if (fireballSpawnDelay < Mathf.Infinity){
			StartCoroutine(SpawnFireball());
		}
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape) && !gameOver && !gameWon){
			Pause();
		}
	}

	private IEnumerator SpawnFireball(){
		if (fireballPrefab.SpawnOffset.z + player.transform.position.z > ring.transform.position.z){
			yield break;
		}
		Fireball fireball = Instantiate(fireballPrefab);
		fireball.transform.parent = obstacleParent.transform;
		yield return new WaitForSeconds(fireballSpawnDelay);
		StartCoroutine(SpawnFireball());
	}

	private IEnumerator SpawnFireWall(){
		yield return new WaitForSeconds(fireWallSpawnTime);
		Vector3 spawnPoint = new Vector3(0f, 0f, player.transform.position.z + 25f + Random.Range(-5f, 5f));
		if (!Physics.Raycast(spawnPoint, Vector3.down, 3f)){
			int moveDirection = Random.Range(0, 2) - 1;
			for (int i = 0; i < 10; i++){
				if (!Physics.Raycast(spawnPoint, new Vector3(0, -1, 0), 3f)){
					spawnPoint.z += moveDirection;
				} else{
					break;
				}
			}
			spawnPoint.z += 10f * moveDirection;
		} else{
			for (float i = 0f; i < 10f; i += .5f){
				if (!Physics.Raycast(new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z + i), Vector3.down, 3f)){
					spawnPoint.z -= 10f;
					break;
				}
				if (!Physics.Raycast(new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z - i), Vector3.down, 3f)){
					spawnPoint.z += 10f;
					break;
				}
			}
		}
		if (spawnPoint.z > ring.transform.position.z){
			yield break;
		}
		Instantiate(fireWallPrefab, spawnPoint, fireWallPrefab.transform.rotation);
		StartCoroutine(SpawnFireWall());
	}

	public void Pause(){
		if(!gameOver) {
			paused = !paused;
			if(paused){
				SoundController.Instance.Pause();
				Time.timeScale = 0f;
				pausePanel.SetActive(true);
			} else {
				SoundController.Instance.UnPause();
				Time.timeScale = 1f;
				pausePanel.SetActive(false);
			}
		}
	}
	
	public void GoToMenu(){
		if(paused){
			Pause();
		}
		SceneController.Instance.LoadMenu();
	}

	public void WinGame(){
		print("Won Game!");
		gameWon = true;
	}

	public void LoseGame(){
		// TODO: remove scene change
		SceneController.Instance.LoadGame();
		gameOver = true;
	}
}
