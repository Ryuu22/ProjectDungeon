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

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void Update ()
    {
        healthBar.sizeDelta = new Vector2(player.Life * 2, 10);
        energyBar.sizeDelta = new Vector2(100 - player.DashCooldownCounter * 20, 6);
    }
}
