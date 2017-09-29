using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;  
using UnityEngine.UI;
public class AppDataLoad : MonoBehaviour {
	public Text TextA; public Text TextB; public Text TextC; public Text TextD;
	// Use this for initialization
	void Start () {
		Load( Application.dataPath +  "/Resources/Capsters/" + "ApplicationText.txt");
	}
	
	// Update is called once per frame
	void Update () {
		
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
					//string[] ItemsMainNameA = line.Split('@');
					//TextA.text = ItemsMainNameA[1];
					try
					{
					string[] ItemsMainNameB = line.Split('$');
					TextB.text = ItemsMainNameB[1];
					}	catch { 	}

					try
					{
					string[] ItemsMainNameC = line.Split('#');
					TextC.text = ItemsMainNameC[1];
				}
				catch {	}

					try
					{
					string[] ItemsMainNameD = line.Split('%');
					TextD.text = ItemsMainNameD[1];
			}
			catch { 	}
					Debug.Log(line);
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
