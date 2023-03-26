using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] string MainMenuScene;
    [SerializeField] GameObject BlackFade;
    public void LoadCheckpoint()
    {
        BlackFade.SetActive(true);
        Invoke("CheckpointLoad", 3);
       
    }

    void CheckpointLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void LoadMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    public void LoadMainmenu()
    {
        BlackFade.SetActive(true);
        Invoke("LoadMenu", 3);
    }


}
