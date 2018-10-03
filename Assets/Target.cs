using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	// private void OnTriggerEnter2D( Collider2D other )
	// {
	// 	if( other.name == "Ball" )
	// 	{
	// 		Debug.Log( "You got me....." );
	// 		Destroy( gameObject );
	// 	}
	// }

	private void OnCollisionEnter2D( Collision2D collision )
    {
		Debug.Log( "You got me....." );
		Destroy( gameObject );
	}
}
