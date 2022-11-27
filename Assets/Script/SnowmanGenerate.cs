using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanGenerate : MonoBehaviour
{

    public string str;
    // GameObject snowman_prefab;
    GameObject prefab_obj;
    Vector3 ScreenPos;
    Vector3 RealPos;

    void CalculatePos()
    {
        ScreenPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);
        RealPos = hitData.point;
        Debug.Log(RealPos.x + " " + RealPos.y + " " + RealPos.z);
    }

    void MoveToTarget()
    {
        GameObject bullet = Instantiate<GameObject>(prefab_obj, new Vector3(RealPos.x, RealPos.y, RealPos.z), Quaternion.identity);
    }

    void NewObject()
    {
        prefab_obj = Resources.Load<GameObject>(str);
        CalculatePos();
        MoveToTarget();
    }

    public void ChangeToRocks()
    {
        str = "Rock";
    }

    public void ChangeToHouse()
    {
        str = "House";
    }

    public void ChangeToSnowman()
    {
        str = "Snowman";
    }

    public void ChangeToWell()
    {
        str = "Well";
    }

    public void ChangeToSwing()
    {
        str = "Swing";
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NewObject();
        }
    }
}
