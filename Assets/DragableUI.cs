using UnityEngine;
using UnityEngine.EventSystems;

public class DragableUI : UIBehaviour, IBeginDragHandler, IDragHandler
{
	/// <summary>
	/// The RectTransform that we are able to drag around.
	/// if null: the transform this Component is attatched to is used.
	/// </summary>
	public RectTransform dragObject;

	/// <summary>
	/// The area in which we are able to move the dragObject around.
	/// if null: canvas is used
	/// </summary>
	public RectTransform dragArea;

	private Vector2 originalLocalPointerPosition;
	private Vector3 originalPanelLocalPosition;

	private RectTransform dragObjectInternal
	{
		get
		{
			if (dragObject == null)
				return (transform as RectTransform);
			else
				return dragObject;
		}
	}

	private RectTransform dragAreaInternal
	{
		get
		{
			if (dragArea == null)
			{
				RectTransform canvas = transform as RectTransform;
				while (canvas.parent != null && canvas.parent is RectTransform)
				{
					canvas = canvas.parent as RectTransform;
				}
				return canvas;
			}
			else
				return dragArea;
		}
	}
	float OldPos = 0;
	public void OnBeginDrag(PointerEventData data)
	{
		originalPanelLocalPosition = dragObjectInternal.localPosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out originalLocalPointerPosition);
		OldPos = dragObjectInternal.localPosition.x;
	}

	public void OnDrag(PointerEventData data)
	{
		Vector2 localPointerPosition;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out localPointerPosition))
		{
			Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
			dragObjectInternal.localPosition = originalPanelLocalPosition + offsetToOriginal;
			dragObjectInternal.localPosition = new Vector3 (OldPos,dragObjectInternal.localPosition.y,0);
		}

		ClampToArea();
	}
	public GameObject StopDragUp; public GameObject StopDragEnd;
	void Start () {

	}
	int Adder = 0;

	// Update is called once per frame 	 PlayerPrefs.SetInt("ActivateDragger", 0) ;	
	int SavePosUp = 10; float PosUp = 0; int SavePosEnd =10; float PosEnd = 0;
	void Update () {
		if (PlayerPrefs.GetInt ("ActivateDragger") == 1) {
			Adder = Adder + 1;
			if (Adder == 1) {
				StopDragUp = GameObject.Find ("BlockText1");
				StopDragEnd = GameObject.Find (PlayerPrefs.GetString ("EndDragUI"));
			}
			//StopDragUp.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0,0);
		
			if (StopDragUp.GetComponent<RectTransform> ().position.y > -9959) {
				SavePosUp = SavePosUp + 1;
				if (SavePosUp == 1) {
					PosUp = this.gameObject.GetComponent<RectTransform> ().position.y;
				}
			} else {
				SavePosUp = 0;
			}

			if (StopDragEnd.GetComponent<RectTransform> ().position.y < 15300) {
				SavePosEnd = SavePosEnd + 1;
				if (SavePosEnd == 1) {
					PosEnd = this.gameObject.GetComponent<RectTransform> ().position.y;
				}
			} else {
				SavePosEnd = 0;
			}
			if (StopDragUp.GetComponent<RectTransform> ().position.y < 15) {
				this.gameObject.GetComponent<RectTransform> ().position = new Vector3 (this.gameObject.GetComponent<RectTransform> ().position.x,-55,this.gameObject.GetComponent<RectTransform> ().position.z);
			}

			float Minus = StopDragUp.GetComponent<RectTransform> ().position.y - StopDragEnd.GetComponent<RectTransform> ().position.y;
			//Debug.Log (Minus + "NNNNNNN " + StopDragUp.GetComponent<RectTransform> ().position + " GG " + StopDragEnd.GetComponent<RectTransform> ().position + " B " + this.gameObject.GetComponent<RectTransform> ().position);
			if ( StopDragEnd.GetComponent<RectTransform> ().position.y > -52) { 
				this.gameObject.GetComponent<RectTransform> ().position = new Vector3 (this.gameObject.GetComponent<RectTransform> ().position.x, this.gameObject.GetComponent<RectTransform> ().position.y-4,this.gameObject.GetComponent<RectTransform> ().position.z);
			}
		}



	}
	// Clamp panel to dragArea
	private void ClampToArea()
	{
		Vector3 pos = dragObjectInternal.localPosition;

		Vector3 minPosition = dragAreaInternal.rect.min - dragObjectInternal.rect.min;
		Vector3 maxPosition = dragAreaInternal.rect.max - dragObjectInternal.rect.max;

		pos.x = Mathf.Clamp(dragObjectInternal.localPosition.x, minPosition.x, maxPosition.x);
		//pos.y = Mathf.Clamp(dragObjectInternal.localPosition.y, minPosition.y, maxPosition.y);

		dragObjectInternal.localPosition = pos;
	}
}