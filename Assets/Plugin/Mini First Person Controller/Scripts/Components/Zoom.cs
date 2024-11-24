using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera camera;
    [SerializeField] private CinemachineVirtualCamera vcam;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;


    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        // camera = GetComponent<Camera>(); 
        if (vcam)
        {
            // defaultFOV = camera.fieldOfView;
            defaultFOV = vcam.m_Lens.FieldOfView;
        }
    }

    void Update()
    {
        // Update the currentZoom and the camera's fieldOfView.
        currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        vcam.m_Lens.FieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}
