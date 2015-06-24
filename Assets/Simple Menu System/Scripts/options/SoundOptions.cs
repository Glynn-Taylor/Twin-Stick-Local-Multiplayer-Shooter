using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour {
	/*	Todo
	*	- Set value of sliders on load
	*	- Everything else
	*/
	[SerializeField] Slider MasterSlider;
	[SerializeField] Slider DialogSlider;
	[SerializeField] Slider SFXSlider;
	[SerializeField] Slider MusicSlider;
	[SerializeField] Toggle SubtitlesToggle;
	[SerializeField] Text SpeakerModeDropdown;
	[SerializeField] Text LanguageDropdown;
	
	public static float DialogVolume=1;
	public static float SFXVolume=1;
	public static float MusicVolume=1;
	public static bool SubtitlesEnabled=false;
	public static string Language="English";
	private static string SpeakerMode="Stereo";
	
	private bool UnsavedChanges;
	
	public void Start(){
		loadAudioSettings();
	}
	
	public void setMasterVolume(float value){
		AudioListener.volume=value;
		UnsavedChanges=true;
	}
	public void setDialogVolume(float value){
		DialogVolume=value;
		UnsavedChanges=true;
	}
	public void setSFXVolume(float value){
		SFXVolume=value;
		UnsavedChanges=true;
	}
	public void setMusicVolume(float value){
		MusicVolume=value;
		UnsavedChanges=true;
	}
	public void toggleSubtitles(bool value){
		SubtitlesEnabled=SubtitlesToggle.isOn;
		UnsavedChanges=true;
	}
	public void setSpeakerMode(string value){
		switch(value){
			case "Mono":
				AudioSettings.speakerMode=AudioSpeakerMode.Mono;
				break;
			case "Stereo":
				AudioSettings.speakerMode=AudioSpeakerMode.Stereo;
				break;
			case "Surround":
				AudioSettings.speakerMode=AudioSpeakerMode.Surround;
				break;
		}
		SpeakerMode=value;
		SpeakerModeDropdown.text=SpeakerMode;
		UnsavedChanges=true;
	}
	public void setLanguage(string value){
		Language=value;
		LanguageDropdown.text=Language;
		UnsavedChanges=true;
	}
	public void applyChanges(){
		if(UnsavedChanges){
			PlayerPrefs.SetFloat("MasterVolume",AudioListener.volume);
			PlayerPrefs.SetFloat("DialogVolume",DialogVolume);
			PlayerPrefs.SetFloat("SFXVolume",SFXVolume);
			PlayerPrefs.SetFloat("MusicVolume",MusicVolume);
			PlayerPrefs.SetInt("Subtitles",SubtitlesEnabled?1:0);
			PlayerPrefs.SetString("SpeakerMode",SpeakerMode);
			PlayerPrefs.SetString("Language",Language);
			PlayerPrefs.Save ();
			UnsavedChanges=false;
		}
	}
	public void loadAudioSettings(){
		if(PlayerPrefs.HasKey("MasterVolume")){
			MasterSlider.onValueChanged.Invoke(PlayerPrefs.GetFloat("MasterVolume"));
			MasterSlider.value=AudioListener.volume;
		}
		if(PlayerPrefs.HasKey("DialogVolume")){
			DialogVolume=PlayerPrefs.GetFloat("DialogVolume");
			DialogSlider.onValueChanged.Invoke(DialogVolume);
			DialogSlider.value=DialogVolume;
		}
		if(PlayerPrefs.HasKey("MusicVolume")){
			MusicVolume=PlayerPrefs.GetFloat("MusicVolume");
			MusicSlider.onValueChanged.Invoke(MusicVolume);
			MusicSlider.value=MusicVolume;
		}
		if(PlayerPrefs.HasKey("SFXVolume")){
			SFXVolume=PlayerPrefs.GetFloat("SFXVolume");
			SFXSlider.onValueChanged.Invoke(SFXVolume);
			SFXSlider.value=SFXVolume;
		}
		if(PlayerPrefs.HasKey("Subtitles")){
			SubtitlesEnabled=PlayerPrefs.GetInt("Subtitles")==1?true:false;
			SubtitlesToggle.isOn=SubtitlesEnabled;
		}
		if(PlayerPrefs.HasKey("SpeakerMode")){
			SpeakerMode=PlayerPrefs.GetString("SpeakerMode");
			setSpeakerMode(SpeakerMode);
			SpeakerModeDropdown.text=SpeakerMode;
		}
		if(PlayerPrefs.HasKey("Language")){
			Language=PlayerPrefs.GetString("Language");
			LanguageDropdown.text=Language;
		}
		UnsavedChanges=false;
	}
}
