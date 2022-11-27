using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Change_Sex : MonoBehaviour
{
    public bool Sex = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos;
        if(Input.GetKeyDown(KeyCode.Q)){
            Sex = !Sex;
            if(Sex){
                pos = transform.GetChild(1).transform.position;
                transform.GetChild(0).gameObject.transform.position = pos;
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(true);
            
            }else{
                pos = transform.GetChild(0).transform.position;
                transform.GetChild(1).gameObject.transform.position = pos;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        
    }
}
