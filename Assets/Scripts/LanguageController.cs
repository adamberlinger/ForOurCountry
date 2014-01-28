using UnityEngine;
using System.Collections;

public class LanguageController : MonoBehaviour {

	public enum Language {
		AUTODETECT = 0,
		CZ = 1,
		EN = 2
	}

	protected Language language;
	protected static Language globalLanguage = Language.AUTODETECT;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		if(language == Language.AUTODETECT){
			if(globalLanguage == Language.AUTODETECT){
				globalLanguage = language = (Application.systemLanguage == SystemLanguage.Czech)?
					Language.CZ:Language.EN;
			}
			else {
				language = globalLanguage;
			}
		}
		Debug.Log ("Selected language: " + language);
	}

	public void setLanguage(Language lang){
		language = lang;
		Debug.Log ("Selected language: " + language);
	}

	public Language getLanguage(){return language;}

	// Update is called once per frame
	void Update () {
	
	}
}
