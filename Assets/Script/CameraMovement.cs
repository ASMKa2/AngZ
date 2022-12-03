using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraMovement : MonoBehaviour
{
    public Transform human;
    GameObject male;
    GameObject female;
    public Transform objectTofollow;
    public float followSpeed = 10f;
    public float sensitivity = 500f;
    public float clampAngle = 70f;
    public float ScrollSpeed = 1f;

    private float rotX;
    private float rotY;
    public float ScrollWheel;
    public float curScroll;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;
    int editmode;
    int sex;

    public void mainCameraSet()
    {
        Debug.Log("main camera set");
        editmode = 0;
        //human = GameObject.Find("human(Clone)").GetComponent<Transform>();

        objectTofollow = human.GetChild(sex).transform.Find("FollowCam").gameObject.GetComponent<Transform>();
        ScrollWheel = -0.5f;
    }

    public void ChangeSex()
    {
        sex = 1 - sex;
        objectTofollow = human.GetChild(sex).transform.Find("FollowCam").gameObject.GetComponent<Transform>();
    }


    // Start is called before the first frame update
    void Start()
    {
        editmode = 1;
        sex = 0;
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        ScrollWheel = 0f;
        ScrollSpeed = 200f;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;
        /*
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;

        if (editmode == 0 && Input.GetKey(KeyCode.Q))
        {
            ChangeSex();
        }
        ScrollWheel += Input.GetAxis("Mouse ScrollWheel");

    }

    void LateUpdate()
    {

        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed + Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        Vector3 CameraDirection = realCamera.localRotation * Vector3.forward;
        realCamera.localPosition += CameraDirection * Time.deltaTime * ScrollWheel * ScrollSpeed;
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);

    }
}
