using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public PlayerStats playerStats;
    private TMP_Text text;

    private void Start()
    {
        Image image = transform.parent.gameObject.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        text = gameObject.GetComponent<TMP_Text>();
    }

    public void UpdateCoins()
    {
        text.text = $"Coins: {playerStats.coins}";
    }
}
