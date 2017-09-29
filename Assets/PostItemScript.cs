using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //To access 'Toggle'
using System.IO;
public class PostItemScript : MonoBehaviour {
	public Toggle toggle; public Image ImgOn;  public Image ImgOff;
	private int TogAnim = 2; private int TogStateAnim = 0;

	public Image VerticalLine;

	public Text TextBlockA; //

	public RawImage ImgBlockA;

	public Text TextBlockB; //public Image NullB;
	public RawImage ImgBlockB;

	public Text TextBlockC; //public Image NullC;
	public RawImage ImgBlockC;

	public Text TextBlockD; //public Image NullD;
	public RawImage ImgBlockD;
	// Use this for initialization TogStateAnim

	void Awake() {
		
		TextBlockA.text = "Какой-то Вольфганг Ишингер .. Который не имеет никакой должности ни в ЕС, ни (тем более) в НАТО.., а возглавляет Мюнхенскую конференцию по безопасности. Как и Юнкер, - очень немолодой (я не пишу маразматик, заметьте), 71 год от роду. Заявляет, что не уверен хочет ли Украина в НАТО и станет ли НАТО сильнее с Украиной. Поэтому он скептик и сомневается в членстве.\n\n( Collapse )\n\nНа что Яценюк (прошедший все горячие точки 90-х) тут же ставит его на место - Да, подавляющее большинство украинцев поддерживает вступление Украины в НАТО. Да, НАТО, безусловно, станет сильнее с Украиной, ведь только Украина может защитить Европу от орды. И, безусловно, Украина скоро будет в НАТО. Браво, Арсений! ";
		TextBlockB.text = "71 год от роду. Заявляет, что не уверен хочет ли Украина в НАТО и станет ли НАТО сильнее с Украиной. Поэтому он скептик и сомневается в членстве.\n\n( Collapse )\n\nНа что Яценюк (прошедший все горячие точки 90-х) тут же ставит его на место - Да, подавляющее большинство украинцев поддерживает вступление Украины в НАТО. Да, НАТО, безусловно, станет сильнее с Украиной, ведь только Украина может защитить Европу от орды. И, безусловно, Украина скоро будет в НАТО. Браво, Арсений! ";
		TextBlockC.text = "Какой-то Вольфганг Ишингер .. Который не имеет никакой должности ни в ЕС, Какой-то Вольфганг Ишингер .. Который не имеет никакой должности ни в ЕС,Какой-то Вольфганг Ишингер .. Который не имеет никакой должности ни в ЕС,ни (тем более) в НАТО.., а возглавляет Мюнхенскую конференцию по безопасности. Как и Юнкер, - очень немолодой (я не пишу маразматик, заметьте), 71 год от роду. Заявляет, что не уверен хочет ли Украина в НАТО и станет ли НАТО сильнее с Украиной. Поэтому он скептик и сомневается в членстве.\n\n( Collapse )\n\nНа что Яценюк (прошедший все горячие точки 90-х) тут же ставит его на место - Да, подавляющее большинство украинцев поддерживает вступление Украины в НАТО. Да, НАТО, безусловно, станет сильнее с Украиной, ведь только Украина может защитить Европу от орды. И, безусловно, Украина скоро будет в НАТО. Браво, Арсений! ";
		TextBlockD.text = "Да, подавляющее большинство украинцев поддерживает вступление Украины в НАТО. Да, НАТО, безусловно, станет сильнее с Украиной, ведь только Украина может защитить Европу от орды. И, безусловно, Украина скоро будет в НАТО. Браво, Арсений! ";

	}
	void Start () {


		RectTransform rtTXTA = TextBlockA.GetComponent<RectTransform>();
		ImgBlockA.texture = LoadPNG(Application.dataPath + "/Resources/GUI/Img/Catt.jpg");
		print("Size is " + ImgBlockA.texture.width + " by " + ImgBlockA.texture.height + " ResizeToH: " + 640.0f/ImgBlockA.texture.width*ImgBlockA.texture.height );
		RectTransform rtImgA = ImgBlockA.GetComponent<RectTransform>();	rtImgA.sizeDelta = new Vector2(640, 640.0f/ImgBlockA.texture.width*ImgBlockA.texture.height);
		ImgBlockA.GetComponent<RectTransform>().anchoredPosition = new Vector2(ImgBlockA.GetComponent<RectTransform>().anchoredPosition.x, TextBlockA.GetComponent<RectTransform>().anchoredPosition.y - TextBlockA.GetComponent<Text>().preferredHeight );

		RectTransform rtTXTB = TextBlockB.GetComponent<RectTransform>();
		TextBlockB.GetComponent<RectTransform>().anchoredPosition = new Vector2(ImgBlockB.GetComponent<RectTransform>().anchoredPosition.x, ImgBlockA.GetComponent<RectTransform>().anchoredPosition.y - ImgBlockA.GetComponent<RectTransform>().rect.height );
		ImgBlockB.texture = LoadPNG(Application.dataPath + "/Resources/GUI/Img/CatDr.jpg");
		print("Size is " + ImgBlockB.texture.width + " by " + ImgBlockB.texture.height + " ResizeToH: " + 640.0f/ImgBlockB.texture.width*ImgBlockB.texture.height );
		RectTransform rtImg = ImgBlockB.GetComponent<RectTransform>();	rtImg.sizeDelta = new Vector2(640, 640.0f/ImgBlockB.texture.width*ImgBlockB.texture.height);
		ImgBlockB.GetComponent<RectTransform>().anchoredPosition = new Vector2(ImgBlockB.GetComponent<RectTransform>().anchoredPosition.x, TextBlockB.GetComponent<RectTransform>().anchoredPosition.y - TextBlockB.GetComponent<Text>().preferredHeight );

		RectTransform rtTXTC = TextBlockC.GetComponent<RectTransform>();
		TextBlockC.GetComponent<RectTransform>().anchoredPosition = new Vector2(ImgBlockC.GetComponent<RectTransform>().anchoredPosition.x, ImgBlockB.GetComponent<RectTransform>().anchoredPosition.y - ImgBlockB.GetComponent<RectTransform>().rect.height );
		ImgBlockC.texture = LoadPNG(Application.dataPath + "/Resources/GUI/Img/maxresdefault.jpg");
		print("Size is " + ImgBlockC.texture.width + " by " + ImgBlockC.texture.height + " ResizeToH: " + 640.0f/ImgBlockC.texture.width*ImgBlockC.texture.height );
		rtImg = ImgBlockC.GetComponent<RectTransform>();	rtImg.sizeDelta = new Vector2(640, 640.0f/ImgBlockC.texture.width*ImgBlockC.texture.height);
		ImgBlockC.GetComponent<RectTransform>().anchoredPosition = new Vector2(ImgBlockC.GetComponent<RectTransform>().anchoredPosition.x, TextBlockC.GetComponent<RectTransform>().anchoredPosition.y - TextBlockC.GetComponent<Text>().preferredHeight );

		RectTransform rtTXTD = TextBlockD.GetComponent<RectTransform>();
		TextBlockD.GetComponent<RectTransform>().anchoredPosition = new Vector2(ImgBlockD.GetComponent<RectTransform>().anchoredPosition.x, ImgBlockC.GetComponent<RectTransform>().anchoredPosition.y - ImgBlockC.GetComponent<RectTransform>().rect.height );
		ImgBlockD.texture = LoadPNG(Application.dataPath + "/Resources/GUI/Img/SetCat.jpg");
		print("Size is " + ImgBlockD.texture.width + " by " + ImgBlockD.texture.height + " ResizeToH: " + 640.0f/ImgBlockD.texture.width*ImgBlockD.texture.height );
		rtImg = ImgBlockD.GetComponent<RectTransform>();	rtImg.sizeDelta = new Vector2(640, 640.0f/ImgBlockD.texture.width*ImgBlockD.texture.height);
		ImgBlockD.GetComponent<RectTransform>().anchoredPosition = new Vector2(ImgBlockD.GetComponent<RectTransform>().anchoredPosition.x, TextBlockD.GetComponent<RectTransform>().anchoredPosition.y - TextBlockD.GetComponent<Text>().preferredHeight );
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
	int AnimDown = 300; 
	int SpeedAnim = 5; // Важный параметр скорость анимации всех эффектов
	// Update is called once per frame
	void Update () {
		if (toggle.isOn)
		{  
			TogAnim = SpeedAnim;	
			// когда развернуто делаем большим
			RectTransform rt = this.GetComponent<RectTransform>();
			float BttPos = ( ImgBlockD.GetComponent<RectTransform>().anchoredPosition.y - ImgBlockD.GetComponent<RectTransform>().rect.height - 150)*-1;
			//rt.sizeDelta = new Vector2(rt.sizeDelta.x, BttPos);
			rt.sizeDelta = new Vector2(rt.sizeDelta.x, AnimDown);
			VerticalLine.GetComponent<RectTransform>().sizeDelta = new Vector2(VerticalLine.GetComponent<RectTransform>().sizeDelta.x, 1 + BttPos);
		}
		else
		{  
			TogAnim = SpeedAnim*-1; 
			// делаем размер маленьким когда все свернуто
			RectTransform rt = this.GetComponent<RectTransform>();
			//rt.sizeDelta = new Vector2(rt.sizeDelta.x, 300);
			rt.sizeDelta = new Vector2(rt.sizeDelta.x, AnimDown);
			VerticalLine.GetComponent<RectTransform>().sizeDelta = new Vector2(VerticalLine.GetComponent<RectTransform>().sizeDelta.x, 166);
		}

		TogStateAnim = TogStateAnim + TogAnim;
		if (TogStateAnim > 100) {
			TogStateAnim = 100;
		}
		if (TogStateAnim < -100) {
			TogStateAnim = -100;
		}

		AnimDown = AnimDown + TogAnim*3;
		if (AnimDown > (ImgBlockD.GetComponent<RectTransform> ().anchoredPosition.y - ImgBlockD.GetComponent<RectTransform> ().rect.height - 150) * -1) {
			AnimDown = (int)((ImgBlockD.GetComponent<RectTransform> ().anchoredPosition.y - ImgBlockD.GetComponent<RectTransform> ().rect.height - 150) * -1) ;
		}
		if (AnimDown < 350) {
		
			AnimDown = 350;
		}
		Color c1 = ImgOn.color;
		c1.a = (0.0f + TogStateAnim)/100; ImgOn.color = c1;
		Color c2 = ImgOff.color; 
		c2.a = (0.0f + TogStateAnim*-1)/100;  ImgOff.color = c2;




	}
}
