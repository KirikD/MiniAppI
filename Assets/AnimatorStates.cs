using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStates : MonoBehaviour {
	public Animator ObjAnim;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("DellTXT", 0) ;
		jumpToTime (currentAnimationName (),0.5f);
		ObjAnim.speed = 0.0f;
	}
	public void HigeTex () 
	{
	   PlayerPrefs.SetInt ("HigeText", 1);
    }

	float Stopper = 1;
	// Update is called once per frame

	void Update() {
		AdderRestore =  AdderRestore + 1;
		if ( AdderRestore == 50)
		{
			PlayerPrefs.SetInt ("RestoreAllButtons", 1); // активировали кнопку при возвращении в главное меню

		}
	}

	void LateUpdate() {
		// Resources/ParagraphInside.txt   Resources/Capsters/03/ParagraphInside.txt
		float AnimTime = ObjAnim.GetCurrentAnimatorStateInfo (0).normalizedTime;
		if (AnimTime > Stopper) {
			ObjAnim.speed = 0.0f; 
		} 
		//Debug.Log ( AnimTime);

	}
	public void BttPressed (int State) {
		HigeTex ();
		if (State == 1) {
			jumpToTime (currentAnimationName (), 0.5f);
			ObjAnim.speed = 1.0f;
			Stopper = 0.999f;
		}
		if (State == 2) {
			jumpToTime (currentAnimationName (), 0.0f);
			ObjAnim.speed = 1.0f;
			Stopper = 0.5f;
		}
	}
	public GameObject DellMeny;
	public void DellMenyParagr () 
	{
		DellMeny.transform.position = new Vector3 ( 0 , 10 , 0 );
		AdderRestore = 0;
	}
	public void Restore () 
	{
	AdderRestore = 0;
}
	int AdderRestore = 0;
	public void TextHige() { // скрываем текст
		PlayerPrefs.SetInt("TextHige", -1) ;
		PlayerPrefs.SetInt("DellTXT", 1) ;
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
}
