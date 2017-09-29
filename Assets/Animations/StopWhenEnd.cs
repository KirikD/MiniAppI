using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWhenEnd : MonoBehaviour {
	public Animator ObjAnim;
	// Use this for initialization
	void Start () {
		ObjAnim.speed = 1.0f; 
	}
	
	// Update is called once per frame
	void Update () {
		float AnimTime = ObjAnim.GetCurrentAnimatorStateInfo (0).normalizedTime;
		if (AnimTime > 0.95f) {
			ObjAnim.speed = 0.0f; 
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
}
