using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SkillsDesc", menuName = "Skills Description list", order = 51)]
public class SkillsDescription : ScriptableObject
{
    public List<string> skillName, skillDesc;
}
