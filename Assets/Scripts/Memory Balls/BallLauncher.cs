using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{

    [SerializeField] private Transform launcher;
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    //private BlockSpawner blockSpawner;
    private Vector3 direction;



    private LaunchPreview launchPreview;
    private List<Ball> balls = new List<Ball>();
    private int ballsReady;

    private  RaycastHit hit;

    [SerializeField] private Ball ballPrefab;

    [SerializeField] private bool isOkToDrag = false;

    public bool IsOkToDrag 
    { 
        get{ return isOkToDrag; } 
    }

    private void Awake()
    {
       // blockSpawner = FindObjectOfType<BlockSpawner>();
        launchPreview = GetComponent<LaunchPreview>();
        CreateBall();
    }

    public void ReturnBall()
    {
        ballsReady++;
        if (ballsReady == balls.Count)
        {
            //blockSpawner.SpawnRowOfBlocks();
            isOkToDrag = false;
            CreateBall();
        }
    }

    private void CreateBall()
    {
        Ball ball = null;

        if( ballsReady == 0 )
        {
            ball = Instantiate(ballPrefab) as Ball;
            balls.Add(ball);
            ballsReady++;
        }
       
    }

    private void OnMouseOver()
    {
        isOkToDrag = true;
    }


    private void Update()
    {
        
        
        // Does the ray intersect any objects excluding the player layer
        if ( Physics.Raycast( transform.position, -direction, out hit, Mathf.Infinity ) )
        {
            Debug.DrawRay( transform.position, -direction * hit.distance, Color.yellow );
            Debug.Log( "Did Hit" );
        }
         else
         {
            // Debug.DrawRay( transform.position, -direction * 10, Color.white );
          //   Debug.Log( "Did not Hit" );
         }
        
        
        if (ballsReady != balls.Count) // don't let the player launch until all balls are back.
            return;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition ) + Vector3.back * -10;
       // launcher.rotation = Quaternion.LookRotation( Vector3.back, worldPosition - transform.position );

    

        if( !isOkToDrag )
        {
            return;
        }
        

        if (Input.GetMouseButtonDown(0)  )
        {
            StartDrag( worldPosition );
        }
        else if (Input.GetMouseButton(0)  )
        {
            ContinueDrag( worldPosition );
        }
        else if (Input.GetMouseButtonUp(0) )
        {
            EndDrag();
        }

       
        
    }


    private void EndDrag()
    {
    
        float dist = Vector3.Distance( startDragPosition, endDragPosition );
        
        if( dist > 0.35f )
            StartCoroutine(LaunchBalls());
    }

    private IEnumerator LaunchBalls()
    {
        direction = endDragPosition - startDragPosition;
        Debug.Log( direction );
        direction.Normalize();
        Debug.Log( "Normalize : " + direction );

        foreach (var ball in balls)
        {
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce( -direction );

            yield return new WaitForSeconds(0.1f);
        }
        ballsReady = 0;
        
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        endDragPosition = worldPosition;

        Vector3 direction = endDragPosition - startDragPosition;
        
        Debug.Log( "Direction " + direction.z );
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        Debug.Log( "Local Rotation: " + rotation );
        //launcher.rotation = rotation;

        launchPreview.SetEndPoint(transform.position - direction);
    }

    private void StartDrag(Vector3 worldPosition)
    {
        startDragPosition = worldPosition;
        launchPreview.SetStartPoint(transform.position);
    }
}
