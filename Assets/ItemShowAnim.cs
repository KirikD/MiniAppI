using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
public class ItemShowAnim : MonoBehaviour {
	public Vector2 PosContainer;
	public Animator ObjAnim; 
	public GameObject HigeAll;GameObject CanvasC;
	public Text TxtP;
	// Use this for initialization
	Vector3 SaveInitPos = Vector3.zero;
	public string EndPath = "";
	void Start () {
		// время появление в зависимости от номера кнопки
		GameObject TimeObj = this.gameObject.transform.parent.gameObject;
		string NameNum =  TimeObj.name; // ItemMain0
		NameNum = NameNum.Replace("ItemMain0", "");
		WaitNum = int.Parse(NameNum);

		CanvasC = GameObject.Find("Canvas");
		BttPressed (1); 
		this.gameObject.GetComponent<Button>().interactable = false;

		string IndexItem = this.gameObject.transform.parent.gameObject.name;
		IndexItem = IndexItem.Replace("ItemMain", "");
		//PlayerPrefs.SetString("ItemsMainName" + IndexItem, ItemsMainName[1] ) ; // сохранили Заголовок
		Load( Application.dataPath +  "/Resources/Capsters/" + IndexItem +"/ParagraphInside.txt");
		// загрузка картинки из файла в спрайт текстуры
		Texture2D Texx = LoadPNG (Application.dataPath + "/" +  EndPath);  //"/Resources/GUI/Img/Catt.jpg");
		Sprite Spr = Sprite.Create(Texx, new Rect(0, 0, Texx.width, Texx.height), new Vector2(0.5f, 0.5f));
		this.gameObject.GetComponent<Image> ().sprite = Spr;

		PlayerPrefs.SetInt ("RestoreAllButtons", 0);
			//this.gameObject.GetComponent<RawImage> ().texture = LoadPNG(Application.dataPath + "/Resources/GUI/Img/Catt.jpg");
		// запомним стартовую координату
		SaveInitPos = HigeAll.GetComponent<RectTransform> ().anchoredPosition;
		PlayerPrefs.SetFloat ("ContX",SaveInitPos.x); PlayerPrefs.SetFloat ("ContY",SaveInitPos.y); // вернем координату на место

		PlayerPrefs.SetInt("TextHige", 0) ;
	}
	int WaitNum = 0;
	float Stopper = 0.98f;
	// Update is called once per frame
	float AnimDirection = 1;
	int AdderPar = 0;
	int WaitAnimTime;
	void Update() {
		// отображение и дубликация параграфа по новой
		if (PlayerPrefs.GetInt("RestoreParagraph") == 1)
		{
			AdderPar = AdderPar + 1;
			if (AdderPar == 2) {
				ParagraphShow (PlayerPrefs.GetString ("ParagraphOldName"));
				PlayerPrefs.SetInt ("RestoreParagraph", 0);
				AdderPar = 0;
			}
		}

		if (PlayerPrefs.GetInt ("RestoreAllButtons") == 1) // активируем при входе в главное меню
		{
			this.gameObject.GetComponent<Button> ().interactable = true;

			///Debug.Log("SetActiv");
			AdderRes = AdderRes + 1;
			if (AdderRes == 10) {
				PlayerPrefs.SetInt ("RestoreAllButtons", 0);
				AdderRes = 0;
			}

		}
	}
	int AdderRes = 0;
	int Addinteractable = 10;
	void LateUpdate() {



		float AnimTime = ObjAnim.GetCurrentAnimatorStateInfo (0).normalizedTime;
		if (AnimDirection > 0) {
			if (AnimTime > Stopper) {  
				ObjAnim.speed = 0.0f; 
			} 
		} else {
			if (AnimTime < Stopper) {  
				ObjAnim.speed = 0.0f; 
			} 
		}
		Addinteractable = Addinteractable + 1;
		if (Addinteractable == 131) {
			this.gameObject.GetComponent<Button> ().interactable = true;
		}
		if (Addinteractable < 130) {
			this.gameObject.GetComponent<Button> ().interactable = false;
		}
	}
	public void BttHidge () {
		BttPressed (3);
	}
	public GameObject ParagraphInst;
	public void ParagraphShow (string PathFile) { // дублируем и отображаем параграф
		Vector3 Pos = ParagraphInst.transform.position;
		GameObject Paragraph = Instantiate(ParagraphInst, Pos, Quaternion.identity);

		Paragraph.name = PathFile;
		Paragraph.transform.parent = CanvasC.transform;// 
		Paragraph.SetActive(true);
		Paragraph.transform.SetSiblingIndex(10);
		Paragraph.transform.localScale = new Vector3 (1, 1, 1); 
		Paragraph.transform.localPosition = new Vector3 (Paragraph.transform.localPosition.x, Paragraph.transform.localPosition.y, 0); 
	}

