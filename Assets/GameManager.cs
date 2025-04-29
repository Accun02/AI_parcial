using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 public static GameManager Instance;
public PlayerController PlayerController;
[SerializeField]List <GameObject> EnemyList;

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

    public void Damage(int damage)
    {
        PlayerController.Health -= damage;
    }
   
}
