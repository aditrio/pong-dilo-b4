using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public KeyCode upButton = KeyCode.W;
	public KeyCode downButon = KeyCode.S;
	public float speed = 10.0f;
	public float yBoundary = 9.0f;
	private Rigidbody2D rigidBody2D;
	private int score;

	private ContactPoint2D lastContactPoint;

    
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
    	Movement();

    }

    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

    void Movement(){
    	Vector2 velocity = rigidBody2D.velocity;

    	if(Input.GetKey(upButton))
    	{
    		velocity.y = speed;
    	}
    	else if (Input.GetKey(downButon))
    	{
    		velocity.y = -speed;
    	}else{
    		velocity.y = 0.0f;
    	}
    	rigidBody2D.velocity = velocity;

    	// boundary

    	Vector3 position = transform.position;

    	if(position.y > yBoundary) {
    		position.y = yBoundary;
    	}else if(position.y < -yBoundary){
    		position.y = -yBoundary;
    	}

    	transform.position = position;
    }


    public void IncrementScore(){
    	score++;
    }

    public void ResetScore(){
    	score = 0;
    }

    public int GetScore(){
    	return score;
    }

}
