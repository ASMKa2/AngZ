using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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
        GameObject bullet = Instantiate(prefab_obj, new Vector3(RealPos.x, RealPos.y, RealPos.z), Quaternion.identity);
        if (str == "Swing") bullet.transform.Rotate(new Vector3(-90.0f, 0, 0));
    }

    void NewObject()
    {
        prefab_obj = Resources.Load<GameObject>(str);
        CalculatePos();
        InstantiateAtTarget();
    }

    void DestroyObject()
    {
        ScreenPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitData);
        DestroyObj = hitData.collider.gameObject;
        if (DestroyObj != background) Destroy(DestroyObj);
    }

    void SelectObject()
    {
        if (selected  == true)
        {
            ScreenPos = Input.mousePosition;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitData);
            GameObject NewSelectedObj = hitData.collider.gameObject;
            Debug.Log(NewSelectedObj.name);
            Debug.Log(NewSelectedObj.transform.position);
            if (NewSelectedObj == SelectedObj)
            {
                Debug.Log("here");
                NewSelectedObj.GetComponentInChildren<Light>().intensity = 0;
                selected = false;
                return;
            }
            else return;
        }
        ScreenPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitData);
        SelectedObj = hitData.collider.gameObject;
        Debug.Log(SelectedObj.name);
        SelectedObj.GetComponentInChildren<Light>().intensity = 20;
        selected = true;
        MoveObject();
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
            if (mode == 0) NewObject();
            else if (mode == 1) DestroyObject();
            else if (mode == 2) SelectObject();
        }
    }
}
