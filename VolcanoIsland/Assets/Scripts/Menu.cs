using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void onPlayClick()
    {
        SceneManager.LoadScene("MainGame");
    }
}
