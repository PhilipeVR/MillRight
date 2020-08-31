using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{   
    
    public void loadlevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.Z)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
