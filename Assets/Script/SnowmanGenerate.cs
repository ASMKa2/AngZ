using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanGenerate : MonoBehaviour
{
    GameObject snowman_prefab;
    Vector3 ScreenPos;
    Vector3 RealPos;
    void CalculatePos()
    {
        snowman_prefab = Resources.Load<GameObject>("snowman");
        ScreenPos = Input.mousePosition;
        // Debug.Log("SC : " + ScreenPos.x + " " + ScreenPos.y + " " + ScreenPos.z);
        // RealPos = Camera.main.ScreenToWorldPoint(new Vector3(ScreenPos.x, ScreenPos.y,
        //    Camera.main.transform.position.y - Terrain.activeTerrain.terrainData.GetHeight((int)ScreenPos.x, (int)ScreenPos.y)));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);
        RealPos = hitData.point;
        Debug.Log(RealPos.x + " " + RealPos.y + " " + RealPos.z);
        //RealPos = Camera.main.ScreenToWorldPoint(new Vector3(ScreenPos.x, ScreenPos.y, 
        //    Camera.main.transform.position.y - Terrain.activeTerrain.terrainData.GetHeight((int)ScreenPos.x, (int)ScreenPos.y)));


        //Debug.Log("RE : " + RealPos.x + " " + RealPos.y + " " + RealPos.z);
        // RealPos.y = Camera.main.transform.position.y - Terrain.activeTerrain.terrainData.GetHeight((int)RealPos.x, (int)RealPos.y);
        //Debug.Log("RE2 : " + RealPos.x + " " + RealPos.y + " " + RealPos.z);
        // Debug.Log(Terrain.activeTerrain.SampleHeight(Input.mousePosition));
        // TargetPos = new Vector3(RealPos.x, 100f, RealPos.z);
    }

    void MoveToTarget()
    {
        // Debug.Log("Pos : " + RealPos.x + " " + RealPos.y + " "+ RealPos.z);
        GameObject bullet = Instantiate<GameObject>(snowman_prefab, new Vector3(RealPos.x, RealPos.y, RealPos.z), Quaternion.identity);
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
            CalculatePos();
            MoveToTarget();
        }
    }
}
