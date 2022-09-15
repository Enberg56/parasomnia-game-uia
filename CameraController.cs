using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform camTarget;
    public Vector3 camOffset;
    public Camera ThirdPersonCamera, FirstPersonCamera;
    public KeyCode CKey;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        camOffset = transform.position - camTarget.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(CKey)) //Switching between cameras input
        {
            CameraSwitch();
            
        }
    }

    void FixedUpdate()
    {
        transform.position = camTarget.transform.position + camOffset;
    }
    public void CameraSwitch() //Switching between cameras function
    {
        ThirdPersonCamera.gameObject.SetActive(!ThirdPersonCamera.gameObject.activeInHierarchy);
        FirstPersonCamera.gameObject.SetActive(!FirstPersonCamera.gameObject.activeInHierarchy);
    }
}
