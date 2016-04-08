using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour, IGameManager
{
	[SerializeField] private AudioSource soundSource; // ссылка на новый источник звука

	[SerializeField] private AudioSource music1Source;
	//[SerializeField] private string introBGMusic;
	[SerializeField] private string levelBGMusic;
	//[SerializeField] private AudioSource music2Source;

	private AudioSource _activeMusic;
	private AudioSource _inactiveMusic;
	public float crossFadeRate = 1.5f;
	private bool _crossFading;

	public ManagerStatus status {get; private set;}

	#region Управление громкостью фоновой музыки
	private float _musicVolume;
	public float musicVolume{
		get{return _musicVolume;} 
		set{
			_musicVolume = value;

			if(music1Source != null && !_crossFading){
				music1Source.volume = _musicVolume;
				//music2Source.volume = _musicVolume; // Регулировка громкости обоих источников музыки
			}
		}}

	public bool musicMute{
		get{
			if(music1Source != null)
				return music1Source.mute;
			return false;
		}
		set{
			if(music1Source != null){
				music1Source.mute = value;
				//music2Source.mute = value;
			}
		}
	}
	#endregion

	// свойство с функцией чтения и функцией доступа для громкости
	public float soundVolume{
		get{return AudioListener.volume;}
		set{AudioListener.volume = value;}
	}
	
	public bool soundMute{
		get{return AudioListener.pause;}
		set{AudioListener.pause = value;}
	}

	public void Startup(){
		Debug.Log("Audio manager starting...");
		// Игнорируем громкость компонента AudioListener
		music1Source.ignoreListenerVolume = true;
		music1Source.ignoreListenerPause = true;
		//music2Source.ignoreListenerVolume = true;
		//music2Source.ignoreListenerPause = true;

		soundVolume = 1f;
		musicVolume = 1f;

		_activeMusic = music1Source;
		//_inactiveMusic = music2Source;
		status = ManagerStatus.Started;

		//Managers.Audio.PlayIntroMusic();
		Managers.Audio.PlayLevelMusic();
	}

	// воспроизводим звуки, не имеющие другого источника
	public void PlaySound(AudioClip clip){
		soundSource.PlayOneShot(clip);
	}


	#region Фоновая музыка
	// воспроизведение с помощью параметра AudioSource.clip
	public void PlayMusic(AudioClip clip){
		if(_crossFading){
			return;
		}
		StartCoroutine(CrossFadeMusic(clip));
	}

	public void PlayIntroMusic(){
		//PlayMusic(Resources.Load("Music/" + introBGMusic) as AudioClip);
	}

	public void PlayLevelMusic(){
		PlayMusic(Resources.Load("Music/" + levelBGMusic) as AudioClip);
	}

	public void StopMusic(){
		_activeMusic.Stop();
		_inactiveMusic.Stop();
	}

	private IEnumerator CrossFadeMusic(AudioClip clip){
		_crossFading = true;

		_inactiveMusic.clip = clip;
		_inactiveMusic.volume = 0;
		_inactiveMusic.Play();

		float scaledRate = crossFadeRate * _musicVolume;

		while(_activeMusic.volume > 0){
			_activeMusic.volume -= scaledRate * Time.deltaTime;
			_inactiveMusic.volume += scaledRate * Time.deltaTime;

			yield return null;
		}

		AudioSource temp = _activeMusic;

		_activeMusic = _inactiveMusic;
		_activeMusic.volume = _musicVolume;

		_inactiveMusic = temp;
		_inactiveMusic.Stop();

		_crossFading = false;
	}

	#endregion



}

