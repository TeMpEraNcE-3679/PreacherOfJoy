using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Virtual Cameras")] 
    [SerializeField] private CinemachineVirtualCamera firstPerson;
    [SerializeField] private CinemachineVirtualCamera[] overviewCams;
    
    public static CameraManager S;

    private int curPriority = 0;

    public bool isInSkyview { get; private set; }
    
    private void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        firstPerson.Priority = 1;
        foreach (var cam in overviewCams)
        {
            cam.Priority = curPriority;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetOverviewPerspective(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetOverviewPerspective(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetOverviewPerspective(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetOverviewPerspective(3);
        } else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetOverviewPerspective(4);
        }

        if (isInSkyview)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnFirstPerson();
            }
        }
    }

    private void PrioritizeCamera(CinemachineVirtualCamera vcam)
    {
        vcam.Priority = ++curPriority;
    }

    public void ReturnFirstPerson()
    {
        PrioritizeCamera(firstPerson);
        isInSkyview = false;
    }

    public void SetOverviewPerspective(int i)
    {
        isInSkyview = true;
        if(i < 0 || i >= overviewCams.Length) return;
        
        PrioritizeCamera(overviewCams[i]);
    }
    
}
