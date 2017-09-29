 using UnityEngine;
 using LitJson;
 using System;
 using System.Collections;
using System.IO;
 public class parseJSON
 {
	public string MainAppName;
	public string MainAppImg;

	public string ConnectTxt;
	public string ConnectImg;
	public string CloseImg;
	public string SpeedAnim;
     public ArrayList but_title;
     public ArrayList but_image;

	public ArrayList  but_TimeData;

	public ArrayList  but_TextA, but_TextB , but_TextC , but_TextD;
	public ArrayList  but_ImageA , but_ImageB , but_ImageC  , but_ImageD ;
 }
 public class JSON_D : MonoBehaviour
 {
	public string contentAllRead;

	void readTextFile(string file_path)
	{
		StreamReader inp_stm = new StreamReader(file_path);

		while(!inp_stm.EndOfStream)
		{
			string inp_ln = inp_stm.ReadLine( );
			//Debug.Log("Pathh: " + inp_ln); // Do Something with the input. 
			contentAllRead = contentAllRead + inp_ln;
		}
		inp_stm.Close( );  
	}
	void Awake() {
		readTextFile(Application.dataPath + "/Resources/JsonData.json"); // считваем весь файл
		Processjson(contentAllRead); // а это из файла на диске по указанному пути
	}
     // Sample JSON for the following script has attached.
     IEnumerator Start()
     {
		
		//Debug.Log("Pathh: " + Application.dataPath + "/Resources/JsonData.json");
	//	Debug.Log("Pathh2: " + Application.persistentDataPath + "file.txt");
		string PathFollder = "Pathh: " + Application.dataPath + "/Resources/JsonData.json";

		string url = "Http// путь к файлу";
         WWW www = new WWW(url);
         yield return www;
         if (www.error == null)
         {
            Processjson(www.data); // это из интернета

         }
         else
         {
             Debug.Log("ERROR: " + www.error);
         }        
     }    
     private void Processjson(string jsonString)
     {
         JsonData jsonvale = JsonMapper.ToObject(jsonString);
         parseJSON parsejson;
         parsejson = new parseJSON();
		 parsejson.MainAppName = jsonvale["MainAppName"].ToString();
		 parsejson.MainAppImg  = jsonvale["MainAppImg"].ToString();

		 parsejson.ConnectTxt  = jsonvale["ConnectTxt"].ToString();
		 parsejson.ConnectImg  = jsonvale["ConnectImg"].ToString();
		 parsejson.CloseImg  = jsonvale["CloseImg"].ToString();
		parsejson.SpeedAnim = jsonvale["SpeedAnim"].ToString();
         
         parsejson.but_title = new ArrayList ();
         parsejson.but_image = new ArrayList ();
		parsejson.but_TimeData = new ArrayList ();
		parsejson.but_TextA = new ArrayList (); parsejson.but_TextB = new ArrayList (); parsejson.but_TextC = new ArrayList (); parsejson.but_TextD = new ArrayList ();
		parsejson.but_ImageA = new ArrayList (); parsejson.but_ImageB = new ArrayList (); parsejson.but_ImageC = new ArrayList (); parsejson.but_ImageD = new ArrayList ();

		for(int i = 0; i<jsonvale["Titles"].Count; i++)
         {
			parsejson.but_title.Add(jsonvale["Titles"][i]["Title"].ToString());
			//Debug.Log(jsonvale["Titles"][i]["Title"].ToString()); 
			parsejson.but_image.Add(jsonvale["Titles"][i]["Image"].ToString());
			parsejson.but_TimeData.Add(jsonvale["Titles"][i]["TimeData"].ToString());
 
			for(int ii = 0; ii<  jsonvale["Titles"][i]["ContentInsidePost"].Count; ii++)
			{
				//Debug.Log(jsonvale["Titles"][i]["ContentInsidePost"][ii]["TextA"].ToString()); 
				parsejson.but_TextA.Add(jsonvale["Titles"][i]["ContentInsidePost"][ii]["TextA"].ToString()); 
				parsejson.but_ImageA.Add(jsonvale["Titles"][i]["ContentInsidePost"][ii]["ImageA"].ToString()); 

				parsejson.but_TextB.Add(jsonvale["Titles"][i]["ContentInsidePost"][ii]["TextB"].ToString()); 
				parsejson.but_ImageB.Add(jsonvale["Titles"][i]["ContentInsidePost"][ii]["ImageB"].ToString()); 

				parsejson.but_TextC.Add(jsonvale["Titles"][i]["ContentInsidePost"][ii]["TextC"].ToString()); 
				parsejson.but_ImageC.Add(jsonvale["Titles"][i]["ContentInsidePost"][ii]["ImageC"].ToString()); 

				parsejson.but_TextD.Add(jsonvale["Titles"][i]["ContentInsidePost"][ii]["TextD"].ToString()); 
				parsejson.but_ImageD.Add(jsonvale["Titles"][i]["ContentInsidePost"][ii]["ImageD"].ToString()); 
			}
				
         }    
     }
 }