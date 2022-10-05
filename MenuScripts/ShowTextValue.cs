using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script displays slider's value as a text value

public class ShowTextValue : MonoBehaviour
{
    public TextMeshProUGUI xText;

    public void textUpdate(float value) {
        xText.text = "" + value;
    }
}
