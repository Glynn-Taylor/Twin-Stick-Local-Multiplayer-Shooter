using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GraphicsOptions : MonoBehaviour {
	[SerializeField] Slider BrightnessSlider;
	[SerializeField] Toggle FullscreenToggle;
	[SerializeField] Text ResolutionDropdown;
	[SerializeField] Text AspectRatioDropdown;
	/*	Notes
		- Brightness only goes from set brightness to 0, idk if it works too
		- Need to clamp brightness if changing the range
		- Save first launch resolution and aspect ratio as default
		- Populate list of resolutions with Screen.resolutions filtered by aspect ratio
		- Set aspect ratio and resolution on apply
	*/
	private float Brightness=0.5f;
	private int ARx=4;
	private int ARy=3;
	private int ScreenWidth=1280;
	private int ScreenHeight=720;
	
	private bool UnsavedChanges;
	
	private Color LevelAmbientLight;
	// Use this for initialization
	void Start () {
		Resolution[] resolutions = Screen.resolutions;
		foreach (Resolution res in resolutions) {
			Debug.Log(res.width + "x" + res.height);
		}
		LevelAmbientLight=RenderSettings.ambientLight;
		if(!PlayerPrefs.HasKey("GraphicsOptions"))
			createDefaults();
		loadSettings();
	}
	
	public void setBrightness(float value){
		RenderSettings.ambientLight = new Color(LevelAmbientLight.r*value, LevelAmbientLight.g*value, LevelAmbientLight.b*value, LevelAmbientLight.a);
		Brightness=value;
		UnsavedChanges=true;
	}
	public void applyChanges(){
		if(UnsavedChanges){
			PlayerPrefs.SetFloat("Brightness",Brightness);
			PlayerPrefs.Save ();
			UnsavedChanges=false;
			loadSettings();
		}
	}
	public void loadSettings(){
		if(PlayerPrefs.HasKey("Brightness")){
			Brightness=PlayerPrefs.GetFloat("Brightness");
			BrightnessSlider.onValueChanged.Invoke(Brightness);
			BrightnessSlider.value=Brightness;
		}
		if(PlayerPrefs.HasKey("ARx")){	
			ARx = PlayerPrefs.GetInt("ARx");
			ARy = PlayerPrefs.GetInt("ARx");
			AspectRatioDropdown.text=ARx.ToString()+":"+ARy.ToString();
		}
		if(PlayerPrefs.HasKey("ScreenWidth")&&PlayerPrefs.HasKey("Fullscreen")){
			Screen.SetResolution(PlayerPrefs.GetInt("ScreenWidth"),PlayerPrefs.GetInt("ScreenHeight"),PlayerPrefs.GetInt("Fullscreen")==1?true:false);
			ScreenWidth=Screen.width;
			ScreenHeight=Screen.height;
			ResolutionDropdown.text=ScreenWidth.ToString()+"x"+ScreenHeight.ToString();
		}
		UnsavedChanges=false;
	}
	private void createDefaults(){
		PlayerPrefs.SetFloat("Brightness",0.5f);
		Vector2 AR = GetAspectRatio(Screen.width,Screen.height);
		PlayerPrefs.SetInt("ARx",(int)AR.x);
		PlayerPrefs.SetInt("ARy",(int)AR.y);
		PlayerPrefs.SetInt("ScreenWidth",Screen.width);
		PlayerPrefs.SetInt("ScreenHeight",Screen.height);
		PlayerPrefs.SetInt("Fullscreen",Screen.fullScreen?1:0);
		PlayerPrefs.SetInt("GraphicsOptions",1);
		PlayerPrefs.Save ();
	}
	private Vector2 GetAspectRatio(int x, int y){
		float f = (float)x / (float)y;
		int i = 0;
		while(true){
			i++;
			if(System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
				break;
		}
		return new Vector2((float)System.Math.Round(f * i, 2), i);
	}
}
