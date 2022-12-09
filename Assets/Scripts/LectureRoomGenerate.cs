using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;

public class LectureRoomGenerate : MonoBehaviour
{
    public string str;
    GameObject prefab_obj;
    GameObject NatureWorldEdit;
    GameObject background;
    GameObject DestroyObj;
    public GameObject SelectedObj;
    Vector3 ScreenPos;
    Vector3 RealPos;
    public int mode;
    public bool selected;
    Ray ray;
    RaycastHit hitData;
    public GameObject NewSelectedObj;
    float rotSpeed = 5f;
    float scaleSpeed = 1f;
    int colorID = 0;
    int texID = 0;
    object[] tex;
    int R = 125;
    int G = 125;
    int B = 125;

    public enum ActivePanel
    {
        MAIN = 0,
        STATIONERY = 1,
        PERSON = 2,
        WALLPROPS = 3,
        ECT = 4
    }

    public ActivePanel activePanel = ActivePanel.MAIN;

    public GameObject[] panels;

    private void ChangePanel(ActivePanel panel)
    {
        foreach (GameObject _panel in panels)
        {
            _panel.SetActive(false);
        }
        panels[(int)panel].SetActive(true);
    }

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
        //if (str == "Swing") bullet.transform.Rotate(new Vector3(-90.0f, 0, 0));
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
                else
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

    public void ChangeToMain()
    {
        mode = 0;
        selected = false;
        ChangePanel(ActivePanel.MAIN);
    }
    public void ChangeToStationery()
    {
        mode = 0;
        selected = false;
        ChangePanel(ActivePanel.STATIONERY);
    }
    public void ChangeToPerson()
    {
        mode = 0;
        selected = false;
        ChangePanel(ActivePanel.PERSON);
    }
    public void ChangeToWallProps()
    {
        mode = 0;
        selected = false;
        ChangePanel(ActivePanel.WALLPROPS);
    }
    public void ChangeToEct()
    {
        mode = 0;
        selected = false;
        ChangePanel(ActivePanel.ECT);
    }
    public void ChangeToBackpack1()
    {
        mode = 0;
        str = "LectureRoom/Backpack 1";
        selected = false;
    }
    public void ChangeToBackpack2()
    {
        mode = 0;
        str = "LectureRoom/Backpack 2";
        selected = false;
    }
    public void ChangeToBIN()
    {
        mode = 0;
        str = "LectureRoom/Bin";
        selected = false;
    }
    public void ChangeToBook1()
    {
        mode = 0;
        str = "LectureRoom/Book 1";
        selected = false;
    }
    public void ChangeToBook2()
    {
        mode = 0;
        str = "LectureRoom/Book 2";
        selected = false;
    }
    public void ChangeToBook3()
    {
        mode = 0;
        str = "LectureRoom/Book 3";
        selected = false;
    }
    public void ChangeToBook4()
    {
        mode = 0;
        str = "LectureRoom/Book 4";
        selected = false;
    }
    public void ChangeToBook5()
    {
        mode = 0;
        str = "LectureRoom/Book 5";
        selected = false;
    }
    public void ChangeToChair1()
    {
        mode = 0;
        str = "LectureRoom/Chair 1";
        selected = false;
    }
    public void ChangeToChair2()
    {
        mode = 0;
        str = "LectureRoom/Chair 2";
        selected = false;
    }
    public void ChangeToDesk1()
    {
        mode = 0;
        str = "LectureRoom/Desk 1";
        selected = false;
    }
    public void ChangeToDesk2()
    {
        mode = 0;
        str = "LectureRoom/Desk 2";
        selected = false;
    }
    public void ChangeToEraser()
    {
        mode = 0;
        str = "LectureRoom/Eraser";
        selected = false;
    }
    public void ChangeToEyewear()
    {
        mode = 0;
        str = "LectureRoom/Eyewear";
        selected = false;
    }
    public void ChangeToFiles()
    {
        mode = 0;
        str = "LectureRoom/Files";
        selected = false;
    }
    public void ChangeToMarkers()
    {
        mode = 0;
        str = "LectureRoom/Markers";
        selected = false;
    }
    public void ChangeToNewspaper()
    {
        mode = 0;
        str = "LectureRoom/Newspaper";
        selected = false;
    }
    public void ChangeToNotebook()
    {
        mode = 0;
        str = "LectureRoom/Notebook";
        selected = false;
    }
    public void ChangeToNoticeBoard()
    {
        mode = 0;
        str = "LectureRoom/Notice board";
        selected = false;
    }
    public void ChangeToPaper1()
    {
        mode = 0;
        str = "LectureRoom/Paper 1";
        selected = false;
    }
    public void ChangeToPaper2()
    {
        mode = 0;
        str = "LectureRoom/Paper 2";
        selected = false;
    }
    public void ChangeToPen1()
    {
        mode = 0;
        str = "LectureRoom/Pen 1";
        selected = false;
    }
    public void ChangeToPen2()
    {
        mode = 0;
        str = "LectureRoom/Pen 2";
        selected = false;
    }
    public void ChangeToPen3()
    {
        mode = 0;
        str = "LectureRoom/Pen 3";
        selected = false;
    }
    public void ChangeToPencil()
    {
        mode = 0;
        str = "LectureRoom/Pencil";
        selected = false;
    }
    public void ChangeToPoster1()
    {
        mode = 0;
        str = "LectureRoom/Poster 1";
        selected = false;
    }
    public void ChangeToPoster2()
    {
        mode = 0;
        str = "LectureRoom/Poster 2";
        selected = false;
    }
    public void ChangeToSpeak()
    {
        mode = 0;
        str = "LectureRoom/Speak";
        selected = false;
    }
    public void ChangeToTablet()
    {
        mode = 0;
        str = "LectureRoom/Tablet";
        selected = false;
    }
    public void ChangeToWallPapers()
    {
        mode = 0;
        str = "LectureRoom/Wall papers";
        selected = false;
    }
    public void ChangeToWhiteBoardEraser()
    {
        mode = 0;
        str = "LectureRoom/Whiteboard eraser";
        selected = false;
    }
    public void ChangeToWhiteBoard()
    {
        mode = 0;
        str = "LectureRoom/Whiteboard";
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
        GameObject.Find("CameraControl").GetComponent<CameraControl>().mainCameraOn();
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
        else if (mode == 3)
        {
            EditObject();
        }
    }
}
