using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {

	public int startSymbol;
	public int endSymbol;
	public Vector3 homePosition;

	protected bool moving;

	protected SimpleAnimation<Vector3> moveAnimation;
	
	protected float scale;
	protected float width;
	protected SimpleAnimation<float> scaleAnimation;

	void Awake(){
		scale = 1f;
		scaleAnimation = new SimpleAnimation<float>(1f,1f,20,(x, y, z) => x + (y-x)*z);
		moveAnimation = new SimpleAnimation<Vector3>(transform.localPosition,
		          transform.localPosition,20,Vector3.Lerp);
	}

	// Use this for initialization
	void Start () {
		width = GetComponent<BoxCollider2D>().size.x;
		localise();
 	}

	protected void localise(){
		if(GameObject.Find ("LanguageController").GetComponent<LanguageController>()
		   .getLanguage() == LanguageController.Language.CZ){

			localiseTile(transform.FindChild("CardStart").GetComponent<tk2dSprite>(),startSymbol);
			localiseTile(transform.FindChild("CardEnd").GetComponent<tk2dSprite>(),endSymbol);
		}
	}

	protected void localiseTile(tk2dSprite targetSprite,int symbol){
		if((symbol >= 3 && symbol <= 7) || symbol == 9
		   || (symbol >= 11 && symbol <= 12) || symbol == 16 || symbol == 19 || symbol == 25){

			string name = symbol.ToString("D3")+"-cz";
			targetSprite.SetSprite(Resources.Load("game-cz/game-cz Data/game-cz", typeof(tk2dSpriteCollectionData)) as tk2dSpriteCollectionData,name);
		}
	}

	public float getWidth(){
		return width;
	}

	public void selectCard(){
		scaleAnimation.refresh(this.scale,1.2f,20);
		Vector3 v = moveAnimation.getEndPosition();
		moveAnimation.refresh(new Vector3(v.x,v.y,-1f));
	}

	public void unselectCard(){
		scaleAnimation.refresh(this.scale,0.5f,30);
		moveAnimation.refresh(transform.localPosition,homePosition,30);
	}

	public void moveTo(Vector3 position,int frames = 10){
		moveAnimation.refresh(position,frames);
	}

	public void cardAdded(Vector3 localPosition,int frames){
		scaleAnimation.refresh(1f,frames);
		transform.eulerAngles = new Vector3(0f,0f,0f);
		moveAnimation.refresh(transform.localPosition,localPosition,frames);
		foreach(Transform part in transform){
			part.GetComponent<tk2dSprite>().color = new Color(1f,1f,1f,0.5f);
		}
		if(audio){
			audio.Play();
		}
	}

	public void pushSet(){
		foreach(Transform part in transform){
			part.GetComponent<tk2dSprite>().color = Color.white;
		}
	}

	// Update is called once per frame
	void Update () {
		if(scaleAnimation.isActive()){
			this.scale = scaleAnimation.step();
			transform.localScale = new Vector3(scale,scale,scale);
		}

		if(moveAnimation.isActive()){
			this.transform.localPosition = moveAnimation.step ();

		}
	}
}
