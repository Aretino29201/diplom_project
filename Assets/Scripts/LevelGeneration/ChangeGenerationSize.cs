using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeGenerationSize : MonoBehaviour
{
    public LevelUnitsContainer levelUnits;

    public TMP_InputField textBox;

    private void Start()
    {
        textBox.text = levelUnits.unitGenerationSize.ToString();
    }
    public void ChangeGenSize(TMP_InputField textInput)
    {
        levelUnits.unitGenerationSize = Convert.ToInt32(textInput.text);
    }
}
