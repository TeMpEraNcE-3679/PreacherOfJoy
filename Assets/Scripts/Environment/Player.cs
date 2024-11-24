using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player S;
    private Rigidbody rb;
    
    private void Awake()
    {
        S = this;
        rb = GetComponent<Rigidbody>();
    }

    public void ClimbWall(Transform wallTransform, Vector3 sideOffset)
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Vector3 offset = wallTransform.up * 0.5f;
        transform.position = wallTransform.position + offset - sideOffset; 
        transform.rotation = Quaternion.LookRotation(wallTransform.forward, wallTransform.up);
    }

    public void DropFromWall()
    {
        rb.useGravity = true;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        
        rb.AddForce(transform.forward * 2.0f + Vector3.down * 5.0f, ForceMode.Impulse);
    }
}
