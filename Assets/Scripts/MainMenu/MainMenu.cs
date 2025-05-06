using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //Carga la escena de Gameplay.
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //Cierra el videojuego al tocar el botón exit en el Main Menu.
    public void ExitGame()
    {
        Application.Quit();
    }
}
