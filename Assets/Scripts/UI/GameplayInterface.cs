using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayInterface : MonoBehaviour
{
    Player player;
    BossSlime boss;
    Vector2 bossPos;
    [SerializeField]
    RectTransform healthBar;
    [SerializeField]
    RectTransform energyBar;
    [SerializeField]
    RectTransform bossHealthBar;
    [SerializeField]
    GameObject bossBar;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //boss = GameObject.FindGameObjectWithTag("BossSlime").GetComponent<BossSlime>();
    }

	void Update ()
    {
        healthBar.sizeDelta = new Vector2(player.Life * 2, healthBar.sizeDelta.y);

        if(boss != null)
        {
            bossPos = boss.gameObject.transform.position;
        }

        if (Camera.main.WorldToViewportPoint(bossPos).x > 0 && Camera.main.WorldToViewportPoint(bossPos).x < 1 && Camera.main.WorldToViewportPoint(bossPos).y > 0 && Camera.main.WorldToViewportPoint(bossPos).y < 1)
        {
            bossBar.SetActive(true);
        }
        else bossBar.SetActive(false);

        if(boss == null) bossBar.SetActive(false);

        //bossHealthBar.localPosition = new Vector3(200 - boss.Life, bossHealthBar.localPosition.y, 0);
    }
}
