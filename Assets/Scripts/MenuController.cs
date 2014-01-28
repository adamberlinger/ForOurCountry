using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject textButton;
	public GameObject textButtonWhite;
	public GameObject textButtonBlack;

	public GameObject textInfo;
	public GameObject textInfoWhite;
	public GameObject textInfoBlack;

	public GameObject textBigWhite;
	public GameObject textBigBlack;

	public GameObject button;
	public GameObject buttonWhite;
	public GameObject buttonBlack;

	public GameObject cam;

	private Animator animator;
	private CameraController c;
	public int state = 0;

	public GameObject introText1;
	public GameObject introText2;
	public GameObject introText3;
	public GameObject introText4;

	public GameObject turnWhite;
	public GameObject turnBlack;
	public GameObject dvojice;

	public GameObject logo;

	public GameObject czSwitch;
	public GameObject enSwitch;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		state = 0;
		c = cam.GetComponent<CameraController> ();

		StartCoroutine(FadeInText (introText1, 0.5f));
		StartCoroutine(FadeInText (introText2, 1f));
		StartCoroutine(FadeInText (introText3, 1.5f));
		StartCoroutine(FadeInText (introText4, 2f));
		StartCoroutine(FadeInText (czSwitch, 2f));
		StartCoroutine(FadeInText (enSwitch, 2f));
		StartCoroutine(FadeInSpriteTk2D (logo));
	}


	void GameStart () {
		c.targetY = 0;
	}

	void clicked() {
		switch(state) {

		case 0:
				StartCoroutine(DisableClick (button, 4f));
				c.targetX = 30;
				state = 1;				
				StartCoroutine(FadeOutText (textButton, 2f));
				StartCoroutine(FadeOutText (textInfo, 2f));
				StartCoroutine(FadeOutSpriteTk2D(dvojice, 2f));
			break;
		case 1:
				StartCoroutine(FadeOutText (textButtonWhite));
				StartCoroutine(FadeOutText (textInfoWhite));
				StartCoroutine(FadeOutSpriteTk2D(turnWhite));
				StartCoroutine(ChangeText (textButtonWhite, "I understand", 1.5f));
				StartCoroutine(DisableClick (buttonWhite, 3f));
				StartCoroutine(FadeInText (textButtonWhite, 1.5f));
				StartCoroutine(FadeInSprite (textBigWhite, 1.5f));
				state = 2;
				break;
			case 2:
				StartCoroutine(DisableClick (buttonWhite, 4f));
				c.targetX = -30;
				state = 3;
				break;
			case 3:
				StartCoroutine(FadeOutText (textButtonBlack));
				StartCoroutine(FadeOutText (textInfoBlack));
				StartCoroutine(FadeOutSpriteTk2D(turnBlack));
				StartCoroutine(ChangeText (textButtonBlack, "I understand", 1.5f));
				StartCoroutine(DisableClick (buttonBlack, 3f));
				StartCoroutine(FadeInText (textButtonBlack, 1.5f));
				StartCoroutine(FadeInSprite (textBigBlack, 1.5f));
				state = 4;
				break;
			case 4:
				c.targetX = 0;
				ShowText (textButton);
				ShowText (textInfo);
				StartCoroutine(ChangeText (textInfo, "Now both of you can look.\n\nDon't talk about your quests and characters!"));
				StartCoroutine(ChangeText (textButton, "START"));	
				state = 5;
				break;
			case 5:
				c.targetX = 30;
				HideText (textButtonWhite);
				HideSprite (textBigWhite);
				state = 6;
				StartCoroutine(SwitchScene ("game", 1f));
				break;
		}
	}

	public void setCZ(){
		GameObject.Find ("LanguageController").GetComponent<LanguageController>().setLanguage(LanguageController.Language.CZ);
		//TODO: change menu texts
	}
	public void setEN(){
		GameObject.Find ("LanguageController").GetComponent<LanguageController>().setLanguage(LanguageController.Language.EN);
		//TODO: change menu texts
	}


	void HideSprite(GameObject obj) {
		Color c = obj.GetComponent<SpriteRenderer>().color;
		c.a = 0;
		obj.GetComponent<SpriteRenderer>().color = c;
	}

	void HideText(GameObject obj) {
		Color c = obj.GetComponent<tk2dTextMesh>().color;
		c.a = 0;
		obj.GetComponent<tk2dTextMesh>().color = c;
	}

	void ShowText(GameObject obj) {
		Color c = obj.GetComponent<tk2dTextMesh>().color;
		c.a = 1;
		obj.GetComponent<tk2dTextMesh>().color = c;
	}

	IEnumerator SwitchScene(string scene, float delay) {
		yield return new WaitForSeconds(delay);
		Application.LoadLevel (scene);
	}

	IEnumerator DisableClick(GameObject obj, float delay) {
		obj.GetComponent<tk2dUIItem> ().enabled = false;
		yield return new WaitForSeconds(delay);
		obj.GetComponent<tk2dUIItem> ().enabled = true;
	}

	IEnumerator ChangeText(GameObject obj, string text, float delay = 0) {
		yield return new WaitForSeconds(delay);
		obj.GetComponent<tk2dTextMesh>().text = text;
	}

	IEnumerator FadeOutText(GameObject obj, float delay = 0) {
		yield return new WaitForSeconds(delay);
		while(obj.GetComponent<tk2dTextMesh>().color.a > 0) {
			Color c = obj.GetComponent<tk2dTextMesh>().color;
			c.a -= Time.deltaTime;
			obj.GetComponent<tk2dTextMesh>().color = c;
			yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator FadeInText(GameObject obj, float delay = 0) {
		yield return new WaitForSeconds(delay);
		while(obj.GetComponent<tk2dTextMesh>().color.a < 1) {
			Color c = obj.GetComponent<tk2dTextMesh>().color;
			c.a += Time.deltaTime;
			obj.GetComponent<tk2dTextMesh>().color = c;
			yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator FadeInSprite(GameObject obj, float delay = 0) {
		yield return new WaitForSeconds(delay);
		while(obj.GetComponent<SpriteRenderer>().color.a < 1) {
			Color c = obj.GetComponent<SpriteRenderer>().color;
			c.a += Time.deltaTime;
			obj.GetComponent<SpriteRenderer>().color = c;
			yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator FadeOutSpriteTk2D(GameObject obj, float delay = 0) {
		yield return new WaitForSeconds(delay);
		while(obj.GetComponent<tk2dSprite>().color.a > 0) {
			Color c = obj.GetComponent<tk2dSprite>().color;
			c.a -= Time.deltaTime;
			obj.GetComponent<tk2dSprite>().color = c;
			yield return new WaitForSeconds(0.01f);
		}
	}
	IEnumerator FadeInSpriteTk2D(GameObject obj, float delay = 0) {
		yield return new WaitForSeconds(delay);
		while(obj.GetComponent<tk2dSprite>().color.a < 1) {
			Color c = obj.GetComponent<tk2dSprite>().color;
			c.a += Time.deltaTime;
			obj.GetComponent<tk2dSprite>().color = c;
			yield return new WaitForSeconds(0.01f);
		}
	}
}
