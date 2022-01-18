using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SkillTree;

public class Skill : MonoBehaviour
{
    public int id;

    public TMP_Text titleText;
    public TMP_Text descText;

    public int[] connectedSkills;

    public int levelPass = 0;

    private static bool level5 = false;

    public void UpdateUI()
    {
        titleText.text = $"{skillTree.skillLevels[id]}/{skillTree.skillCaps[id]}\n{skillTree.skillNames[id]}";
        descText.text = $"{skillTree.skillDesc[id]}";

        GetComponent<Image>().color = skillTree.skillLevels[id] >= skillTree.skillCaps[id] ? new Color(0.831f, 0.686f, 0.216f) : skillTree.skillPoint >= 1 ? new Color(0.125f, 0.8f, 0.509f) : skillTree.skillLevels[id] >= 1 ? new Color(0f, 0f, 0.254f) : Color.grey;
        if (level5 && skillTree.skillLevels[id] == 0 && skillTree.skillCaps[id] == 5)
            GetComponent<Image>().color = Color.grey;

        foreach (var skill in connectedSkills)
        {
            skillTree.skillList[skill].gameObject.SetActive(skillTree.skillLevels[id] >= levelPass);
            skillTree.connecList[skill].SetActive(skillTree.skillLevels[id] >= levelPass);
        }
    }

    public void Buy()
    {
        if (skillTree.skillPoint < 1 || skillTree.skillLevels[id] >= skillTree.skillCaps[id])
            return;

        if (skillTree.skillCaps[id] == 5 && level5 && skillTree.skillLevels[id] == 0)
            return;

        if (skillTree.skillCaps[id] == 5)
            level5 = true;

        skillTree.skillPoint -= 1;
        skillTree.skillLevels[id]++;
        skillTree.ChangePoints();

        skillTree.UpdateAllSkillUI();
    }
}
