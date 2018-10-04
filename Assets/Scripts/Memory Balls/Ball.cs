using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	private new Rigidbody2D rigidBody2D;
	[SerializeField] private Vector2 direction;

	[SerializeField] private float moveSpeed = 10f;

	private SpriteRenderer renderer;

	private BallLauncher ballLauncher;

	private void Awake()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
		renderer = GetComponent<SpriteRenderer>();
		
        ballLauncher = FindObjectOfType<BallLauncher>();
    
	}

	void Start()
	{
		//Vector2 direction = new Vector2( Random.Range( 0f, 1f ), Random.Range( 0f , 1f ) );
		//rigidBody2D.AddForce( direction );
		//renderer.material.color  = new Color( Random.Range( 0f, 1.0f ), Random.Range( 0f, 1.0f ), Random.Range( 0f, 1.0f ), 1.0f );
	}

	private void Update()
	{
		rigidBody2D.velocity = rigidBody2D.velocity.normalized * moveSpeed;
	
	}

	private void OnCollisionEnter2D( Collision2D collision )
	{
		if( collision.collider.tag == "Target" )
		{
			ballLauncher.ReturnBall();
			gameObject.SetActive( false );
		}
	}
}
