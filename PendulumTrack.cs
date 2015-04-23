// This script has to be attached to image target that we want to track. 
// Tracks motion of the pendulum and calculates time period of the periodic motion, prints it on the screen.
// Image target has to be defined in the unity scene

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
	public float midpointX = 0f;

	// Use this for initialization
	void Start () {
		// We retrieve the ImageTargetBehaviour component
		// Note: This only works if this script is attached to an ImageTarget
		mImageTargetBehaviour = GetComponent<ImageTargetBehaviour>();

		
		if (mImageTargetBehaviour == null)
		{
			Debug.Log ("ImageTargetBehaviour not found ");
		}
		StartCoroutine (TimeKeep());

	}


	// Update is called once per frame
	void FixedUpdate () 
	{	
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

						newXAbs = XAbs;
						XAbs = Mathf.Abs (screenPoint.x);
						YAbs = Mathf.Abs (screenPoint.y);
				
				

						//Debug.Log("Abs Value is:" + XAbs + "," + YAbs);

						XMax = Mathf.Max (XAbs, XMax);
						//midpointX = Screen.width / 2;


				
						XMin = Mathf.Min (XAbs, XMin);
						Vector3 ScreenPointMax = new Vector3 (XMax, 0f, YMax);
						YMax = Mathf.Max (YAbs, YMax);

				
				

	
						//Debug.Log ("Maximum between x and y " + XMax + "," + YMax + " Min X is: " + XMin);
						//GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
						//sphere.transform.position = new Vector3 (XMax, 1F, YMax);
						//sphere.transform.localScale = new Vector3(
		
				
	}

	void OnGUI()
	{
	
		GUIStyle style = new GUIStyle ();
		style.fontSize = 30;
		GUI.color = Color.green;
		//style.font.material.color = Color.green;
		//GUI.contentColor = Color.green;

	
		GUI.Label(new Rect(10, 70, 100, 80), "Maximum X:" +XMax.ToString());
		GUI.Label (new Rect (10, 100, 100, 80), "Minimum X:" + XMin.ToString ());
		GUI.Label (new Rect (10, 130, 100, 80), "Maximum Y:" + YMax.ToString ());
		GUI.Label (new Rect (10, 180, 100, 80), "timestart: " + newTime.ToString ());
		GUI.Box (new Rect (100, 70, 100, 80), "Time Period:  " + period.ToString (), style);
		GUI.Label (new Rect (10, 250, 100, 80), "oldtime: " + oldTime.ToString ());
		GUI.Label (new Rect (10, 10, 100, 80), "Current Position X:" + XAbs.ToString ());
		GUI.Label (new Rect (10, 40, 100, 80), "Current Position Y:" + YAbs.ToString ());


	}
	//float x = 0f;

	IEnumerator TimeKeep()
	{
		if (XMax <= XAbs) {
			oldTime = newTime;
			newTime = Time.time;
			Debug.Log (" OLD: " + oldTime);
			Debug.Log (" NEW: " + newTime);
			//Debug.Log ("old period:" + period);
			period = (newTime - oldTime); 
			//XMax = 0f; 
				

		//Debug.Log ("new period:" + period);

	
		}

		yield return new WaitForSeconds (1F);
		StartCoroutine (TimeKeep());
		//XMax = Mathf.NegativeInfinity;

	}
	

	
}
