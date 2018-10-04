using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour 
{

	[SerializeField] private BallLauncher bLauncher;

	// Use this for initialization
	void Start () {

		GameObject bL = GameObject.Find( "BallLauncher" );
		
		if( bL != null )
			bLauncher = bL.GetComponent<BallLauncher>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if( !bLauncher.IsOkToDrag  )
		{
			return;
		}

		 Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
 		 Vector3 dir = Input.mousePosition - pos;
 
 		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Debug.Log( "Angle : " + angle ); 
 		transform.rotation = Quaternion.AngleAxis(angle, new Vector3( 0,0,1 ));
	}
}
