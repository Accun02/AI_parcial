using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReloadHealth : MonoBehaviour
{
    private int health = 100;
    private bool isInRange;

    PlayerController player;

    PlayerHealth playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerController>();
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();

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

    void Reload(int health)
    {
        player.currentHealth += health;
        player.UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange == true)
        {
            Reload(health);
        }
    }
}
