using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController> {
	
	override protected void Awake(){
		base.Awake();
		DontDestroyOnLoad(this);
	}

	public void LoadGame(){
		SceneManager.LoadScene("Game");
	}

	public void LoadMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void ExitGame(){
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}

