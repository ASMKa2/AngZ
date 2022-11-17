using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool activeInventory = false;

    private void start(){
        inventoryPanel.SetActive(activeInventory);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}
