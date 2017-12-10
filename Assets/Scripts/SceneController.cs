using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>{

	private string previousSceneName;
	private readonly string[] gameSceneNames = {"Game" , "FirstLevel", "Level2"}; // TODO add names here
	
	protected override void Awake(){
		base.Awake();
		DontDestroyOnLoad(this);
	}

	void Start(){
		SceneManager.sceneLoaded += OnSceneLoaded;
		previousSceneName = SceneManager.GetActiveScene().name;
	}
	
	private void OnSceneLoaded(Scene nextScene, LoadSceneMode mode){
		if(gameSceneNames.Contains(previousSceneName)&& nextScene.name == "MainMenu"){
			SoundController.Instance.PlayMenuMusic();
		}
		previousSceneName = nextScene.name; 
	}

	public void LoadGame(){
		SceneManager.LoadScene("Game");
		SoundController.Instance.PlayGameMusic();
	}

	public void LoadMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void LoadInstructions(){
		SceneManager.LoadScene("Instructions");
	}

	public void LoadCredits(){
		SceneManager.LoadScene("Credits");
	}

	public void ExitGame(){
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}

