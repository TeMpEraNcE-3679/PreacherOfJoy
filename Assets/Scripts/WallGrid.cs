using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrid : MonoBehaviour
{
    public Vector3 offset = new Vector3(1.0f, 0.25f, 0.0f);
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("The player is touching the wall.");
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Sliding start!");
                GameManager.S.StartWallSliding(transform, offset);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player") && GameManager.S.isSliding)
        {
            GameManager.S.IncrementWallCount();
        }
    }
}
