using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class TextBlock : MonoBehaviour {
	public GameObject IconBtt;
	public GameObject TextInstance;
	public GameObject TextC; Vector2 SaveInitC ; 
	Vector2 SaveInitPos ; Vector2 SaveInitPosImg ;
	Vector2 SaveInitPosPar ; Vector2 SaveInitPosIco ;
	float TextBlockAdder = 0;
	int TextBlockIndexStart = 0;
	public GameObject ContentImg;
	void Awake() {
		IconBtt.gameObject.GetComponent<Image> ().color =  new Color(0f,213f,255f,0f );
		TextC.gameObject.GetComponent<Text> ().color =  new Color(0f,213f,255f,0f );


		ObjAnim = TextC.GetComponent<Animator>() ;
		jumpToTime (currentAnimationName (ObjAnim), 0.98f,ObjAnim);
		ObjAnim = IconBtt.GetComponent<Animator>() ;
		jumpToTime (currentAnimationName (ObjAnim), 0.98f,ObjAnim);
	}


	void LaunchProjectile() // появление спустя время
	{
		TextInstance.transform.position = GameObject.Find ("PlacerTxtStart").transform.position ;
		this.gameObject.GetComponent<Button>().interactable = false;
		string[] TextInst = TextInstance.GetComponent<Text> ().text.Split('^');  // / &
		GameObject TextInstObj;  GameObject ImgInstObj;
		for (int i = 0; i < TextInst.Length; i++) {

			TextInstance.GetComponent<Text> ().fontSize = 20; // базовый размер текста задали
			SaveInitPos = TextInstance.GetComponent<RectTransform> ().anchoredPosition; 
			TextInstObj = Instantiate (TextInstance, new Vector3 (i * 2.0F, 0, 0), Quaternion.identity);

			TextInstObj.name = "BlockText" + i;
			TextInstObj.GetComponent<Text> ().text = TextInst [i];

			TextInstObj.transform.parent = this.transform;// узнаем высоту текста и в запоминаем ее для позиционирования следующего блока текста
			TextInstObj.transform.localScale = new Vector3 (1, 1, 1);
			TextInstObj.transform.localPosition = new Vector3 (TextInstObj.transform.localPosition.x,TextInstObj.transform.localPosition.y, 0); 



			// в переменную текст блок аддер добавляем высоту этой картинки
			// инстансируем картинку имя и разрешение задаем
			try { 
				string Txt = TextInst [i];
				string[] ImgInst = Txt.Split('&');
				if (ImgInst [1] != "") {
					ImgInstObj = Instantiate (ContentImg, new Vector3 (i * 2.0F, 0, 0), Quaternion.identity);  ImgInstObj.SetActive(true); ImgInstObj.transform.parent = this.transform;

					// грузим из файла картинку

					ImgInstObj.GetComponent<RawImage> ().texture = LoadPNG(Application.dataPath + ImgInst [1]);
					//print("Size is " + ImgBlockD.texture.width + " by " + ImgBlockD.texture.height + " ResizeToH: " + 640.0f/ImgBlockD.texture.width*ImgBlockD.texture.height );
					RectTransform rtImg = ImgInstObj.GetComponent<RectTransform>();	
					rtImg.sizeDelta = new Vector2(640, 640.0f/ImgInstObj.GetComponent<RawImage> ().texture.width*ImgInstObj.GetComponent<RawImage> ().texture.height);

					ImgInstObj.transform.localScale = new Vector3 (1, 1, 1);  ImgInstObj.transform.localPosition = new Vector3 (ImgInstObj.transform.localPosition.x,ImgInstObj.transform.localPosition.y, 0); 
					SaveInitPosImg = ImgInstObj.GetComponent<RectTransform> ().anchoredPosition; ImgInstObj.name = "BlockText" + i; 
					TextBlockAdder = TextBlockAdder + ImgInstObj.GetComponent<RectTransform>().rect.height + 5; // последнее число это отступ перед вставкой картинки по умолчанию 5 пх
					ImgInstObj.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (SaveInitPos.x, SaveInitPos.y - TextBlockAdder); // вернем старое местоположение



				}
			}  catch  {	}


			TextBlockAdder = TextBlockAdder + TextInstObj.GetComponent<Text> ().preferredHeight;
			TextInstObj.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (SaveInitPos.x, SaveInitPos.y - TextBlockAdder); // вернем старое местоположение

		}
		Destroy (TextInstance);  Destroy ( GameObject.Find ("BlockText0")  );
		Destroy (ContentImg); 
		// делаем показ сформированного текстового блока
		// время вспять чтобы скрыть категорию
		PlayerPrefs.SetInt ("TimeAdderB",-1); 
	}
	public void TextBlocAdd () // добавляем текстовый блок ждем 2 секунды
	{
		Invoke("LaunchProjectile", 2);//this will happen after 2 seconds
	
	}
	// Use this for initialization
	int PosAdder = -35; int TimeAdder = 1; int myInt = 1000; // myInt индекс номера обекта
	void Start () {

		PlayerPrefs.SetInt ("HigeText", 0);



		TextBlockIndexStart = 1;
		SaveInitPosPar = this.gameObject.GetComponent<RectTransform> ().anchoredPosition; 
		SaveInitPosIco = IconBtt.GetComponent<RectTransform> ().anchoredPosition; 
		SaveInitC = TextC.GetComponent<RectTransform> ().anchoredPosition; 
		this.gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (SaveInitPosPar.x,SaveInitPosPar.y);

		string strTest = this.gameObject.name;
		strTest = strTest.Replace("ParagraphInside", "");
		myInt = 0;
		try { myInt = int.Parse(strTest); } catch { }

		//Debug.Log ("strTest  " + myInt);

		// BlockText1
		// время появление в зависимости от номера кнопки
		GameObject TimeObj = this.gameObject;
		string NameNum =  TimeObj.name; // ItemMain0
		NameNum = NameNum.Replace("ParagraphInside", "");
		WaitNum = int.Parse(NameNum);

		BttPressed (2); 		AnimDirection = -1;
		ObjAnim = TextC.GetComponent<Animator>() ;
		jumpToTime (currentAnimationName (ObjAnim), 0.98f,ObjAnim);
		ObjAnim = IconBtt.GetComponent<Animator>() ;
		jumpToTime (currentAnimationName (ObjAnim), 0.98f,ObjAnim);

		PlayerPrefs.SetString ("ParagDell", this.gameObject.name); // ParagraphInside6
	}
	int WaitNum = 0;
	int WaisSec = 0;
	// Update is called once per frame
	public void TextBlocForward (int indexText)
	{
		if (indexText == 1) {
			TextBlockIndexStart = 1;
		}


	}

	public void  DelThisObjFunc(string fileName)
	{  Destroy (this.gameObject);  }

	int AdderAA = 0;
	public void HHtext ()
	{	PlayerPrefs.SetInt ("HigeText", 1);  }

	void Update () {


		if (PlayerPrefs.GetInt ("DellTXT") == 1) {
			AnimDirection = 0;
			ObjAnim = TextC.GetComponent<Animator>() ;
			jumpToTime (currentAnimationName (ObjAnim), 0.98f,ObjAnim);
			ObjAnim = IconBtt.GetComponent<Animator>() ;
			jumpToTime (currentAnimationName (ObjAnim), 0.98f,ObjAnim);


			// подготовка к удалению
			IconBtt.transform.localScale = new Vector3 (0,0,0);
			TextC.transform.localScale = new Vector3 (0,0,0);
		}

		if (PlayerPrefs.GetInt ("HigeText") == 1) // скрили все надписи
		{ BttPressed (1);  this.gameObject.GetComponent<Button>().interactable = false;  }


		AdderAA = AdderAA + 1;
		if (AdderAA == 2) {
			IconBtt.gameObject.GetComponent<Image> ().color =  new Color(0f,213f,255f,255f );
			TextC.gameObject.GetComponent<Text> ().color =  new Color(0f,213f,255f,255f );
		}


		WaisSec = WaisSec + PlayerPrefs.GetInt("TimeAdderB");
		if (WaisSec > PlayerPrefs.GetInt ("WaisSecB") + 35) {
			WaisSec = PlayerPrefs.GetInt ("WaisSecB") ;
		}
		// выжидаем время в зависимости от номера обекта
		if (WaisSec > myInt * 6 && WaisSec < myInt * 6 + 36) { 
			PlayerPrefs.SetInt ("WaisSecB", WaisSec);
			
			PosAdder = PosAdder + TextBlockIndexStart;
			if (PosAdder <= 0) {
			


				IconBtt.gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (SaveInitPosIco.x + PosAdder * 3, SaveInitPosIco.y);
				TextC.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (SaveInitC.x - PosAdder * 25, SaveInitC.y);
				IconBtt.gameObject.GetComponent<Image> ().color = new Color (0f, 213f, 255f, (PosAdder / -35.0f) * -1 + 1);
				TextC.gameObject.GetComponent<Text> ().color = new Color (0f, 213f, 255f, (PosAdder / -35.0f) * -1 + 1);
				//Debug.Log ("df  " + ( ( PosAdder/-35.0f )*-1 + 1));
			} else {
				PosAdder = 0;  
			}
		} // end

	}

	int DellObj = 0; public GameObject DellMeny;
	public void BttPressedAndDell (int State) { // скрыть и удалить меню
		

	}
	void LaunchProjectileA() // появление спустя время ParagraphInside1
	{
		ObjAnim = TextC.GetComponent<Animator>() ;
		jumpToTime (currentAnimationName (ObjAnim), 0.0f,ObjAnim);
		ObjAnim.SetFloat("Speed", 1); AnimDirection = 1;
		ObjAnim.speed = 1.0f;
		Stopper = 0.981f;
		ObjAnim = IconBtt.GetComponent<Animator>() ;
		jumpToTime (currentAnimationName (ObjAnim), 0.0f,ObjAnim);
		ObjAnim.SetFloat("Speed", 1); AnimDirection = 1;
		ObjAnim.speed = 1.0f;
		Stopper = 0.981f;
		//Debug.Log ("появление из ниоткуда");
	}
	void LaunchProjectileB() // появление спустя время ParagraphInside1
	{
		ObjAnim = TextC.GetComponent<Animator>() ;
		jumpToTime (currentAnimationName (ObjAnim), 0.98f,ObjAnim);
		ObjAnim.SetFloat("Speed", -1); AnimDirection = -1;
		ObjAnim.speed = 1.0f;
		Stopper = 0.0f;
		ObjAnim = IconBtt.GetComponent<Animator>() ;
		jumpToTime (currentAnimationName (ObjAnim), 0.98f,ObjAnim);
		ObjAnim.SetFloat("Speed", -1); AnimDirection = -1;
		ObjAnim.speed = 1.0f;
		Stopper = 0.0f;

		Debug.Log ("появление из ниоткуда");
	}

	public void BttPressed (int State) {


		if (State == 1) { // Вперед
			float TimeWait = ( WaitNum * 0.4f ) + Random.Range(0.0f, 0.1f);
			Invoke("LaunchProjectileA", TimeWait);//this will happen after 2 seconds
			ObjAnim = TextC.GetComponent<Animator>() ;
			jumpToTime (currentAnimationName (ObjAnim), 0.0f,ObjAnim);
			ObjAnim.SetFloat("Speed", 1); AnimDirection = 1;
			ObjAnim.speed = 0.0f;
			ObjAnim = IconBtt.GetComponent<Animator>() ;
			jumpToTime (currentAnimationName (ObjAnim), 0.0f,ObjAnim);
			ObjAnim.SetFloat("Speed", 1); AnimDirection = 1;
			ObjAnim.speed = 0.0f;
		}
		if (State == 2) { // Назад
			float TimeWait = ( WaitNum * 0.4f ) + Random.Range(0.0f, 0.1f);
			Invoke("LaunchProjectileB", TimeWait);//this will happen after 2 seconds
			ObjAnim = TextC.GetComponent<Animator>() ;
			jumpToTime (currentAnimationName (ObjAnim), 0.98f,ObjAnim);
			ObjAnim.SetFloat("Speed", -1); AnimDirection = -1;
			ObjAnim.speed = 0.0f;
			ObjAnim = IconBtt.GetComponent<Animator>() ;
			jumpToTime (currentAnimationName (ObjAnim), 0.98f,ObjAnim);
			ObjAnim.SetFloat("Speed", -1); AnimDirection = -1;
			ObjAnim.speed = 0.0f;
		}
	}
	public void PreRemovePrepare ()
	{
		IconBtt.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0,0);
		TextC.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0,0);
	}
	float AnimDirection = 1; float Stopper = 0.98f;
	void LateUpdate() {

		if (PlayerPrefs.GetInt ("HigeText") == 1) // скрили все надписи
		{ PlayerPrefs.SetInt ("HigeText", 0); }

		float AnimTime = ObjAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
		if (AnimDirection > 0) {
			if (AnimTime > Stopper) {  
				ObjAnim = TextC.GetComponent<Animator>() ;
				ObjAnim.speed = 0.0f; 
				ObjAnim = IconBtt.GetComponent<Animator>() ;
				ObjAnim.speed = 0.0f; 
				TextC.transform.localScale  = new Vector3 (0,0,0);  IconBtt.transform.localScale  = new Vector3 (0,0,0); 
			} 
		} else {
			if (AnimTime < Stopper) {  
				ObjAnim = TextC.GetComponent<Animator>() ;
				ObjAnim.speed = 0.0f; 
				jumpToTime (currentAnimationName (ObjAnim), 0.01f,ObjAnim);
				ObjAnim = IconBtt.GetComponent<Animator>() ;
				ObjAnim.speed = 0.0f;  
				jumpToTime (currentAnimationName (ObjAnim), 0.01f,ObjAnim);
			} 
		}

		if (DellMeny.transform.position.y > 2)
		{
			//Debug.Log("CCCDDD " + AnimTime);
			if (AnimTime > 0.95f)
			{
				// последняя грохает весь параграф
				if (this.gameObject.name == PlayerPrefs.GetString ("ParagDell") )
				{
				   DellMeny.transform.position = new Vector3(0,0,0);
				   Destroy(this.transform.parent.gameObject);
				}
				Destroy(this.transform.gameObject);
			}
		}

	}

	public void StartBttAnim ()
	{

	}


	public Animator ObjAnim; 
	string currentAnimationName(Animator AnimmO)
	{
		var currAnimName = "";
		foreach (AnimationClip clip in AnimmO.runtimeAnimatorController.animationClips) {
			if (AnimmO.GetCurrentAnimatorStateInfo (0).IsName (clip.name)) {
				currAnimName = clip.name.ToString();
			}
		}

		return currAnimName;

	}
	void jumpToTime(string name, float nTime, Animator AnimmOO)
	{
		AnimmOO.Play (name, 0, nTime);
	}
	public static Texture2D LoadPNG(string filePath) {

		Texture2D tex = null;
		byte[] fileData;

		if (File.Exists(filePath))     {
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}
}
