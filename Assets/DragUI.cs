using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DragUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	public Vector2 Canvas_Size;
	public Vector2 a;
	//public float b;
	public RectTransform Rt; /***/
	public GameObject bot; /****/
	private bool mouseDown = false;
	private Vector3 startMousePos;
	private Vector3 startPos;


	public void OnPointerDown(PointerEventData ped) 
	{
		//BoxCollider2D.widht = 
		//Canvas.
		mouseDown = true;
		startPos = transform.position;
		startMousePos = Input.mousePosition;
	}
	/*******************************************/
	void Get()
	{
		Rt = bot.GetComponent<RectTransform>();
		a.x = Rt.rect.height;
		a.y = Rt.rect.width;
		//Rt.rect.center = a;
	}

	/***********************************************/

	public void OnPointerUp(PointerEventData ped) 
	{
		mouseDown = false;
	}


	void Update () 
	{
		if (mouseDown) {
			Vector3 currentPos = Input.mousePosition;
			Vector3 diff = currentPos - startMousePos;
			Vector3 pos = startPos + diff;
			transform.position = pos;
		}
		//it is the test on the canvas  limits


		if(transform.position.x <= -(Rt.rect.width/2 ))
		{
			transform.position = new Vector2(-(Rt.rect.width/2), transform.position.y);
		}
		else if (transform.position.x >= (Rt.rect.width/2))
		{
			transform.position = new Vector2((Rt.rect.width/2), transform.position.y);
			//transform.position.x = b; //Rt.rect.width;

		}
		if(transform.position.y <= -(Rt.rect.width /2))
		{
			transform.position = new Vector2(transform.position.x, -(Rt.rect.width/2 ));
		}
		else if(transform.position.y >= (Rt.rect.width/2 ))
		{
			transform.position = new Vector2(transform.position.x, (Rt.rect.width/2));
		}
		//a = new Vector2(0, 0);

	}
}
