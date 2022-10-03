using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public Transform target;
	public float smoothing;
	public Vector2 maxPos, maxipos;
	public Vector2 minPos, minipos;

	public bool isInfinite;

	public bool autoScroll;
	public float autoScrollSpeed;

	// Use this for initialization
	void Start () {
		maxipos = maxPos;
		minipos = minPos;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		
		if(transform.position != target.position && isInfinite == false)
		{
			Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
			targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
			targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);
			if(!autoScroll)
			{
				transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
			}
			
		}
		else if(transform.position != target.position && !autoScroll)
		{
			Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
		}

		if(autoScroll)
		{
			transform.Translate(Vector3.right * Time.deltaTime * autoScrollSpeed);
			transform.position = new Vector2(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x), Mathf.Clamp(transform.position.y, minPos.y, maxPos.y));
		}
	}
}
