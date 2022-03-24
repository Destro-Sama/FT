using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class StatTooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    //Awake is a function called when an object is initialised
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    //Void is the function return type, void means no return
    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
            //SetActive(false) makes the object invisible and uninteractable
            headerField.gameObject.SetActive(false);
        else
        {
            //SetActive(true) makes the object visible and interactable
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        //z ? x : y is ternary operator. if z is true, then x, else y
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }
}
