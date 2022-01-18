using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;

    public GameObject heteroCanvas;

    public int[] skillLevels;
    public int[] skillCaps;
    public string[] skillNames;
    public string[] skillDesc;

    public List<Skill> skillList;
    public GameObject skillHolder;

    public List<GameObject> connecList;
    public GameObject connecHolder;

    public SkillPoints points;
    public int skillPoint = 100;

    private void Awake()
    {
        skillTree = this;
    }

    public void Start()
    {

        skillLevels = new int[2];
        skillCaps = new[] { 1, 1 };

        skillNames = new[] { "Dash", "Higher Jump"};
        skillDesc = new[]
        {
            "You Can Dash!!",
            "You Jump Higher!!"
        };

        foreach (var skill in skillHolder.GetComponentsInChildren<Skill>())
        {
            skillList.Add(skill);
        }

        foreach (var connector in connecHolder.GetComponentsInChildren<RectTransform>())
        {
            connecList.Add(connector.gameObject);
        }

        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i].id = i;
        }

        skillList[0].connectedSkills = new[] { 1 };

        skillList[0].levelPass = 1;



        UpdateAllSkillUI();


        heteroCanvas.SetActive(false);
        gameObject.SetActive(false);
    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in skillList)
        {
            skill.UpdateUI();
        }
        UpdatePoint();
    }

    public void ChangePoints()
    {
        points.ChangePoints(1);
    }

    public void UpdatePoint()
    {
        points.UpdatePoints();
    }
}

