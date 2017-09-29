using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CodeAnim : MonoBehaviour {
	public int ScrIndex;
	public Image ItemAnim; public Image ItemAnimImg;
	public Image LineImg;

	public Text TextShow;

	public Image ContentBlock;
	// Use this for initialization
	Vector2 OldPosition;
	Vector2 OldSize;
	int InitPos = 60;



	float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	private Vector3 InitPosV; 
	Vector3 targetPosition = Vector3.zero;

	float smoothTimeB = 0.3F;
	private Vector3 velocitB = Vector3.zero;
	Vector3 targetPositionB = Vector3.zero;

	void Start () { 
		InitContainerPos = ContentBlock.GetComponent<RectTransform> ().anchoredPosition;
		Pivot =  new Vector2 (0.2f,0.84f);
		InitPosV = ItemAnim.GetComponent<RectTransform> ().sizeDelta;

		PlayerPrefs.SetInt ("BttPressDirection",1);
		PlayerPrefs.SetInt ("ParamAnimDrag",1);
		PlayerPrefs.SetInt ("PressedItem",0);
		AdderStrt = InitPos;
		OldPosition = ItemAnim.GetComponent<RectTransform> ().anchoredPosition; 
		OldPosition = new Vector2 (OldPosition.x,OldPosition.y ); 
		OldSize = new Vector2 (ItemAnim.GetComponent<RectTransform> ().rect.width,ItemAnim.GetComponent<RectTransform> ().rect.height);
	}
	int AdderAnim = -70; int AdderStrt = 0; int AdderLine = -30;
	int BttPressedAdder = 0;
	// Update is called once per frame
	int BttCollapse = 0;

	int ParamAnimDrag = 1;
	Vector2 InitContainerPos;
	void Update () {
		//ParamAnimDrag = PlayerPrefs.GetInt("ParamAnimDrag");

		AdderStrt = AdderStrt - ParamAnimDrag;

		AdderLine = AdderLine + ParamAnimDrag*5; 

		AdderAnim = AdderAnim + ParamAnimDrag;

		if (AdderLine < 220) {
			LineImg.GetComponent<RectTransform> ().sizeDelta = new Vector2 (AdderLine, LineImg.GetComponent<RectTransform> ().rect.height);
			if (AdderLine < -30) {	AdderLine = -30;	}
		} else {AdderLine = 220;	}

		if (AdderStrt > 0) {
			ItemAnimImg.color =  new Color(255f,255f,255f,( AdderStrt / 60.0f )*-1 + 1 );
			ItemAnim.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (ItemAnim.GetComponent<RectTransform> ().anchoredPosition.x, OldPosition.y - AdderStrt);
			if (AdderStrt > 60) {	AdderStrt  = 60;	}
		} else { AdderStrt = 0;	}



			if (AdderAnim < 65) {
				TextShow.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (TextShow.GetComponent<RectTransform> ().anchoredPosition.x, TextShow.GetComponent<RectTransform> ().rect.height - AdderAnim ); 
			if (AdderAnim < -70) {	AdderAnim  = -70;	}
		} else { AdderAnim = 65;	}
		if (BttPress == 1) {
			BttCollapse = 1;


			if (Index == ScrIndex) {

				BttPressedAdder = BttPressedAdder + PlayerPrefs.GetInt ("BttPressDirection");
				if (BttPressedAdder < 100) {
					//ContentBlock.GetComponent<RectTransform> ().pivot = Pivot;
					//Debug.Log (BttPressedAdder );
					//ContentBlock.GetComponent<RectTransform> ().sizeDelta = new Vector2 (ContentBlock.GetComponent<RectTransform> ().rect.width + BttPressedAdder / 100.0f, ContentBlock.GetComponent<RectTransform> ().rect.height + BttPressedAdder / 100.0f);
					//ItemAnim.GetComponent<RectTransform> ().sizeDelta = new Vector2 (ItemAnim.GetComponent<RectTransform> ().rect.width + BttPressedAdder / 100.0f, ItemAnim.GetComponent<RectTransform> ().rect.height + BttPressedAdder / 100.0f);
					if (BttPressedAdder < 0) {	BttPressedAdder = 0;	}
				} else {		BttPressedAdder  = 100; }
			} else {

			}
		} else {
			if (PlayerPrefs.GetInt("PressedItem") == 1 ) {
				ParamAnimDrag = -1;
			}
		}

		targetPosition = Vector3.SmoothDamp(targetPosition, InitPosV, ref velocity, smoothTime);
		ItemAnim.GetComponent<RectTransform> ().sizeDelta = new Vector2 (targetPosition.x,targetPosition.y);

		targetPositionB = Vector3.SmoothDamp(targetPositionB,  new Vector3(Pivot.x,Pivot.y,0), ref velocitB, smoothTimeB);
		Debug.Log ("PressedItem" + Pivot );
		ContentBlock.GetComponent<RectTransform> ().pivot = new Vector2(targetPositionB.x,targetPositionB.y);
		ContentBlock.GetComponent<RectTransform> ().anchoredPosition = new Vector2(-324,74);
	}
	// кнопку нажали
	private int BttPress = 0; private int Index = 0;
	Vector2 Pivot = new Vector2 (0,0);
	public void BttPressed (int IndexBtt) {
		InitPosV = new Vector3 (400,380,0);
		BttPress = 1; Index = IndexBtt;
		if (IndexBtt == 1) {
			Pivot =  new Vector2 (0.2f,0.84f); 
			PlayerPrefs.SetInt ("PressedItem",1);
		}
		if (IndexBtt == 2) {
			Pivot =  new Vector2 (0.5f,0.84f);
			PlayerPrefs.SetInt ("PressedItem",1);
		}
		if (IndexBtt == 3) {
			Pivot =  new Vector2 (0.82f,0.84f);
			PlayerPrefs.SetInt ("PressedItem",1);
		}

		if (IndexBtt == 4) {
			Pivot =  new Vector2 (0.2f,0.54f);
			PlayerPrefs.SetInt ("PressedItem",1);
		}
		if (IndexBtt == 5) {
			Pivot =  new Vector2 (0.5f,0.54f);
			PlayerPrefs.SetInt ("PressedItem",1);
		}
		if (IndexBtt == 6) {
			Pivot =  new Vector2 (0.82f,0.54f);
			PlayerPrefs.SetInt ("PressedItem",1);
		}

		if (IndexBtt == 7) {
			Pivot =  new Vector2 (0.2f,0.21f);
			PlayerPrefs.SetInt ("PressedItem",1);
		}
		if (IndexBtt == 8) {
			Pivot =  new Vector2 (0.5f,0.21f);
			PlayerPrefs.SetInt ("PressedItem",1);
		}
		if (IndexBtt == 9) {
			Pivot =  new Vector2 (0.82f,0.21f);
			PlayerPrefs.SetInt ("PressedItem",1);
		}

		// запускаем вперед анимацию появления параграфов
		PlayerPrefs.SetInt ("TimeAdderB",1);
	}

	public void BttUndoPress () { 
		
		InitPosV = new Vector3 (314,269,0);
		PlayerPrefs.SetInt ("TimeAdderB",-1);
		 PlayerPrefs.SetInt ("BttPressDirection",-1);
		// задаем направление анимации главных итемов
		PlayerPrefs.SetInt ("ParamAnimDrag",1);
		BttPress = 1;
		ParamAnimDrag = 1;

		// запускаем текст анимацию вспять
		PlayerPrefs.SetInt("TextHige", -1) ;


	}
}
