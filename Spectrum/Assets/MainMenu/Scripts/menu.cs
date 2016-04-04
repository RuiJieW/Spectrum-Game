using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public bool isQuit = false;

    void OnPointerClick()
    {

            SceneManager.LoadScene("MainMenu");

    }
}