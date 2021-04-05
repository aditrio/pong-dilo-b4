﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     // Pemain 1
    public PlayerMovement player1; // skrip
    private Rigidbody2D player1Rigidbody;
 
    // Pemain 2
    public PlayerMovement player2; // skrip
    private Rigidbody2D player2Rigidbody;
 
    // Bola
    public BallControl ball; // skrip
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D ballCollider;

    private bool isDebugWindowShown = false;
    public Trajectory trajectory;
    
    // Skor maksimal
    public int maxScore;

  	// Inisialisasi rigidbody dan collider
    private void Start()
    {
        player1Rigidbody = player1.GetComponent<Rigidbody2D>();
        player2Rigidbody = player2.GetComponent<Rigidbody2D>();
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Untuk menampilkan GUI
    void OnGUI()
    {
        Color oldColor = GUI.backgroundColor;

        float ballMass = ballRigidbody.mass;
        Vector2 ballVelocity = ballRigidbody.velocity;
        float ballSpeed = ballRigidbody.velocity.magnitude;
        Vector2 ballMomentum = ballMass * ballVelocity;
        float ballFriction = ballCollider.friction;

        float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
        float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
        float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
        float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

        string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100),"" + player1.GetScore());
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.GetScore());
    
        if (GUI.Button(new Rect(Screen.width/2 - 60, 35 , 120, 53), "RESTART"))
        {
            player1.ResetScore();
            player2.ResetScore();

            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        if (player1.GetScore() == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000,1000), "PLAYER ONE WINS");

            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        else if (player2.GetScore() == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");

            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        if(isDebugWindowShown)
        {
            GUI.backgroundColor = Color.red;
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);
        }
        
        else 
        {
            GUI.backgroundColor = oldColor;
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
    }
}
