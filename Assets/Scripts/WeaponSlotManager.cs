using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{   
    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;

    DamageCollider leftHandDamageCollider;
    DamageCollider rightHandDamageCollider;

    private static PlayerController playerController;

private static PlayerController PlayerControllerInstance
{
    get
    {
        if (playerController == null)
        {
            playerController = PlayerController.Instance;
        }
        return playerController;
    }
}


    private void Awake()
    {
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }
    //? Code Reference : Sebastian Graves
    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if(isLeft)
        {   leftHandSlot.currentWeapon = weaponItem;
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();
        }
        else
        {   
            rightHandSlot.currentWeapon = weaponItem;
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDamageCollider();
        }
    }

    private void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider =  leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }
    
    private void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider =  rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    public void OpenRightDamageCollider()
    {
        rightHandDamageCollider.EnableDamageCollider();
        PlayerControllerInstance.walkSpeed = 0.001f;
    PlayerControllerInstance.runSpeed = 0.001f;
    }
    
    public void OpenLeftDamageCollider()
    {
        leftHandDamageCollider.EnableDamageCollider();
        PlayerControllerInstance.walkSpeed = 0.001f;
    PlayerControllerInstance.runSpeed = 0.001f;
    }
    
    public void CloseRightHandDamageCollider()
    {
        rightHandDamageCollider.DisableDamageCollider();
        PlayerControllerInstance.walkSpeed = 3; // Ganti dengan nilai yang sesuai
    PlayerControllerInstance.runSpeed = 11;
    }
    
    public void CloseLeftHandDamageCollider()
    {
        leftHandDamageCollider.DisableDamageCollider();
        PlayerControllerInstance.walkSpeed = 3; // Ganti dengan nilai yang sesuai
    PlayerControllerInstance.runSpeed = 11;
    }
}
