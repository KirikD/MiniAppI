using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContentAnim : MonoBehaviour {
	public GameObject TextShow;
	public GameObject ImgShow;
	Vector2 TextInitPos;
	// Use this for initialization
	int myInt = 1000;
	public GameObject OriginalParagraph ; private GameObject Canv ;
	void Start () {
		PlayerPrefs.SetInt("DellTXT", 0) ;
		PlayerPrefs.SetInt("RestoreParagraph", 0) ;
		PlayerPrefs.SetInt("TextHige", 1) ;
		try { TextShow.gameObject.GetComponent<Text> ().color =  new Color(0f,0f,0f,0f );  } catch { }
		try { ImgShow.gameObject.GetComponent<RawImage> ().color =  new Color(0f,0f,0f,0f );  } catch { }
		PlayerPrefs.SetInt("ActivateDragger", 0) ;

		InitPos = new Vector3 (TextShow.GetComponent<RectTransform> ().anchoredPosition.x + 200,TextShow.GetComponent<RectTransform> ().anchoredPosition.y,0);
		targetPosition = new Vector3 (TextShow.GetComponent<RectTransform> ().anchoredPosition.x,TextShow.GetComponent<RectTransform> ().anchoredPosition.y,0);
		TextInitPos = TextShow.GetComponent<RectTransform> ().anchoredPosition; 
	
		string strTest = this.gameObject.name;
		strTest = strTest.Replace("BlockText", "");

		try { myInt = int.Parse(strTest) + 9; } catch { }

		//Debug.Log ("strTest  " + myInt);
		// сохранили имя параграфа в переменну
		OriginalParagraph = GameObject.FindGameObjectWithTag("Finish");

		//OriginalParagraph  = parent.FindObject("Paragraph"); 
		//OriginalParagraph = rootObject.transform.Find( "Paragraph" ).gameObject;
		Canv = GameObject.Find("Canvas");
		PlayerPrefs.SetString("EndDragUI", this.gameObject.name) ;
	}

	float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	private Vector3 InitPos; 
	Vector3 targetPosition = Vector3.zero;

	int Adder  = 0; int AdderDellTxt;
	public GameObject ParagrafObj;
	int PermAdd = 0;
	void Update() {
		try { TextShow.gameObject.GetComponent<Text> ().color =  new Color(0f,0f,0f,0f );  } catch { }
		try { ImgShow.gameObject.GetComponent<RawImage> ().color =  new Color(0f,0f,0f,0f );  } catch { }

		Adder = Adder + PlayerPrefs.GetInt("TextHige") ;
		if (Adder == myInt * 6 + 30) { 
			InitPos = new Vector3 (InitPos.x - 200,InitPos.y,255);
			try { TextShow.gameObject.GetComponent<Text> ().color =  new Color(0f,0f,0f,255f ); } catch { }
			//try { ImgShow.gameObject.GetComponent<RawImage> ().color =  new Color(0f,0f,0f,0f );  } catch { }

		}


		if (Adder > myInt * 6 + 30) {
			Adder = myInt * 6 + 30;

			try { PermAdd = PermAdd + 1;
			if (PermAdd == 2) {
					PlayerPrefs.SetInt("ActivateDragger", 1) ;
				string[] ImgInst = this.gameObject.GetComponent<Text> ().text.Split('&'); 
				string Inst = ImgInst[1];
				this.gameObject.GetComponent<Text> ().text = " "; // заменили строчку с путем к картинке на пустой пробел
				} } catch { }
		}


		if (Adder < 0) {
			InitPos = new Vector3 (InitPos.x + 200,InitPos.y,0);
			AdderDellTxt = AdderDellTxt + 1;
			if (AdderDellTxt == 100) {
				Destroy (ParagrafObj);
				// клонируем параграф со старым именем
				PlayerPrefs.SetInt("RestoreParagraph", 1) ; // отображение и дубликация параграфа по новой
				PlayerPrefs.SetInt("DellTXT", 1) ;
				//string OldName = PlayerPrefs.GetString("ParagraphOldName") ; // Взяли старое имя параграфа
				//GameObject Pa = Instantiate(OriginalParagraph, OriginalParagraph.transform.position, Quaternion.identity);
			//	Pa.transform.parent = Canv.transform;  Pa.name = OldName;


			}
			//TextShow.gameObject.GetComponent<Text> ().color =  new Color(0f,0f,0f,0f );
			Adder = 0;
		}
		//InitPos = new Vector3 (InitPos.x ,InitPos.y,255);
		targetPosition = Vector3.SmoothDamp(targetPosition, InitPos, ref velocity, smoothTime);
		 TextShow.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (targetPosition.x,targetPosition.y); 
		try { ImgShow.gameObject.GetComponent<RawImage> ().color =  new Color(255f,255f,255f,targetPosition.z );  } catch { }
		try { TextShow.gameObject.GetComponent<Text> ().color =  new Color(0f,0f,0f,targetPosition.z);  } catch { }

	}


}
