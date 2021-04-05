using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

	private Rigidbody2D rb;

	public float initialX;
	public float initialY;

	private float speed = 100;

 	private Vector2 trajectoryOrigin;


    // Start is called before the first frame update
    void Start()
    {
   		rb = GetComponent<Rigidbody2D>();
   		trajectoryOrigin = transform.position;
   		RestartGame();     
    }

   
    void ResetBall()
    {
    	transform.position = Vector2.zero;
    	rb.velocity = Vector2.zero;
    }

    void PushBall()
    {
    	float yRandomForce = Random.Range(-initialY, initialY);
    	float xRandomForce = Mathf.Sqrt(speed * speed - yRandomForce*yRandomForce);
    	float randomDir = Random.Range(0,2);

    	if(randomDir < 1.0f)
    	{
    		rb.AddForce(new Vector2(xRandomForce, yRandomForce));
    	}else{
    		rb.AddForce(new Vector2(-xRandomForce, yRandomForce));
    	}

    }

    void RestartGame()
    {
    	ResetBall();
    	Invoke("PushBall", 2);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
