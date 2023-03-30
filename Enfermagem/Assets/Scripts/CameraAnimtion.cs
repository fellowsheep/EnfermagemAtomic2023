using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraAnimtion : MonoBehaviour
{
    [SerializeField] private TextMeshPro texto = default;

    public void TextCameraEvent(string legenda)
    {
        texto.text = legenda;
    }
}
