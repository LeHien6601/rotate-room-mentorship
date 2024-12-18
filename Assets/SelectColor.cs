using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectColor : MonoBehaviour
{
    [SerializeField] private Slider redSlide;
    [SerializeField] private Slider greenSlide;
    [SerializeField] private Slider blueSlide;
    [SerializeField] private Slider alphaSlide;
    [SerializeField] private Image image;

    private void Start()
    {
        redSlide.value = image.color.r;
        greenSlide.value = image.color.g;
        blueSlide.value = image.color.b;
        alphaSlide.value = image.color.a;
    }

    private void Update()
    {
        image.color = new Color(redSlide.value, greenSlide.value, blueSlide.value, alphaSlide.value);
    }
    public void SaveColor()
    {
        Color color = image.color;
        string colorHex = ColorUtility.ToHtmlStringRGB(color);
        Debug.Log(colorHex);
        PlayerPrefs.SetString("Color", colorHex);
        PlayerPrefs.SetFloat("Alpha", color.a);
        PlayerPrefs.Save();
    }
    public void BackToMainMenu()
    {
        GameController.instance.LoadLevel(0);
    }
}
