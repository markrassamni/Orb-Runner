using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController> {

	[SerializeField] private AudioClip menuMusic;
	[SerializeField] private AudioClip gameMusic;
	[SerializeField] private AudioClip collisionClip;
	[SerializeField] private AudioClip gameOverClip;
	[SerializeField] private AudioClip gameWonClip;
 	private AudioSource musicSource;
	private AudioSource sfxSource;

	protected override void Awake(){
		base.Awake();
		DontDestroyOnLoad(this);
	}

	void Start () {
		musicSource = GetComponent<AudioSource>();
		musicSource.loop = true;
		musicSource.clip = menuMusic;
		musicSource.Play();
	}

	public void PlayMenuMusic(){
		musicSource.Stop();
		musicSource.clip = menuMusic;
		musicSource.Play();
	}

	public void PlayGameMusic(){
		musicSource.Stop();
		musicSource.clip = gameMusic;
		musicSource.Play();
	}

	public void PlayGameWon(){
		musicSource.Stop();
		sfxSource.PlayOneShot(gameWonClip);
	}

	public void PlayGameOver(){
		musicSource.Stop();
		sfxSource.PlayOneShot(gameOverClip);
	}

	public void PlayCollision(){
		sfxSource.PlayOneShot(collisionClip);
	}

	public void Pause(){
		sfxSource.Pause();
		musicSource.Pause();
	}

	public void UnPause(){
		musicSource.UnPause();
		sfxSource.UnPause();
	}

	public void SetVolume(float volume){
		musicSource.volume = volume;
	}

	public void SetSfxSource(AudioSource source){
		sfxSource = source;
	}
}
