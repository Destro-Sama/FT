using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public PlayerStats playerStats;
    private TMP_Text text;

    //Start is a unity function that gets called at the start of runtime
    private void Start()
    {
        Image image = transform.parent.gameObject.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        text = gameObject.GetComponent<TMP_Text>();
    }

    //Void is the function return type, void means no return
    public void UpdateCoins()
    {
        //$"{}" is text formatting in c#
        text.text = $"Coins: {playerStats.coins}";
    }
}
