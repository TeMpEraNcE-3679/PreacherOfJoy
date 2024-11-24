using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager S;
    public Transform collideTransform;

    public GameObject[] notes;

    private int curNote = 0;

    private void Awake()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNextNote()
    {
        int index = Random.Range(0, notes.Length);
        GameObject g = Instantiate(notes[index], collideTransform);
        
        Destroy(g, 3.0f);
    }
}
