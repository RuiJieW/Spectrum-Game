using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    //checks if R, M or ESC buttons are pressed within the scene

    private KeyCode[] desiredKeys = { KeyCode.R, KeyCode.M, KeyCode.Escape };

    void Update()
    {
        if (HasALetterBeenPressed())
        {
            Restart();
            Menu();
            Exit();
            Debug.Log("A letter has been pressed.");
        }
    }

    public bool HasALetterBeenPressed()
    {
        foreach (KeyCode keyToCheck in desiredKeys)
        {
            if (Input.GetKeyDown(keyToCheck))
                return true;
        }
        return false;
    }

    public void Restart()
    {
        Debug.Log("this restart is working");
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Menu()
    {
        Debug.Log("this return menu is working");
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }


    public void Exit()
    {
        Debug.Log("this exit is working");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
