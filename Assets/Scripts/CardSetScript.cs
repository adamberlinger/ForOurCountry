using UnityEngine;
using System.Collections;

public class CardSetScript : MonoBehaviour {

	public int startSymbol;

	private float right;
	private int lastSymbol;
	private bool white = true;
	private bool killed = false,tookGuns = false;

	protected SimpleAnimation<Vector3> moveAnimation;

	protected GameObject cardMasterHolder;
	protected GameObject mainCamera;
	protected GameObject gunsCard;
	protected GameObject gunsCard2;
	protected GameObject dropHere;
	protected Transform lastCard;

	protected Vector3 movePrev;
	protected bool dragging;

	protected float minX,maxX,minY,maxY,screenWidth;
	protected Vector3 originalPosition;
		
	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
		moveAnimation = new SimpleAnimation<Vector3>(transform.position,transform.position,60,Vector3.Lerp);
	}

		// Use this for initialization
	void Start () {
		lastSymbol = startSymbol;
		dragging = false;
		//right = renderer.bounds.max.x;
		right = GetComponent<tk2dSprite>().GetUntrimmedBounds().max.x;
		cardMasterHolder = GameObject.Find ("CardMasterHolder");
		lastCard = null;

		minX  = -GetComponent<tk2dSprite>().GetUntrimmedBounds().size.x/2;
		maxX  = -minX;
		minY  = -GetComponent<tk2dSprite>().GetUntrimmedBounds().size.y/2;
		maxY  = -minY;
		originalPosition = transform.position;
		mainCamera = GameObject.Find ("Main Camera");
		screenWidth = mainCamera.GetComponent<tk2dCamera>().TargetResolution.x;
		gunsCard = GameObject.Find("Card05guns");
		gunsCard.SetActive(false);
		gunsCard2 = GameObject.Find("Card02guns");
		gunsCard2.SetActive(false);
		dropHere = GameObject.Find ("DropHere");
		if(GameObject.Find("LanguageController").GetComponent<LanguageController>().getLanguage() == LanguageController.Language.CZ){
			dropHere.GetComponent<tk2dSprite>().SetSprite(Resources.Load("game-cz/game-cz Data/game-cz", typeof(tk2dSpriteCollectionData)) as tk2dSpriteCollectionData,"frame-cz");
		}
	} 

	public string getFinalText(){
		if(GameObject.Find("LanguageController").GetComponent<LanguageController>().getLanguage() != LanguageController.Language.CZ){
			if(!killed && !tookGuns){
				return "They escaped without guns";
			}
			else if(killed){
				return "They took guns, killed officer and escaped";
			}
			else {
				return "They took guns, escaped and officer survived";
			}
		}
		else {
			if(!killed && !tookGuns){
				return "Utekli bez zbraní";
			}
			else if(killed){
				return "Zabili strážníka, vzali zbraně a utekli";
			}
			else {
				return "Vzali zbraně a utekli, strážník přežil";
			}
		}
	}

	public bool addCard(Transform card){
		CardScript cardScript = card.gameObject.GetComponent<CardScript>();
		if(lastSymbol == cardScript.startSymbol){
			//transform.Translate(new Vector3(0.0f,0.0f,0.1f));
			lastSymbol = cardScript.endSymbol;
			if(lastSymbol == 32 && !killed) {
				killed = true;
				GameObject.Find("Card02notKilled").SetActive(false);
				GameObject.Find("Card04notKilled").SetActive(false);
			}
			if(lastSymbol == 27 && !tookGuns){
				tookGuns = true;

				gunsCard.SetActive(true);
				GameObject.Find("Card01noguns").SetActive(false);
				GameObject.Find("Card01noguns2").SetActive(false);
				gunsCard2.SetActive(true);
			}
			float cardWidth = card.GetComponent<CardScript>().getWidth();
			card.parent = GetComponent<Transform>();
			card.GetComponent<CardScript>().cardAdded(new Vector3(
				right, // <- space fix
				0.0f,((lastCard)?lastCard.localPosition.z:0f) + 0.1f),60);
			moveAnimation.refresh(transform.position,
			    originalPosition = new Vector3(
				originalPosition.x - cardWidth * 0.5f,
				originalPosition.y,transform.position.z));
			right += cardWidth * 0.5f;
			maxX += cardWidth * 0.5f;
			dropHere.transform.Translate(new Vector3(cardWidth*0.5f,0f,0f));
			white = !white;
			if(lastSymbol < 0){
				cardMasterHolder.GetComponent<CardMasterHolderScript>().endSet();
				white = false;
				GameObject text = GameObject.Find("FinalText");
				StartCoroutine(FadeInText (text, 0f));
				StartCoroutine(NextScene());
				tk2dTextMesh textMesh = text.GetComponent<tk2dTextMesh>();
				textMesh.text = getFinalText();
				textMesh.Commit();
			}
			else {
				cardMasterHolder.GetComponent<CardMasterHolderScript>().nextSet();
			}
			if(white){
				StartCoroutine(MoveInWhite(GameObject.Find ("WhiteBack")));
			}
			else {
				StartCoroutine(MoveOutWhite(GameObject.Find ("WhiteBack")));
			}


			if(lastCard){
				lastCard.GetComponent<CardScript>().pushSet();
			}
			lastCard = card;
			return true;
		}
		return false;
	}

	IEnumerator NextScene(){
		Destroy(dropHere);
		yield return new WaitForSeconds(3f);
		mainCamera.camera.GetComponent<CameraController>().targetY = -15f;
		yield return new WaitForSeconds(1f);
		Application.LoadLevel("final");
		this.gameObject.transform.Translate(
			new Vector3(0f,0f,-100f));
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

	IEnumerator MoveInWhite(GameObject obj){
		obj.transform.position =
			new Vector3(29f,obj.transform.position.y,obj.transform.position.z);
		while(obj.transform.position.x > 0) {
			obj.transform.Translate(new Vector3(-0.58f,0f,0f));
			yield return new WaitForSeconds(0.01f);
		}
		obj.transform.position =
			new Vector3(0,obj.transform.position.y,obj.transform.position.z);
	}

	IEnumerator MoveOutWhite(GameObject obj){
		while(obj.transform.position.x > -29f) {
			obj.transform.Translate(new Vector3(-0.58f,0f,0f));
			yield return new WaitForSeconds(0.01f);
		}
		obj.transform.position =
			new Vector3(-29f,obj.transform.position.y,obj.transform.position.z);
	}

	// Update is called once per frame
	void Update () {

		if(moveAnimation.isActive()){
			transform.position = moveAnimation.step();
		}
		else if(Input.GetMouseButtonDown(0)){
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if((minY + transform.position.y) < mousePosition.y && mousePosition.y < (maxY + transform.position.y) &&
			   (minX + transform.position.x) < mousePosition.x && mousePosition.x < (maxX + transform.position.x)){
				movePrev = mousePosition;
				dragging = true;
			}
			else {
				//Debug.Log (minX + " " + maxX + " " + minY + " " + maxY + " " + mousePosition);
			}
		}
		else if(Input.GetMouseButton(0) && dragging){
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float direction = mousePosition.x - movePrev.x;
			transform.Translate(new Vector3(direction, 0f,0f));
			if(transform.position.x + maxX < -minX){
				transform.position = new Vector3(-maxX - minX,
				                                 transform.position.y,
				                                 transform.position.z);
			}
			else if(transform.position.x > 0f) {
				transform.position = new Vector3(0f,
				                                 transform.position.y,
				                                 transform.position.z);
			}
			else {
				movePrev = mousePosition;
			}
		}
		else if(Input.GetMouseButtonUp(0) && dragging){
			dragging = false;
		}
	}
}
