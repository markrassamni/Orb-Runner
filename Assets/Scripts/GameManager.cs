using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>{

	[SerializeField] private GameObject obstacleParent;
	[SerializeField] private float fireballSpawnDelay;
	[SerializeField] private Fireball fireballPrefab;
	[SerializeField] private float fireWallSpawnTime;
	[SerializeField] private GameObject fireWallPrefab;
	[SerializeField] private GameObject ring;
	[SerializeField] private GameObject pausePanel;
	[SerializeField] private GameObject gameOverPanel;
	[SerializeField] private Button changeSceneButton;
	[SerializeField] private Text winLoseText;
	[SerializeField] private GameObject stopMovingGlow;
 	private Ball player;
	private bool gameOver;
	private bool gameWon;
	private bool paused;

	private delegate void ChangeScene();
	private event ChangeScene changeScene;

	protected override void Awake(){
		base.Awake();
		Assert.IsNotNull(obstacleParent);
		Assert.IsNotNull(fireballPrefab);
		Assert.IsNotNull(fireballPrefab);
		Assert.IsNotNull(ring);
		Assert.IsNotNull(gameOverPanel);
		Assert.IsNotNull(pausePanel);
		Assert.IsNotNull(stopMovingGlow);
		Assert.AreNotEqual(0f, fireballSpawnDelay);
		Assert.AreNotEqual(0f, fireWallSpawnTime);
	}
	
	IEnumerator Start(){
		stopMovingGlow.SetActive(false);
		StartCoroutine(RemovePastObstacles());
		changeScene = SceneController.Instance.ReloadScene;
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
		if(gameOver || gameWon) yield break;
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
		if(gameOver || gameWon || spawnPoint.z > ring.transform.position.z) yield break;
		Instantiate(fireWallPrefab, spawnPoint, fireWallPrefab.transform.rotation, obstacleParent.transform);
		StartCoroutine(SpawnFireWall());
	}

	private IEnumerator RemovePastObstacles(){
		yield return new WaitForSeconds(8f);
		foreach (Transform child in obstacleParent.transform){
			if (child.position.z < player.transform.position.z - 20f){
				Destroy(child.gameObject);
			}
		}
		if(gameOver || gameWon) yield break;
		StartCoroutine(RemovePastObstacles());
	}

	public void Pause(){
		if(gameOver) return;
		paused = !paused;
		if(paused){
			SoundController.Instance.Pause();
			Time.timeScale = 0f;
			pausePanel.SetActive(true);
		} else{
			SoundController.Instance.UnPause();
			Time.timeScale = 1f;
			pausePanel.SetActive(false);
		}
	}

	public void ChangeSceneClicked(){
		if (changeScene == null){
			Debug.LogError("changeScene Delegate Not Set!");
			return;
		}
		if (paused){
			gameOver = false;
			Pause();
		}
		changeScene();
	}
	
	public void GoToMenu(){
		if(paused){
			Pause();
		}
		SceneController.Instance.LoadMenu();
	}

	public void WinGame(){
		gameWon = true;
		if (SceneController.Instance.IsCurrentLevelLast()){
			changeSceneButton.gameObject.SetActive(false);
			winLoseText.text = "Game Completed!";
		} else{
			changeScene = SceneController.Instance.LoadNextLevel;
			winLoseText.text = "You Win!";
		}
		SceneController.Instance.WinLevel();
		SoundController.Instance.PlayGameWon();
		changeSceneButton.GetComponentInChildren<Text>().text = "Next Level";
		gameOverPanel.SetActive(true);
	}

	public void LoseGame(){
		if(gameWon) return;
		gameOver = true;
		changeScene = SceneController.Instance.ReloadScene;
		winLoseText.text = "Game Over!";
		SoundController.Instance.PlayGameOver();
		changeSceneButton.GetComponentInChildren<Text>().text = "Restart";
		gameOverPanel.SetActive(true);
	}

	public void EnableGlow(){
		stopMovingGlow.SetActive(true);
	}

	public void DisableGlow(){
		stopMovingGlow.SetActive(false);
	}
}
