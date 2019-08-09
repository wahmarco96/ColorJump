﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsManager : MonoBehaviour {

    public GameObject StairPrefab;
    int stairIndex = 0;

    float stairWidth = 3;
    float stairHeight = 0.5f;

    float hue;

	
	void Start ()
    {
        InitColor();
        for (int i = 0; i < 5; i++)
        {

            MakeNewStair();
        }
        

    }

    void InitColor()
    {
        hue = Random.Range(0, 1f);
        Camera.main.backgroundColor = Color.HSVToRGB(hue, 0.6f, 0.8f);

    }


    void Update ()
    {
		
	}

    public void MakeNewStair()
    {
        int randomPositionX;
        if (stairIndex == 0)
            randomPositionX = 0;
        else
            randomPositionX = Random.Range(-4, 5);
            

        Vector2 newPosition = new Vector2(randomPositionX, stairIndex * 5);

        GameObject newStair = Instantiate(StairPrefab, newPosition, Quaternion.identity);
        newStair.transform.SetParent(transform);
        newStair.transform.localScale = new Vector2(stairWidth, stairHeight);

        SetColor(newStair);


        DecreaseStairWidth();
        stairIndex++;


    }

    void SetColor(GameObject newStair)
    {
        if(Random.Range(0,3) !=0)
        {
            hue += 0.11f;
            if(hue >= 1)
            {
                hue -= 1;

            }

        }

        newStair.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hue , 0.6f, 0.8f);

    }

    void DecreaseStairWidth()
    {
        stairWidth -= 0.01f;
        if (stairWidth < 1) stairWidth = 1;
    }
}

