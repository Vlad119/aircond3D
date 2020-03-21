using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneNumberInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField FIO;
    [SerializeField] private TMP_InputField phone_number;
    [SerializeField] private TMP_InputField region;
    [SerializeField] private TMP_InputField city;
    [SerializeField] private TMP_InputField organization;
    [SerializeField] private TMP_InputField function;
    [SerializeField] private Toggle email_notification;

    public GameObject error;

    public void GetSMS()
    {
        CheckVoid();
    }


    public void ApplyAllFields()
    {
        var user = AppManager.Instance.userInfo.user;
        user.phone = email.text;
        user.name = FIO.text;
        user.phone_number = phone_number.text;
        user.region = region.text;
        user.city = city.text;
        user.company_name = organization.text;
        user.function = function.text;
    }


    public void CheckVoid()
    {
        if (email.text.Contains("@") && email.text.Contains(".") && 
            !string.IsNullOrEmpty(email.text) && !string.IsNullOrEmpty(FIO.text) &&
            !string.IsNullOrEmpty(phone_number.text) && !string.IsNullOrEmpty(region.text) &&
            !string.IsNullOrEmpty(city.text) && !string.IsNullOrEmpty(organization.text) &&
            !string.IsNullOrEmpty(function.text) && email_notification.isOn)
        {
            ApplyAllFields();
            Request();
        }
        else
        {
            error.SetActive(true);
        }
    }


    public void Request()
    {
        var user = AppManager.Instance.userInfo.user;
        var userInfo = JsonUtility.ToJson(user);
        PlayerPrefs.SetString("user", userInfo);
        WebHandler.Instance.RegisterWraper();
        AppManager.Instance.screens[1].SetActive(true);
        AppManager.Instance.screens[1].GetComponent<Canvas>().enabled = true;
    }


    public void ChangeEmailNotificationStatus()
    {
        var user = AppManager.Instance.userInfo.user;
        if (email_notification.isOn)
        {
            user.email_notification = 1;
        }
        else
        {
            user.email_notification = 0;
        }
    }
    private async void OnEnable()
    {
        await InitializeScreen();
        AppManager.Instance.bottomBar.SetActive(false);
    }

    private async Task InitializeScreen()
    {
        if (WebHandler.Instance == null || AppManager.Instance == null)
            await new WaitForSeconds(.01f);
    }
}


