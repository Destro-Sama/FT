using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static LTDescr delay;
    public string header;

    //Multiline is a header that allows for a multiline string editor in the unity editor
    [Multiline()]
    public string content;

    //OnPointerExit is a unity function called when your pointer is no longer over this object
    public void OnPointerExit(PointerEventData eventData)
    {
        //LeanTween is a system that runs systems over time
        LeanTween.cancel(delay.uniqueId);
        StatTooltipSystem.Hide();
    }

    //OnPointerEnter is a unity function called when your pointer is over this object
    public void OnPointerEnter(PointerEventData eventData)
    {
        //delayedCall will call this function after set time
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            StatTooltipSystem.Show(content, header);
        }).setIgnoreTimeScale(true);
        //IgnoreTimeScale measn when timeScale = 0, this still updates
    }

    //OnMouseExit is a unity function called when your mouse is no longer over this object
    public void OnMouseExit()
    {
        LeanTween.cancel(delay.uniqueId);
        StatTooltipSystem.Hide();
    }

    //OnMouseEnter is a unity function called when your mouse is over this object
    public void OnMouseEnter()
    {
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            StatTooltipSystem.Show(content, header);
        }).setIgnoreTimeScale(true);
    }
}
