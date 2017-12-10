﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : Singleton<SceneController>{

	private string previousSceneName;
	private readonly string[] gameSceneNames = {"Level1", "Level2", "Level3", "Level4", "Level5" , "FirstLevel", "Game"};
	
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

	public void LoadMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void LoadCredits(){
		SceneManager.LoadScene("Credits");
	}

	public void ReloadScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		SoundController.Instance.PlayGameMusic();
	}

	public void LoadLevelSelect(){
		SceneManager.LoadScene("LevelSelect");
	}

	public void LoadLevel(Text levelNumber){
		var level = "Level" + levelNumber.text;
		SceneManager.LoadScene(level);
		SoundController.Instance.PlayGameMusic();
	}

	public void LoadNextLevel(){
		if (!IsCurrentLevelLast()){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			SoundController.Instance.PlayGameMusic();
		} else{
			Debug.LogWarning("Could not load next scene, current index is the last");
			LoadMenu();
			SoundController.Instance.PlayMenuMusic();
		}
	}

	public bool IsCurrentLevelLast(){
		return SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 1;
	}

	public void ExitGame(){
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}

