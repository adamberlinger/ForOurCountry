using UnityEngine;
using System.Collections;

public class FinalController : MonoBehaviour {

	public GameObject answer01yes;
	public GameObject answer01no;
	public GameObject question01;

	public GameObject answer02yes;
	public GameObject answer02no;
	public GameObject question02;

	public GameObject answer03yes;
	public GameObject answer03no;
	public GameObject question03;

	public GameObject answer11yes;
	public GameObject answer11no;
	public GameObject question11;
	
	public GameObject answer12yes;
	public GameObject answer12no;
	public GameObject question12;
	
	public GameObject answer13yes;
	public GameObject answer13no;
	public GameObject question13;

	public GameObject answer21yes;
	public GameObject answer21no;
	public GameObject question21;
	
	public GameObject answer22yes;
	public GameObject answer22no;
	public GameObject question22;
	
	public GameObject answer23yes;
	public GameObject answer23no;
	public GameObject question23;

	public GameObject button0;
	public GameObject button1;
	public GameObject button2;

	public GameObject headline1;
	public GameObject headline2;
	public GameObject headline3;

	public GameObject circle01;
	public GameObject circle02;
	public GameObject circle03;
	public GameObject circle11;
	public GameObject circle12;
	public GameObject circle13;

	public GameObject circleb;
	public GameObject circlew;

	private int[][] results;
	private int answered = 0;

	public GameObject cam;

	private CameraController c;
	public int state = 0;
	private buttonRadio radio;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		state = 0;
		c = cam.GetComponent<CameraController> ();

		results = new int[4][];
		results[0] = new int [4];
		results[1] = new int [4];
		results[2] = new int [4];

		HideText (button0.transform.GetChild (0).GetChild (0).gameObject);
		HideText (button1.transform.GetChild (0).GetChild (0).gameObject);

		HideText (question21);
		HideText (question22);
		HideText (question23);
		HideText (answer21yes);
		HideText (answer21no);
		HideText (answer22yes);
		HideText (answer22no);
		HideText (answer23yes);
		HideText (answer23no);

		HideText (headline3);
		HideText (button2.transform.GetChild (0).GetChild (0).gameObject);

		answered = 0;

