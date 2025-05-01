using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> EnemyList;

    public GameObject PlayerController;

    public static GameManager Instance;

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

    public void Dead()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ShowVictoryText()
    {
        SceneManager.LoadScene("Win");
    }
}
