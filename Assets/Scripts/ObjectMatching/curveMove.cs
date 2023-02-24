using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class curveMove : MonoBehaviour
{

	public AnimationCurve curve;
	[Range(0, 1)]
	public float speed = 1;
	Vector3 targetToReach;

	Vector3 currentPosition;
	float incrementStep;
	Transform thisTransform;
	public bool QuickSlideOnEnable = true;
	public Vector3 quickSlideDistance;// = new Vector3(-200, 0, 0);
	void Start()
	{
		thisTransform = GetComponent<Transform>();

		//assign To OriginalPos
		targetToReach = thisTransform.position;

		if (QuickSlideOnEnable)
		{
			//QuickMove To Move AnotherPos To ThisPos
			thisTransform.Translate(quickSlideDistance);
		}

	}

	void Update()
	{
		//Let's Move OriginalPos With AnimationSlide Effect
		thisTransform.position = Vector3.Lerp(thisTransform.position, targetToReach, curve.Evaluate(incrementStep));

		//Reached Original Position And Then Destroying Script
		incrementStep += speed * Time.deltaTime;
		if (incrementStep >= 1)
			Destroy(this);
	}



}
