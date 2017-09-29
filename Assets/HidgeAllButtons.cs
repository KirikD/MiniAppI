using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidgeAllButtons : MonoBehaviour {
	public GameObject[] Base; /****/
	// Use this for initialization
	int adder = 10;
	void Start () {
		// стартовая позиция контейнера задаем
		SetVec = new Vector3 (PlayerPrefs.GetFloat ("ContX"), PlayerPrefs.GetFloat ("ContY")); SetPosC = new Vector3 (PlayerPrefs.GetFloat ("ContX"), PlayerPrefs.GetFloat ("ContY")); 
	}
	public void BttHidgeSet () {
		adder = 0; ///Debug.Log ( "dddddddddddddddddddddddddddddd");
		// скрываем все кнопки кроме нашей
		for (int i = 0; i < Base.Length; i++) {
			ItemShowAnim other = (ItemShowAnim)Base [i].GetComponent (typeof(ItemShowAnim));
			other.BttPressed(2);
		}
	}
	void LaunchProjectile() // появление спустя время
	{
		adder = 0; ///Debug.Log ( "dddddddddddddddddddddddddddddd");
		for (int i = 0; i < Base.Length; i++) {
			ItemShowAnim other = (ItemShowAnim)Base [i].GetComponent (typeof(ItemShowAnim));
			other.BttPressed(4);
		}
	}
	public void BttShowSet () {
		
		// скрываем все кнопки кроме нашей спустя 2 секунды
		Invoke("LaunchProjectile", 2);//this will happen after 2 seconds

	}

	public Transform target;
	public float smoothTime = 0.4F;
	private Vector3 velocity = Vector3.zero; Vector3 SetPosC;
	int adderA = 0; int adderB = 0;
	Vector3 SetVec;

	public void Exit () {
		Application.Quit();
	}
	// Update is called once per frame
	void LateUpdate()  {
		if (Input.GetKey("escape"))
		{  Application.Quit(); 	}

		adderA = adderA + 1; 
		if (adderA > 1) {
			//PlayerPrefs.GetFloat ("ContX"); PlayerPrefs.GetFloat ("ContY"); 
			SetVec = new Vector3 (PlayerPrefs.GetFloat ("ContX"), PlayerPrefs.GetFloat ("ContY"));
			//Debug.Log (PlayerPrefs.GetFloat ("ContX") + " FF " + PlayerPrefs.GetFloat ("ContY"));
			SetPosC = Vector3.SmoothDamp (SetPosC, SetVec, ref velocity, smoothTime);
			this.gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (SetPosC.x, SetPosC.y);
		}

		if (PlayerPrefs.GetInt ("WaitAnimItem") == 1) {
			adderA = 0;
			PlayerPrefs.SetInt ("WaitAnimItem", 0);
		}
	}
}
