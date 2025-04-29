using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
 public static GameManager Instance;
public GameObject PlayerController;
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



    private void Update()
    {
            if (PlayerController.IsDestroyed()) 
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

}
