using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelList", menuName = "Level List", order = 52)]
public class LevelUnitsContainer : ScriptableObject
{
    public List<LevelUnit> levelUnits;
    public int unitGenerationSize;
}
