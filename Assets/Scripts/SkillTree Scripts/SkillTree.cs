using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    //Static makes the variable a class variable. and syncs its data across every copy of this code.
    //If it changes here, it changes everywhere
    public static SkillTree skillTree;

    public GameObject heteroCanvas;

    //Int[] means a list of Integers
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

    //Awake is a unity function called when an object is initialised
    private void Awake()
    {
        skillTree = this;
    }

    //Start is a untiy function called at the start of runtime, right after Awake
    public void Start()
    {

        skillLevels = new int[2];
        skillCaps = new[] { 1, 1 };

        //These skills don't do anything, but all you have to do is connect the button up to any code and it will be run when brought
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

        //SetActive(false) makes an object invisible and uninteractable
        heteroCanvas.SetActive(false);
        gameObject.SetActive(false);
    }

    //Void is the function return type, void means no return
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

