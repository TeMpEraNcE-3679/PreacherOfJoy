using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameGrid : Grid
{
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.S.isSliding)
        {
            if (collision.transform.CompareTag("Player"))
            {
                if (!hasBeenColored)
                {
                    GameManager.S.IncrementGrids();
                    hasBeenColored = true;
                }
                Debug.Log("I am touched by a player, I am a wall.");
                NextColor();
            }
        }
    }
}
