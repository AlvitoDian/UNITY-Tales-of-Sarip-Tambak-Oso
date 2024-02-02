using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{   
    public bool playerInRange;

    public string ItemName;

    private int collectedItemCount = 0;
 

    public string GetItemName()
    {
        return ItemName;
    }

  /*   void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && playerInRange && SelectionManager.Instance.onTarget)
        {
            Debug.Log("Item Added in Inventory");

            Destroy(gameObject);
        }
    } */

    //? Code Reference : Mike's Code
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && playerInRange && SelectionManager.Instance.onTarget/*  && collectedItemCount < targetItemCount */)
        {
            Debug.Log("Item Added in Inventory");

            Destroy(gameObject);

            collectedItemCount++;
            SelectionManager.Instance.UpdateItemCount(collectedItemCount);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
