using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject RestOfTheGame;

    [SerializeField] FontTyper Game, Over, RtoRetry, EtoLeave;

    [SerializeField] GameObject Texts;

    [SerializeField] string  MenuScene;
    [SerializeField] GameObject ClickSound;

    void Start()
    {
        RestOfTheGame.SetActive(false);
        Cursor.visible = false;

    }


    public void ReadGameOver()
    {
        StartCoroutine(GameOverScreenNumerator());
    }

    IEnumerator GameOverScreenNumerator()
    {
        Game.Type("GAME");
        yield return new WaitForSeconds(1);
        Over.Type("OVER");
        yield return new WaitForSeconds(1);
        RtoRetry.Type("R to restart");
        yield return new WaitForSeconds(1);
        EtoLeave.Type("E to return to menu");

        bool waiting = true;
        bool Restart=false;
        while(waiting)
        {
            yield return null;
            if(Input.GetKeyDown(KeyCode.R))
            {
                waiting = false;
                Restart = true;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                waiting = false;
                
            }


        }
        Texts.SetActive(false);
        Instantiate(ClickSound, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        if(Restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(MenuScene);

        }

    }

}
