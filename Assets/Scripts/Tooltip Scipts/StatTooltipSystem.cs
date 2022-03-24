using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTooltipSystem : MonoBehaviour
{
    private static StatTooltipSystem current;

    public StatTooltip tooltip;

    //Awake is a function called by unity when an object is initialised
    public void Awake()
    {
        current = this;
    }

    //Static means that when this function is called, it is called on all versions of this code
    public static void Show(string content, string header = "")
    {
        current.tooltip.SetText(content, header);
        //SetActive(true) makes an object visible and interactable
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        //SetActive(false) makes an object invisible and uninteractable
        current.tooltip.gameObject.SetActive(false);
    }
}
