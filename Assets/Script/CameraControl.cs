using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject editCam;

    GameObject male;

    public void mainCameraOn()
    {
        
        mainCam.SetActive(true);
        editCam.SetActive(false);
        //GameObject.Find("Camera").GetComponent<CameraMovement>().mainCameraSet();
        //male.SetActive(true);
    }

    public void editCameraOn()
    {
        mainCam.SetActive(false);
        editCam.SetActive(true);
        //male.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //male = GameObject.Find("Male");
        Debug.Log("CameraControl Creator");
        Debug.Log(PlayerPrefs.GetInt("IsCreator"));
        editCam.SetActive(true);
        mainCam.SetActive(false);
        //if (PlayerPrefs.GetInt("IsCreator") == 1) editCameraOn();
        //else mainCameraOn();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
