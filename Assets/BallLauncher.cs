using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    //private BlockSpawner blockSpawner;

    private Vector3 direction;

    private LaunchPreview launchPreview;
    private List<Ball> balls = new List<Ball>();
    private int ballsReady;

    [SerializeField]
    private Ball ballPrefab;

    [SerializeField] private bool isOkToDrag = false;

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
        else if( ballsReady == 1 )
        {
            
        }

        
     
    }

    private void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over " + gameObject.name );
        isOkToDrag = true;
    }

    private void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject. " + gameObject.name );
        //isOkToDrag = false;
    }

    private void Update()
    {
        if (ballsReady != balls.Count) // don't let the player launch until all balls are back.
            return;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;

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
        Vector3 minDirection = new Vector3( 0.1f, 0.1f, 0.0f );

        Debug.Log( Vector3.Distance( minDirection, direction ) );
        StartCoroutine(LaunchBalls());
    }

    private IEnumerator LaunchBalls()
    {
        direction = endDragPosition - startDragPosition;
        direction.Normalize();

        foreach (var ball in balls)
        {
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(-direction);

            yield return new WaitForSeconds(0.1f);
        }
        ballsReady = 0;
        
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        endDragPosition = worldPosition;

        Debug.Log( endDragPosition );

        Vector3 direction = endDragPosition - startDragPosition;

        Debug.Log( "Direction " + direction );

        launchPreview.SetEndPoint(transform.position - direction);
    }

    private void StartDrag(Vector3 worldPosition)
    {
        startDragPosition = worldPosition;
        launchPreview.SetStartPoint(transform.position);
    }
}
