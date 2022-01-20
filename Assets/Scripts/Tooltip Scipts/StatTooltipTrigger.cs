using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static LTDescr delay;
    public string header;

    [Multiline()]
    public string content;

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        StatTooltipSystem.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            StatTooltipSystem.Show(content, header);
        }).setIgnoreTimeScale(true);
    }

    public void OnMouseExit()
    {
        LeanTween.cancel(delay.uniqueId);
        StatTooltipSystem.Hide();
    }

    public void OnMouseEnter()
    {
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            StatTooltipSystem.Show(content, header);
        }).setIgnoreTimeScale(true);
    }
}
