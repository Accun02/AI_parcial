using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RechargeAmmo : MonoBehaviour
{
    private int ammo = 10;
    private bool isInRange;
    PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerController>();
            isInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    public void Reload(int ammo)
    {
        player.RechargeAmmo(ammo);

        player.UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange == true)
        {
            Reload(ammo);
        } 
    }

}
