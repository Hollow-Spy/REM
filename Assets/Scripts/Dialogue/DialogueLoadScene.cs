using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class DialogueLoadScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI SkipText;
    [SerializeField] string sceneName;
    float TimeHeld=0;
    bool once;
    [SerializeField] RawImage BlackFade;
    private void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void loadScene()
    {
       
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 0;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Space))
        {
          
            TimeHeld += Time.deltaTime;

             if (TimeHeld > 3 && !once)
            {
                once = true;

                loadScene();
            }
                    
        }
        else
        {

            if(TimeHeld > 0)
            {
                TimeHeld -= Time.deltaTime;
            }
            else
            {
                TimeHeld = 0;
            }
          
        }
        BlackFade.color = new Color(BlackFade.color.r, BlackFade.color.g, BlackFade.color.b, TimeHeld / 2.95f);
        SkipText.fontSize = 36 + (6 * TimeHeld);
        SkipText.color = new Color(SkipText.color.r, SkipText.color.g, SkipText.color.b, TimeHeld);

    }
}
