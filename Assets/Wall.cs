using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour 
{

	[SerializeField] private GameObject topWall;
	[SerializeField] private GameObject bottomWall;
	[SerializeField] private GameObject leftWall;
	[SerializeField] private GameObject righttWall;
	private void Start()
	{
		var dist = (transform.position - Camera.main.transform.position).z;

		var topBorder = Camera.main.ViewportToWorldPoint( new Vector3( 0, 1, dist ) ).y;
	    var bottomBorder = Camera.main.ViewportToWorldPoint( new Vector3( 0, 0, dist ) ).y;
 		var leftBorder = Camera.main.ViewportToWorldPoint( new Vector3(0, 0, dist)).x;
 		var rightBorder = Camera.main.ViewportToWorldPoint( new Vector3(1, 0, dist)).x;

		Debug.Log( "TOP BORDER: " + topBorder );

		topWall.transform.position = new Vector2( 0.0f, topBorder );
		bottomWall.transform.position = new Vector2( 0.0f, bottomBorder );

		leftWall.transform.position = new Vector2(  leftBorder, 0.0f );
		righttWall.transform.position = new Vector2( rightBorder, 0.0f );




	}
}
