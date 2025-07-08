using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadHealth : MonoBehaviour
{
    private int health = 100;
    private bool isInRange;
    PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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

    void Reload(int health)
    {
        player.currentHealth += health;
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
