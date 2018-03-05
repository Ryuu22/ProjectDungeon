using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayInterface : MonoBehaviour
{
    SceneMaster sceneM;
    Player player;
    Image losePanel;
    Image winPanel;
    bool activateWinPanel;
    float counter;
    float counter2;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sceneM = GameObject.FindGameObjectWithTag("SceneMaster").GetComponent<SceneMaster>();
        losePanel = GameObject.Find("LosePanel").GetComponent<Image>();
        winPanel = GameObject.Find("WinPanel").GetComponent<Image>();
    }

	void Update ()
    {
        if (activateWinPanel)
        {
            counter += Time.deltaTime / 2;
            winPanel.color = new Color(winPanel.color.r, winPanel.color.g, winPanel.color.b, counter);
        }
        if (player.IsDead)
        {
            counter2 += Time.deltaTime;
            if(counter2 >= 0.7f)
            {
                counter += Time.deltaTime / 2;
                losePanel.color = new Color(losePanel.color.r, losePanel.color.g, losePanel.color.b, counter);

                if(counter >= 1)
                {
                    if(Input.anyKeyDown)
                    {
                        sceneM.LoadGameplayScreen();
                        counter = 0;
                        counter2 = 0;
                    }
                }
            }
        }
    }

    public void WinPanel()
    {
        if(!activateWinPanel)
        {
            activateWinPanel = true;
        }
    }
}
