﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    Rigidbody2D rb;

    public float jumpHeight;
    public float gravity = 1;
    float jumpVelocity;

    bool isDragging = false;
    Vector2 touchPosition;
    Vector2 playerPosition;
    Vector2 dragPosition;

    StairsManager stairsManager;

    public GameObject jumpEffectPrefab;
    public GameObject deadEffectPrefab;
    public GameObject stairEffectPrefab;

    bool isDead = false;
    bool isStart = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stairsManager = GameObject.Find("Stairs").GetComponent<StairsManager>();
     
    }


    void Start ()
    {
		
	}
	

	void Update ()
    {

        WaitToTouch();
        if (isDead) return;
        if (!isStart) return;


        GetInput();
        MovePlayer();
        AddGravityToPlayer();
        DeadCheck();
	}

    void WaitToTouch()
    {
        if (!isStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isStart = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().GameStart();
                playerPosition = transform.position;

            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

       
        if (other.gameObject.tag == "stair")
        {
   
            if(rb.velocity.y<=0)
            {

                Jump();
                AddScore();
                Effect(other);
                ChangeBackGroundColor(other);
                DestroyAndMakeNewStep(other);



            }

        

        }


    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Button Down");
            isDragging = true;
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Button Up");
            isDragging = false;
        }




    }

    void MovePlayer()
    {
        if(isDragging == true)
        {
            dragPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = new Vector2(playerPosition.x + (dragPosition.x - touchPosition.x), transform.position.y);

            if(transform.position.x <= -4)
            {
                transform.position = new Vector2(-4, transform.position.y);
            }
            if (transform.position.x > 4)
            {
                transform.position = new Vector2(4, transform.position.y);
            }



        }

    }


    void Jump()
    {
        jumpVelocity = gravity * jumpHeight;
        rb.velocity = new Vector2(0, jumpVelocity);
        gravity += 0.01f;

    }

    void AddScore()
    {
        GameObject.Find("GameManager").GetComponent<ScoreManager>().addScore(1);

    }

    void DestroyAndMakeNewStep(Collider2D stair)
    {
        Destroy(stair.gameObject);
        stairsManager.MakeNewStair();

    }

    void Effect(Collider2D stair)
    {
        GameObject stairEffectObj = Instantiate(stairEffectPrefab, transform.position, Quaternion.identity);
        stairEffectObj.transform.localScale = new Vector2(stair.transform.localScale.x , stair.transform.localScale.y);
        Destroy(stairEffectObj, 0.5f);

        Destroy(Instantiate(jumpEffectPrefab,transform.position,Quaternion.identity), 1f);
        
    }


    void AddGravityToPlayer()
    {
        rb.velocity = new Vector2(0, rb.velocity.y - (gravity * gravity));

    }

    void DeadCheck()
    {

        if (isDead == false && transform.position.y < Camera.main.transform.position.y -10)
        {
            isDead = true;
            if (!isStart) return;

            rb.isKinematic = true;
            rb.velocity = Vector2.zero;

            Destroy(Instantiate(deadEffectPrefab, transform.position, Quaternion.identity), 1f);
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }

    }

    void ChangeBackGroundColor(Collider2D stair)
    {
        Camera.main.backgroundColor = stair.gameObject.GetComponent<SpriteRenderer>().color;

    }
}
