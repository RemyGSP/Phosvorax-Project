using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOptions : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resolutionText;
    [SerializeField] TextMeshProUGUI refreshRateText;
    private int currentResolutionOption;
    private int currentRefreshRateOption;
    private bool fullScreen;
    private bool windowed;
    private bool windowedBorderless;
    private String[] resolutionOptions;
    [SerializeField] private int[] refreshRate;

    // Start is called before the first frame update
    void Start()
    {
        resolutionOptions = new string[Screen.resolutions.Length];
        AddResolutionsToArray();
        currentResolutionOption = 0;
    }

    public void AddResolutionsToArray()
    {
        Resolution[] resolutions = Screen.resolutions;
        int counter = 0;
        foreach (Resolution resolution in resolutions)
        {
            string option = $"{resolution.width} x {resolution.height}";
            resolutionOptions[counter] = option;
            counter++;
        }
    }
    public void ChangeResolutionOption(int direction)
    {
        currentResolutionOption = ChangeOption(direction,currentResolutionOption);
        resolutionText.text = resolutionOptions[currentResolutionOption];
        SetResolution(currentResolutionOption);
    }

    public void ChangeRefreshRate(int direction)
    {
        currentRefreshRateOption = ChangeOption(direction, currentRefreshRateOption);
        refreshRateText.text = currentRefreshRateOption.ToString();
        SetRefreshRate(currentRefreshRateOption);
    }
    //Direction es -1 o 1 dependiendo si tira para alante en la array o al reves
    private int ChangeOption(int direction, int currentOption)
    {
        if (currentOption  + direction > 0 && currentResolutionOption + direction < resolutionOptions.Length)
        {
            currentOption += direction;

        }
        else if (currentResolutionOption + direction < 0)
        {
            currentOption = resolutionOptions.Length - 1;
        }
        else if (currentResolutionOption + direction > resolutionOptions.Length -1)
        {
            currentOption = 0;
        }
        return currentOption;

    }

    public void SetResolution(int index)
    {
        Resolution[] resolutions = Screen.resolutions;
        Resolution selectedResolution = resolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }

    public void SetRefreshRate(int index)
    {
        Application.targetFrameRate = refreshRate[index];
    }

    public void FullScreen()
    {
        fullScreen = true;
        windowed = false;
        windowedBorderless = false;
    }
    public void Windowed()
    {
        fullScreen = false;
        windowed = true;
        windowedBorderless = false;
    }
    public void WindowedBorderless()
    {
        fullScreen = false;
        windowed = false;
        windowedBorderless = true;
    }


}
