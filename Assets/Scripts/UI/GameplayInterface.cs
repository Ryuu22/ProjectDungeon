using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayInterface : MonoBehaviour
{
    Player player;
    [SerializeField]
    RectTransform healthBar;
    [SerializeField]
    RectTransform energyBar;
    Image losePanel;
    Image winPanel;
    bool activateWinPanel;
    bool activateLosePanel;
    float counter;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        losePanel = GameObject.Find("LosePanel").GetComponent<Image>();
        winPanel = GameObject.Find("WinPanel").GetComponent<Image>();
    }

	void Update ()
    {
        //healthBar.sizeDelta = new Vector2(player.Life * 2, healthBar.sizeDelta.y);
        counter += Time.deltaTime / 2;

        if (activateWinPanel)
        {
            winPanel.color = new Color(winPanel.color.r, winPanel.color.g, winPanel.color.b, counter);
        }
        if (activateLosePanel)
        {
            losePanel.color = new Color(losePanel.color.r, losePanel.color.g, losePanel.color.b, counter);
        }
    }

    public void WinPanel()
    {
        if(!activateWinPanel)
        {
            activateWinPanel = true;
            counter = 0;
        }
    }

    public void LosePanel()
    {
        if(!activateLosePanel)
        {
            counter = 0;
            activateLosePanel = true;
        }
    }

}