		HideSpriteTk2D (circle01);
		HideSpriteTk2D (circle02);
		HideSpriteTk2D (circle03);
		HideSpriteTk2D (circle11);
		HideSpriteTk2D (circle12);
		HideSpriteTk2D (circle13);
	}

	void ClickedState() {
		if (state == 0) {
			c.targetX = 30;
			state = 1;
			answered = 0;
		} else if (state == 1) {
			c.targetX = 0;
			state = 2;
			StartCoroutine (FadeInText (question21,1f));
			StartCoroutine (FadeInText (question22,1f));
			StartCoroutine (FadeInText (question23,1f));
			StartCoroutine (FadeInText (answer21yes,1f));
			StartCoroutine (FadeInText (answer21no,1f));
			StartCoroutine (FadeInText (answer22yes,1f));
			StartCoroutine (FadeInText (answer22no,1f));
			StartCoroutine (FadeInText (answer23yes,1f));
			StartCoroutine (FadeInText (answer23no,1f));
			StartCoroutine (FadeInText (headline3,2f));
			StartCoroutine (FadeInText (button2.transform.GetChild (0).GetChild (0).gameObject,3f));

			StartCoroutine (FadeInSpriteTk2D (circle01,1.5f));
			StartCoroutine (FadeInSpriteTk2D (circle02,2f));
			StartCoroutine (FadeInSpriteTk2D (circle03,2.5f));
			StartCoroutine (FadeInSpriteTk2D (circle11,3f));
			StartCoroutine (FadeInSpriteTk2D (circle12,3.5f));
			StartCoroutine (FadeInSpriteTk2D (circle13,4f));

			if(results [0][1] == 2) {circle01.transform.position = answer21yes.transform.position;} else {circle01.transform.position = answer21no.transform.position;}
			if(results [0][2] == 2) {circle02.transform.position = answer22yes.transform.position;} else {circle02.transform.position = answer22no.transform.position;}
			if(results [0][3] == 2) {circle03.transform.position = answer23yes.transform.position;} else {circle03.transform.position = answer23no.transform.position;}
			if(results [1][1] == 2) {circle11.transform.position = answer21yes.transform.position;} else {circle11.transform.position = answer21no.transform.position;}
			if(results [1][2] == 2) {circle12.transform.position = answer22yes.transform.position;} else {circle12.transform.position = answer22no.transform.position;}
			if(results [1][3] == 2) {circle13.transform.position = answer23yes.transform.position;} else {circle13.transform.position = answer23no.transform.position;}

			circle01.transform.Translate(new Vector3(0f, 0.5f, 0f));
			circle02.transform.Translate(new Vector3(0f, 0.5f, 0f));
			circle03.transform.Translate(new Vector3(0f, 0.5f, 0f));
			circle11.transform.Translate(new Vector3(0f, 0.5f, 0f));
			circle12.transform.Translate(new Vector3(0f, 0.5f, 0f));
			circle13.transform.Translate(new Vector3(0f, 0.5f, 0f));

		} else if (state == 2) {
			c.targetY = -30;
			StartCoroutine(SwitchScene("end", 0.5f));
		}
	}

	void ClickedRadio(tk2dUIItem clickedUIItem) {
		radio = clickedUIItem.gameObject.GetComponent<buttonRadio> ();
		if(state == 0) {
			switch (radio.position) {
				case 1:
					ShowSpriteTk2D(circlew);
					if(radio.answer == 2) {circlew.transform.position = answer01yes.transform.position + new Vector3(0f,0.5f,0f);} else {circlew.transform.position = answer01no.transform.position + new Vector3(2.2f,0.5f,0f);}
					StartCoroutine (FadeOutSpriteTk2D (circlew));
					StartCoroutine (FadeOutText (answer01yes.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (answer01no.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (question01));
					break;
				case 2:
					ShowSpriteTk2D(circlew);
					if(radio.answer == 2) {circlew.transform.position = answer02yes.transform.position + new Vector3(0f,0.5f,0f);} else {circlew.transform.position = answer02no.transform.position + new Vector3(2.2f,0.5f,0f);}
					StartCoroutine (FadeOutSpriteTk2D (circlew));
					StartCoroutine (FadeOutText (answer02yes.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (answer02no.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (question02));
					break;
				case 3:
					ShowSpriteTk2D(circlew);
					if(radio.answer == 2) {circlew.transform.position = answer03yes.transform.position + new Vector3(0f,0.5f,0f);} else {circlew.transform.position = answer03no.transform.position + new Vector3(2.2f,0.5f,0f);}
					StartCoroutine (FadeOutSpriteTk2D (circlew));
					StartCoroutine (FadeOutText (answer03yes.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (answer03no.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (question03));
					break;		
			}
		} else {
			switch (radio.position) {
				case 1:
					ShowSpriteTk2D(circleb);
					if(radio.answer == 2) {circleb.transform.position = answer11yes.transform.position + new Vector3(0f,0.5f,0f);} else {circleb.transform.position = answer11no.transform.position + new Vector3(2.2f,0.5f,0f);}
					StartCoroutine (FadeOutSpriteTk2D (circleb));
					StartCoroutine (FadeOutText (answer11yes.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (answer11no.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (question11));
					break;
				case 2:
					ShowSpriteTk2D(circleb);
					if(radio.answer == 2) {circleb.transform.position = answer12yes.transform.position + new Vector3(0f,0.5f,0f);} else {circleb.transform.position = answer12no.transform.position + new Vector3(2.2f,0.5f,0f);}
					StartCoroutine (FadeOutSpriteTk2D (circleb));
					StartCoroutine (FadeOutText (answer12yes.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (answer12no.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (question12));
					break;
				case 3:
					ShowSpriteTk2D(circleb);
					if(radio.answer == 2) {circleb.transform.position = answer13yes.transform.position + new Vector3(0f,0.5f,0f);} else {circleb.transform.position = answer13no.transform.position + new Vector3(2.2f,0.5f,0f);}
					StartCoroutine (FadeOutSpriteTk2D (circleb));
					StartCoroutine (FadeOutText (answer13yes.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (answer13no.transform.GetChild (0).GetChild (0).gameObject));
					StartCoroutine (FadeOutText (question13));
					break;		
			}
		}
		if (results [state] [radio.position] == 0) {
			results [state] [radio.position] = radio.answer;	
			answered++;
			if(answered == 3 && state == 0) {
				StartCoroutine (FadeOutText (headline1));
				StartCoroutine (FadeInText (button0.transform.GetChild (0).GetChild (0).gameObject, 1f));
			}
			if(answered == 3 && state == 1) {
				StartCoroutine (FadeOutText (headline2));
				StartCoroutine (FadeInText (button1.transform.GetChild (0).GetChild (0).gameObject, 1f));
			}
		}

	}
	
	void HideText(GameObject obj) {
		Color c = obj.GetComponent<tk2dTextMesh>().color;
		c.a = 0;
		obj.GetComponent<tk2dTextMesh>().color = c;
	}

	void HideSpriteTk2D(GameObject obj) {
		Color c = obj.GetComponent<tk2dSprite>().color;
		c.a = 0;
		obj.GetComponent<tk2dSprite>().color = c;
	}

	void ShowSpriteTk2D(GameObject obj) {
		Color c = obj.GetComponent<tk2dSprite>().color;
		c.a = 1;
		obj.GetComponent<tk2dSprite>().color = c;
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

	IEnumerator FadeInSpriteTk2D(GameObject obj, float delay = 0) {
		yield return new WaitForSeconds(delay);
		while(obj.GetComponent<tk2dSprite>().color.a < 1) {
			Color c = obj.GetComponent<tk2dSprite>().color;
			c.a += Time.deltaTime;
			obj.GetComponent<tk2dSprite>().color = c;
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
}
