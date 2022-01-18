using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillPoints : MonoBehaviour
{
    public TMP_Text text;

    public SkillTree SkillTree;

    public int points;

    private void Start()
    {
        points = 100;
    }

    private void UpdateUI()
    {
        text.text = $"Points Left: {points}";
    }

    public void UpdatePoints()
    {
        UpdateUI();
        SkillTree.skillPoint = points;
    }

    public void ChangePoints(int change)
    {
        points -= change;
    }
}
