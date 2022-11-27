using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqChange : MonoBehaviour
{
    public Transform Hair;
    public Transform Head;
    public Transform Top;
    public Transform Bottom;
    void Start()
    {
        
    }
    public void HeadEq(){
        Head.gameObject.SetActive(true);
        Top.gameObject.SetActive(false);
        Hair.gameObject.SetActive(false);
        Bottom.gameObject.SetActive(false);
    }
    public void TopEq(){
        Head.gameObject.SetActive(false);
        Top.gameObject.SetActive(true);
        Hair.gameObject.SetActive(false);
        Bottom.gameObject.SetActive(false);
    }
    public void BottomEq(){
        Head.gameObject.SetActive(false);
        Top.gameObject.SetActive(false);
        Hair.gameObject.SetActive(false);
        Bottom.gameObject.SetActive(true);
    }
    public void HairEq(){
        Head.gameObject.SetActive(false);
        Top.gameObject.SetActive(false);
        Hair.gameObject.SetActive(true);
        Bottom.gameObject.SetActive(false);
    }



}
