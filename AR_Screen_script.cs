using EasyMobile;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class AR_Screen_script : MonoBehaviour
{
    public bool showed = false;
    public GameObject slider;
    public GameObject light;
    public GameObject photo;
    public GameObject bottom_bar;
    private Action<string, MediaResult> TakePictureCallback;

    public void ShowHideSlider()
    {
        if (showed)
        {
            slider.SetActive(false);
            showed = false;
        }
        else
        {
            slider.SetActive(true);
            showed = true;
        }
    }

    public async void Photo()
    {
        HideUI();
        await new WaitForEndOfFrame();
        await SaveScreenshotAsync();
        ShowUI();
    }

    
    async Task SaveScreenshotAsync()
    {
        await new WaitForEndOfFrame();
        Texture2D texture =  Sharing.CaptureScreenshot(0,0,Screen.width,Screen.height);
        texture.Apply();
        await new WaitUntil(() =>
        {
            return (texture!=null);
        });
        Media.Gallery.SaveImage(texture, DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss"), ImageFormat.JPG, SaveImageCallback);
    }

    public void HideUI()
    {
        slider.SetActive(false);
        light.SetActive(false);
        photo.SetActive(false);
        bottom_bar.SetActive(false);
    }

    public void ShowUI()
    {
        slider.SetActive(true);
        light.SetActive(true);
        photo.SetActive(true);
        bottom_bar.SetActive(true);
    }


    private void SaveImageCallback(string error)
    {
        if (!string.IsNullOrEmpty(error))
        {
            // There was an error, show it to users. 
        }
        else
        {
            // The image's saved successfully. 
        }
    }
}
