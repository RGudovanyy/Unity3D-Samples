// Скрипт окна настроек звука; подключается к Settings Poopup и UIController
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsPopup : MonoBehaviour {
	[SerializeField] private AudioClip sound;


	public void Open(){
		this.gameObject.SetActive(true);
	}

	public void Close() {
		this.gameObject.SetActive(false);
	}

	// Настройки громкости звуков
	public void OnSoundToggle(){
		Managers.Audio.soundMute = !Managers.Audio.soundMute;
		//Managers.Audio.PlaySound(sound);
	}

	public void OnSoundValue(float volume){
		Managers.Audio.soundVolume = volume;
	}

	// Настройки громкости музыки
	public void OnMusicToogle(){
		Managers.Audio.musicMute = !Managers.Audio.musicMute;
		//Managers.Audio.PlaySound(sound);
	}

	public void OnMusicValue(float volume){
		Managers.Audio.musicVolume = volume;
	}
}
