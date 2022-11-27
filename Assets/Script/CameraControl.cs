using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject editCam;

    GameObject male;

    void mainCameraOn()
    {
        mainCam.SetActive(true);
        editCam.SetActive(false);
        male.SetActive(true);
    }

    void editCameraOn()
    {
        mainCam.SetActive(false);
        editCam.SetActive(true);
        male.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        male = GameObject.Find("Male");
        editCameraOn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            mainCameraOn();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            editCameraOn();
        }
    }
}
