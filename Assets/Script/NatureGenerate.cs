using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;

public class NatureGenerate : MonoBehaviour
{
    public string str;
    public GameObject prefab_obj;
    GameObject NatureWorldEdit;
    public GameObject background;
    GameObject DestroyObj;
    public GameObject SelectedObj;
    Vector3 ScreenPos;
    Vector3 RealPos;
    public int mode;
    public bool selected;
    Ray ray;
    RaycastHit hitData;
    public GameObject NewSelectedObj;
    public float rotSpeed = 5f;
    public float scaleSpeed = 1f;
    int colorID = 0;
    int texID = 0;
    object[] tex;
    int R = 125;
    int G = 125;
    int B = 125;

    void CalculatePos()
    {
        ScreenPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitData);
        RealPos = hitData.point;
        Debug.Log(RealPos.x + " " + RealPos.y);
    }

    void InstantiateAtTarget()
    {
        GameObject bullet = PhotonNetwork.Instantiate(str, new Vector3(RealPos.x, RealPos.y, RealPos.z), Quaternion.identity);
        if (str == "Building") bullet.SetActive(true);
    }

    void NewObject()
    {
        // prefab_obj = Resources.Load<GameObject>(str);
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
        ScreenPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitData);
        if (selected == true)
        {
            if (mode == 2)
            {
                //GameObject NewSelectedObj = hitData.collider.gameObject;
                NewSelectedObj = hitData.collider.gameObject;
                //Debug.Log(NewSelectedObj.name);
                // Debug.Log(NewSelectedObj.transform.position);
                if (NewSelectedObj == SelectedObj)
                {
                    Debug.Log("here");
                    //NewSelectedObj.GetComponentInChildren<Light>().intensity = 0;
                    selected = false;
                    return;
                }
                else if (NewSelectedObj == background)
                {
                    RealPos = hitData.point;
                    MoveObject();
                }
            }
        }
        else
        {
            //selected = true;
            SelectedObj = hitData.collider.gameObject;
            Debug.Log(SelectedObj.name);
            //SelectedObj.GetComponentInChildren<Light>().intensity = 20;
            selected = true;
        }
    }

    void MoveObject()
    {
        SelectedObj.transform.position = Vector3.Lerp(SelectedObj.transform.position, RealPos, 1f);
    }

    void EditObject()
    {
        if (Input.GetKey("z"))
        {
            //SelectedObj.transform.localScale -= scaleSpeed * a;
            SelectedObj.transform.localScale = new Vector3(
                SelectedObj.transform.localScale.x - 1f * scaleSpeed * Time.deltaTime,
                SelectedObj.transform.localScale.y - 1f * scaleSpeed * Time.deltaTime,
                SelectedObj.transform.localScale.z - 1f * scaleSpeed * Time.deltaTime);
        }
        if (Input.GetKey("c"))
        {
            //SelectedObj.transform.localScale += scaleSpeed * a;
            SelectedObj.transform.localScale = new Vector3(
                SelectedObj.transform.localScale.x + 1f * scaleSpeed * Time.deltaTime,
                SelectedObj.transform.localScale.y + 1f * scaleSpeed * Time.deltaTime,
                SelectedObj.transform.localScale.z + 1f * scaleSpeed * Time.deltaTime);
        }
        if (Input.GetKey("t"))
        {
            SelectedObj.transform.Rotate(0f, -0.1f * rotSpeed, 0f, Space.World);
        }
        if (Input.GetKey("y"))
        {
            SelectedObj.transform.Rotate(0f, 0.1f * rotSpeed, 0f, Space.World);
        }
        if (Input.GetKey("u"))
        {
            SelectedObj.transform.Rotate(-0.1f * rotSpeed, 0f, 0f, Space.World);
        }
        if (Input.GetKey("i"))
        {
            SelectedObj.transform.Rotate(0.1f * rotSpeed, 0f, 0f, Space.World);
        }
        if (Input.GetKey("o"))
        {
            SelectedObj.transform.Rotate(0f, 0f, 0.1f * rotSpeed, Space.World);
        }
        if (Input.GetKey("p"))
        {
            SelectedObj.transform.Rotate(0f, 0f, -0.1f * rotSpeed, Space.World);
        }
        //change color
        Renderer Obj = SelectedObj.GetComponent<Renderer>();
        if (Input.GetKeyDown("l"))
        {
            colorID = (colorID + 1) % 11;
            //SelectedObj.transform.localScale += scaleSpeed * a;
            switch (colorID)
            {
                case 0:
                    Obj.material.color = Color.red;
                    break;
                case 1:
                    Obj.material.color = Color.green;
                    break;
                case 2:
                    Obj.material.color = Color.blue;
                    break;
                case 3:
                    Obj.material.color = Color.magenta;
                    break;
                case 4:
                    Obj.material.color = Color.yellow;
                    break;
                case 5:
                    Obj.material.color = Color.cyan;
                    break;
                case 6:
                    Obj.material.color = Color.white;//원래 색나오는듯?
                    break;
                case 7:
                    Obj.material.color = Color.black;
                    break;
                case 8:
                    Obj.material.color = Color.gray;
                    break;
                case 9:
                    Obj.material.color = Color.clear;// -> 이거 black이랑 효과같음
                    break;
            }
        }
        if (Input.GetKey("1"))
        {
            R = (R + 1) % 255;
            Obj.material.color = new Color(R / 255f, G / 255f, B / 255f);
        }
        if (Input.GetKey("2"))
        {
            R = (R - 1) % 255;
            Obj.material.color = new Color(R / 255f, G / 255f, B / 255f);
        }
        if (Input.GetKey("3"))
        {
            G = (G + 1) % 255;
            Obj.material.color = new Color(R / 255f, G / 255f, B / 255f);
        }
        if (Input.GetKey("4"))
        {
            G = (G - 1) % 255;
            Obj.material.color = new Color(R / 255f, G / 255f, B / 255f);
        }
        if (Input.GetKey("5"))
        {
            B = (B + 1) % 255;
            Obj.material.color = new Color(R / 255f, G / 255f, B / 255f);
        }
        if (Input.GetKey("6"))
        {
            B = (B - 1) % 255;
            Obj.material.color = new Color(R / 255f, G / 255f, B / 255f);
        }
        string texPath = "texture";
        tex = Resources.LoadAll(texPath, typeof(Texture2D));
        if (Input.GetKeyDown("k"))
        {
            texID = (texID + 1) % 3;
            //SelectedObj.transform.localScale += scaleSpeed * a;
            Obj.material.SetTexture("_MainTex", (Texture2D)tex[texID]);
        }
    }

    public void ChangeToRocks()
    {
        mode = 0;
        str = "Rock";
        selected = false;
    }

    public void ChangeToHouse()
    {
        mode = 0;
        str = "House";
        selected = false;
    }

    public void ChangeToBuilding()
    {
        mode = 0;
        str = "Building";
        selected = false;
    }

    public void ChangeToWell()
    {
        mode = 0;
        str = "Well";
        selected = false;
    }

    public void ChangeToSwing()
    {
        mode = 0;
        str = "Swing";
        selected = false;
    }

    public void ChangeToDestroy()
    {
        mode = 1;
        selected = false;
    }

    public void ChangeToMove()
    {
        mode = 2;
    }

    public void ChangeToEdit()
    {
        mode = 3;
    }

    public void ExitEditMode()
    {
        mode = -1;
        NatureWorldEdit.SetActive(false);
        GameObject.Find("GameManager").GetComponent<GameManager>().CreateHuman();
        GameObject.Find("CameraController").GetComponent<CameraControl>().mainCameraOn();
    }

    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        background = GameObject.Find("Terrain");
        NatureWorldEdit = GameObject.Find("WorldEdit");
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
            else return;
        }
        else if(mode == 3)
        {
            EditObject();
        }
    }
}
