using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool activeInventory = false;
    public bool Sex = false;
    private void start(){
        inventoryPanel.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Q)){
            Sex = !Sex;
            if(activeInventory){
                activeInventory = !activeInventory;
                inventoryPanel.SetActive(activeInventory);
            }
        }
        if(Sex==true){
            inventoryPanel = transform.GetChild(0).gameObject;
        }else{
            inventoryPanel = transform.GetChild(1).gameObject;
        }
        if(Input.GetKeyDown(KeyCode.E)){
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}
