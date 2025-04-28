using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 public  static GameManager Instance;

[SerializeField]List <GameObject> EnemyList;
 [SerializeField]PlayerController playerController;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(Instance);
        }
        DontDestroyOnLoad(Instance);
    }
  

    public void Damage( int damage)
    {
        playerController.Health -= damage;
        Debug.Log(playerController.Health);
    }

}