	void LaunchProjectile() // появление спустя время
	{
		PlayerPrefs.SetFloat ("ContX",SaveInitPos.x); PlayerPrefs.SetFloat ("ContY",SaveInitPos.y); // вернем координату на место PosContainer
		jumpToTime (currentAnimationName (), 0.5f);
		ObjAnim.SetFloat("Speed", 1); AnimDirection = 1;
		ObjAnim.speed = 1.0f;
		Stopper = 0.985f;
		//Debug.Log ("появление из ниоткуда");
		Addinteractable = 0;
		//this.gameObject.GetComponent<Button>().interactable = true;
		PlayerPrefs.SetInt ("WaitAnimItem", 1); // выжидание перед анимацией
	}
	public void BttPressed (int State) {


		//PlayerPrefs.SetFloat ("ContX",PosContainer.x); PlayerPrefs.SetFloat ("ContY",PosContainer.y); 	PlayerPrefs.SetInt ("WaitAnimItem", 1); // задаем сдвиг тут
		PlayerPrefs.SetInt ("WaitAnimItem", 1);
		if (State == 1) { // появление из ниоткуда 
			
			float TimeWait = ( WaitNum * 0.2f ) + Random.Range(0.0f, 0.5f);
			Invoke("LaunchProjectile", TimeWait);//this will happen after 2 seconds
			jumpToTime (currentAnimationName (), 0.5f);
			ObjAnim.speed = 0.0f;

		}

		if (State == 2) { // скрытие в никуда
			//PlayerPrefs.SetFloat ("ContX",SaveInitPos.x); PlayerPrefs.SetFloat ("ContY",SaveInitPos.y); // вернем координату на место PosContainer
			jumpToTime (currentAnimationName (), 0.98f);
			ObjAnim.SetFloat("Speed", -1); AnimDirection = -1;
			ObjAnim.speed = 1.0f;
			Stopper = 0.5f;
			Debug.Log ("скрытие в никуда");
			this.gameObject.GetComponent<Button>().interactable = false;

		}
		if (State == 3) { // увеличим масштаб в плсю
			
			// даем координату куда двигатся анимации сдвига контента.
			PlayerPrefs.SetFloat ("ContX",PosContainer.x); PlayerPrefs.SetFloat ("ContY",PosContainer.y); 	PlayerPrefs.SetInt ("WaitAnimItem", 1); // задаем сдвиг тут
			//PlayerPrefs.SetInt ("WaitAnimItem", 1);
			// скрываем все другие кнопки
			HidgeAllButtons other = (HidgeAllButtons)HigeAll.GetComponent (typeof(HidgeAllButtons));
			other.BttHidgeSet();

			jumpToTime (currentAnimationName (), 0.0f);
			ObjAnim.SetFloat("Speed", 1); AnimDirection = 1;
			ObjAnim.speed = 1.0f;
			Stopper = 0.335f;
			Debug.Log (" увеличим масштаб в плсю");
			this.gameObject.GetComponent<Button>().interactable = false;
		}

		if (State == 4) { // уменьшим масштаб 
			if (Stopper == 0.335f) {
				Addinteractable = 0;
				// Возвращаем все назад уменьшаем кнопочку
				//PlayerPrefs.SetFloat ("ContX",SaveInitPos.x); PlayerPrefs.SetFloat ("ContY",SaveInitPos.y); // вернем координату на место
				jumpToTime (currentAnimationName (), 0.335f);
				ObjAnim.SetFloat ("Speed", -1);
				AnimDirection = -1;
				ObjAnim.speed = 0.5f;
				Stopper = 0.0f;
				//Debug.Log (" увеличим масштаб в плсю");
			} else {
				BttPressed (1);
			}
		}


	}
	string currentAnimationName()
	{
		var currAnimName = "";
		foreach (AnimationClip clip in ObjAnim.runtimeAnimatorController.animationClips) {
			if (ObjAnim.GetCurrentAnimatorStateInfo (0).IsName (clip.name)) {
				currAnimName = clip.name.ToString();
			}
		}

		return currAnimName;

	}
	void jumpToTime(string name, float nTime)
	{
		ObjAnim.Play (name, 0, nTime);
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

	public void  Load(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();


					string[] ItemsMainName = line.Split('@');
					//string IndexItem = this.gameObject.transform.parent.gameObject.name;
					//IndexItem = IndexItem.Replace("ItemMain", "");
					//PlayerPrefs.SetString("ItemsMainName" + IndexItem, ItemsMainName[1] ) ; // сохранили Заголовок
					TxtP.text = ItemsMainName[1];

					//Debug.Log(line);
					if (line != null)
					{
						//TextT = TextT + line;
					}
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();

			}
		}
		catch { Debug.Log("df");	}
	}
}
