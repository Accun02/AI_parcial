using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    // Start is called before the first frame update


    private void Reset()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
