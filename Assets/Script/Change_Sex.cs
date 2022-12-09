using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Change_Sex : MonoBehaviour
{
    private Transform transform;
    public PhotonView PV;

    public int Sex = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);

        if (PV.IsMine)
        {
            Debug.Log(Camera.main.name);
            Camera.main.GetComponent<CameraMovement>().objectTofollow = transform.GetChild(Sex).transform.Find("FollowCam").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        Vector3 pos;
        if(Input.GetKeyDown(KeyCode.Q)){
            if(Sex == 1){
                pos = transform.GetChild(1).transform.position;
                transform.GetChild(0).gameObject.transform.position = pos;
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(true);
            
            }else{
                pos = transform.GetChild(0).transform.position;
                transform.GetChild(1).gameObject.transform.position = pos;
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(false);
            }
            Sex = Sex == 1 ? 0 : 1;
            Camera.main.GetComponent<CameraMovement>().objectTofollow = transform.GetChild(Sex).transform.Find("FollowCam").transform;
        }

    }
}
