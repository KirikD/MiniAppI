using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;  
using UnityEngine.UI;

public class Paragraph : MonoBehaviour {
	public GameObject ButtonParagraph; public GameObject TextParagraph;  public GameObject TextContent;
	// Use this for initialization
	Vector2 SaveInitPos ;
	float ParagraphAdder = 0;
	void Start () { 
		LoadParagraph (this.gameObject.name);

		string PathSet = Application.dataPath;	string SetPath = PathSet.Replace("/Assets", "");
		Debug.Log ("RRR " +  SetPath );
	}
	public void LoadParagraph(string PathFullFile)
	{
		string PathSet = Application.dataPath;	string SetPath = PathSet.Replace("/Assets", "");
		Load ( Application.dataPath + "/" +  this.gameObject.name ); // Load ( Application.dataPath + "/Resources/ParagraphInside.txt" ); 

		string[] entries = TextT.Split('$');
		GameObject ParagraphInst;
		for (int i = 0; i < entries.Length; i++) {
			SaveInitPos = ButtonParagraph.GetComponent<RectTransform> ().anchoredPosition; 
			ParagraphInst = Instantiate(ButtonParagraph, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);

			ParagraphInst.transform.parent = this.transform;// узнаем высоту текста и в запоминаем ее для позиционирования следующего блока текста
			ParagraphAdder = ParagraphAdder + TextParagraph.GetComponent<Text> ().preferredHeight;
			ParagraphInst.GetComponent<RectTransform> ().anchoredPosition = new Vector2(SaveInitPos.x,SaveInitPos.y - ParagraphAdder); // вернем старое местоположение

			ParagraphInst.name = "ParagraphInside" + i;
			TextParagraph = GameObject.Find(this.gameObject.name + "/ParagraphInside" + i + "/ParText");
			TextContent =   GameObject.Find(this.gameObject.name + "/ParagraphInside" + i + "/ContentText");
			string TextBlock = entries[i];
			string[] Capsters = TextBlock.Split('#');
			TextParagraph.GetComponent<Text> ().text = Capsters[0]; 
			try	{ TextContent.GetComponent<Text> ().text = Capsters[1]; } catch { 	}

			ParagraphInst.transform.localScale = new Vector3 (1, 1, 1);
			ParagraphInst.transform.localPosition = new Vector3 (ParagraphInst.transform.localPosition.x,ParagraphInst.transform.localPosition.y, 0); 
			for (int ii = 0; ii < entries.Length; ii ++) {
				//string ParagraphsBlocs = Capsters[1];
				//string[] ParagraphsArry = ParagraphsBlocs.Split('/');

			}
		}
		Destroy (ButtonParagraph);  Destroy ( GameObject.Find ("ParagraphInside0")  );
		PlayerPrefs.SetString("ParagraphOldName", this.gameObject.name) ; // сохранили имя параграфа
		Debug.Log("ParagraphOldName  " + this.gameObject.name);
	}
	// Update is called once per frame  ItemAnim.GetComponent<RectTransform> ().anchoredPosition; 
	void Update () {
		
	}

	string TextT;
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
					//Debug.Log(line);
					if (line != null)
					{
						TextT = TextT + line;
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
