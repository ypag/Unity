// This script has to be attached to image target that we want to track. 


using UnityEngine;
using System.Collections;

public class TargetImageCoords : MonoBehaviour {

	private ImageTargetBehaviour mImageTargetBehaviour = null;
	public float XMax = Mathf.NegativeInfinity;
	public float YMax = Mathf.NegativeInfinity;
	public float XMin = Mathf.Infinity;
	public float XAbs = 0f;
	public float YAbs = 0f;
	private float newTime = 0f;
	private float oldTime = 0f; 
	public float period = 0f;
	private float newXAbs = 0f;

	// Use this for initialization
	void Start () {
		// We retrieve the ImageTargetBehaviour component
		// Note: This only works if this script is attached to an ImageTarget
		mImageTargetBehaviour = GetComponent<ImageTargetBehaviour>();

		
		if (mImageTargetBehaviour == null)
		{
			Debug.Log ("ImageTargetBehaviour not found ");
		}
		// adding co-routine here so that it doesn't get called multiple times, do not put this in update function
		StartCoroutine (TimeKeep());

	}


	// Update is called once per frame 
	//FixedUpdate makes sure that the loop takes fixed amount of time to run, this is to make sure that we get
	//reliable period value every time fixedupdate runs. 
	void FixedUpdate () {	
				if (mImageTargetBehaviour == null) {
						Debug.Log ("ImageTargetBehaviour not found");
						return;
				}
		
				Vector2 targetSize = mImageTargetBehaviour.GetSize ();
				float targetAspect = targetSize.x / targetSize.y;
		
				// We define a point in the target local reference 
				// we take the bottom-left corner of the target, 
				// just as an example
				// Note: the target reference plane in Unity is X-Z, 
				// while Y is the normal direction to the target plane
				Vector3 pointOnTarget = new Vector3 (-0.5f, 0, -0.5f / targetAspect); 
		
				// We convert the local point to world coordinates
				Vector3 targetPointInWorldRef = transform.TransformPoint (pointOnTarget);
		
				// We project the world coordinates to screen coords (pixels)
				Vector3 screenPoint = Camera.main.WorldToScreenPoint (targetPointInWorldRef);
		
				//Debug.Log ("target point in screen coords: " + screenPoint.x + ", " + screenPoint.y);
				newXAbs = XAbs;
				XAbs = Mathf.Abs (screenPoint.x);
				YAbs = Mathf.Abs (screenPoint.y);
				if (XAbs > newXAbs) {
						TimeKeep ();
				}
				

				//Debug.Log("Abs Value is:" + XAbs + "," + YAbs);
        // Calculate maximum X
				XMax = Mathf.Max (XAbs, XMax);

				XMin = Mathf.Min (XAbs, XMin);
				Vector3 ScreenPointMax = new Vector3 (XMax, 0f, YMax);
				YMax = Mathf.Max (YAbs, YMax);

		}

	void OnGUI()
	{
	// outputting the variables on screen 
		GUI.backgroundColor = Color.blue;
		GUI.Label(new Rect(10, 70, 100, 80), "Maximum X:" +XMax.ToString());
		GUI.Label (new Rect (10, 100, 100, 80), "Minimum X:" + XMin.ToString ());
		GUI.Label (new Rect (10, 130, 100, 80), "Maximum Y:" + YMax.ToString ());
		GUI.Label (new Rect (10, 180, 100, 80), "timestart: " + newTime.ToString ());
		GUI.Label (new Rect (10, 220, 100, 80), "Time Period " + period.ToString ());
		GUI.Label (new Rect (10, 250, 100, 80), "oldtime: " + oldTime.ToString ());
		GUI.Label (new Rect (10, 10, 100, 80), "Current Position X:" + XAbs.ToString ());
		GUI.Label (new Rect (10, 40, 100, 80), "Current Position Y:" + YAbs.ToString ());


	}

	IEnumerator TimeKeep()
	{
		if (XMax <= XAbs) {
			oldTime = newTime;
			newTime = Time.time;
			Debug.Log (" OLD: " + oldTime);
			Debug.Log (" NEW: " + newTime);
			//Debug.Log ("old period:" + period);
			period = (newTime - oldTime); 
				

		//Debug.Log ("new period:" + period);

	
		}
		//delay is important to reject noisy values around XMax
		yield return new WaitForSeconds (2F);
		//To call start co routine again, hence timekeep() is looped. 
		StartCoroutine (TimeKeep());
	}
	

	
}
