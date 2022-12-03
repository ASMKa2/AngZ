using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;

public class SnowmanGenerate : MonoBehaviour
{

    public string str;
    public GameObject prefab_obj;
    GameObject background;
    GameObject DestroyObj;
    GameObject SelectedObj;
    Vector3 ScreenPos;
    Vector3 RealPos;
    int mode;
    bool selected;
    Ray ray;
    RaycastHit hitData;

    void CalculatePos()
    {
        ScreenPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitData);
        RealPos = hitData.point;
    }

    void InstantiateAtTarget()
    {
        GameObject bullet = PhotonNetwork.Instantiate(str, new Vector3(RealPos.x, RealPos.y, RealPos.z), Quaternion.identity);
        if (str == "Swing") bullet.transform.Rotate(new Vector3(-90.0f, 0, 0));
    }

    void NewObject()
    {
        //prefab_obj = Resources.Load<GameObject>(str);
        CalculatePos();
        InstantiateAtTarget();
    }

    void DestroyObject()
    {
        ScreenPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitData);
        DestroyObj = hitData.collider.gameObject;
        if (DestroyObj != background) PhotonNetwork.Destroy(DestroyObj);
    }

    void SelectObject()
    {
        ScreenPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitData);
        if (selected == true)
        {
            GameObject NewSelectedObj = hitData.collider.gameObject;
            //Debug.Log(NewSelectedObj.name);
            // Debug.Log(NewSelectedObj.transform.position);
            if (NewSelectedObj == SelectedObj)
            {
                Debug.Log("here");
                NewSelectedObj.GetComponentInChildren<Light>().intensity = 0;
                selected = false;
                return;
            }
            else if (NewSelectedObj == background)
            {
                RealPos = hitData.point;
                SelectedObj.transform.position = Vector3.Lerp(SelectedObj.transform.position, RealPos, 1f);
            }
        }
        else
        {
            SelectedObj = hitData.collider.gameObject;
            Debug.Log(SelectedObj.name);
            SelectedObj.GetComponentInChildren<Light>().intensity = 20;
            selected = true;
        }
    }

    void MoveObject()
    {

    }

    public void ChangeToRocks()
    {
        mode = 0;
        str = "Rock";
    }

    public void ChangeToHouse()
    {
        mode = 0;
        str = "House";
    }

    public void ChangeToSnowman()
    {
        mode = 0;
        str = "Snowman";
    }

    public void ChangeToWell()
    {
        mode = 0;
        str = "Well";
    }

    public void ChangeToSwing()
    {
        mode = 0;
        str = "Swing";
    }

    public void ChangeToDestroy()
    {
        mode = 1;
    }

    public void ChangeToMove()
    {
        mode = 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        background = GameObject.Find("Terrain");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == true) return;
            if (mode == 0) NewObject();
            else if (mode == 1) DestroyObject();
            else if (mode == 2) SelectObject();
        }
    }
}
