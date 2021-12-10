using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText = default;

    public static UI instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetSubtitle(string subtitle)
    {
        subtitleText.text = subtitle;
    }
}
