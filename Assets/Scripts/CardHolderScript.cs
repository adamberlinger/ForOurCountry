using UnityEngine;
using System.Collections;

public class CardHolderScript : MonoBehaviour {

	public int splitX;
	public int splitY;
	public int offsetY;
	protected bool cardsEnabled;

	private Transform cardList;
	private Transform selectedCard;
	private GameObject cardSet;
	private GameObject dropArea;
	private CardSetScript cardSetScript;

	// Use this for initialization
	void Start () {
		cardsEnabled = true;
		cardList = GetComponent<Transform>();
		foreach(Transform card in cardList){
			card.gameObject.GetComponent<CardScript>().homePosition = card.localPosition;
			card.gameObject.GetComponent<CardScript>().unselectCard();
		}

		cardSet = GameObject.Find("CardSet");
		dropArea = GameObject.Find ("DropArea");
		cardSetScript = cardSet.GetComponent<CardSetScript>();
	}

	public void enableCards(){cardsEnabled = true;}

	protected void selectCard(Vector2 position){
		Transform newSelectedCard = null;
		foreach(Transform card in cardList){
			//if(card.renderer.bounds.Contains(position)){
			if(card.collider2D.OverlapPoint(position)) {
				if(!newSelectedCard || newSelectedCard.transform.position.z > card.transform.position.z){
					newSelectedCard = card;
				}
			}
		}
		if(newSelectedCard != selectedCard){
			if(selectedCard){
				selectedCard.gameObject.GetComponent<CardScript>().unselectCard();
			}
			selectedCard = newSelectedCard;
			if(newSelectedCard){
				newSelectedCard.gameObject.GetComponent<CardScript>().selectCard();
			}
		}
	}

	protected void moveCard(Vector3 position){
		if(selectedCard){
			Vector3 size = selectedCard.GetComponent<BoxCollider2D>().size;
			selectedCard.gameObject.GetComponent<CardScript>().moveTo(
				new Vector3(position.x - transform.position.x + size.x * 0.4f,
			            position.y - transform.position.y + size.y * 0.4f,-1f),10);
		}
	}

	protected void releaseCard(){
		if(selectedCard && dropArea.collider2D.OverlapPoint(
			new Vector2(selectedCard.position.x,selectedCard.position.y))){
			if(cardSetScript.addCard(selectedCard)){
				selectedCard = null;
				cardsEnabled = false;
			}
		}
		if(selectedCard){
			selectedCard.GetComponent<CardScript>().unselectCard();
			selectedCard = null;
		}
	}

	// Update is called once per frame
	void Update () {
		if(cardsEnabled){
			if(Input.GetMouseButtonDown(0)){
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePosition.z = 0.0f;
				this.selectCard(new Vector2(mousePosition.x,mousePosition.y));
			}
			else if(Input.GetMouseButton(0)){
				Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				moveCard(v);
			}
			else if(Input.GetMouseButtonUp(0)){
				releaseCard();
			}
		}
	}
}
