﻿using UnityEngine;
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
			MenuController menuController = GameObject.Find("MenuCotroller").GetComponent<MenuController>();
			if(globalLanguage == Language.AUTODETECT){
				if(Application.systemLanguage == SystemLanguage.Czech)
					menuController.setCZ();
				else
					menuController.setEN();
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
