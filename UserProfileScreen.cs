using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UserProfileScreen : MonoBehaviour
{
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField phone_number;
    [SerializeField] private TMP_InputField region;
    [SerializeField] private TMP_InputField city;
    [SerializeField] private TMP_InputField organization;
    [SerializeField] private TMP_InputField function;
    public GameObject scroll;

    public async void Logout()
    {
        var AM = AppManager.Instance;
        await WebHandler.Instance.LogoutWrapper((resp) =>
        {
            AM.SwitchScreen(0);
            AM.bottomBar.SetActive(false);
            PlayerPrefs.DeleteAll();
            AM.userInfo = new UserInfo();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    public async void OnEnable()
    {
        await InitializeScreen();
        //var userInfo = AppManager.Instance.userInfo.user;
        string s = PlayerPrefs.GetString("user");
        //Debug.Log("s= " + s);
        var userInfo = JsonUtility.FromJson<UserInfo>(s);
        email.text = userInfo.user.phone;
        phone_number.text = userInfo.user.phone_number;
        region.text = userInfo.user.region;
        city.text = userInfo.user.city;
        organization.text = userInfo.user.company_name;
        function.text = userInfo.user.function;
        scroll.SetActive(false);
        scroll.SetActive(true);
    }

    public void SaveUserToPlayerPrefs()
    {
        CheckAllFields();
        var AM = AppManager.Instance;
        var user = AM.userInfo.user;
        string s;
        if (email.text == user.phone)
        {
            print("Updating user");
            string fid = "";
            WebHandler.Instance.UpdateUserWrapper
            ((resp) =>
            {
                JsonUtility.FromJsonOverwrite(resp, AM.userInfo);
            },
            s = JsonUtility.ToJson(AM.userInfo));
            PlayerPrefs.SetString("user", s);
            PlayerPrefs.Save();
            AM.SwitchScreen(6);
        }
        else
        {
            AM.SwitchScreen(12);
        }
    }

    private async Task InitializeScreen()
    {
        var AM = AppManager.Instance;
        while (WebHandler.Instance == null || AM == null || AM.userInfo.user.phone == "")
            await new WaitForSeconds(.01f);
        AM.LoadingScreen.SetActive(true);
        AM.LoadingScreen.SetActive(false);
    }


    public void SaveChanges()
    {
        CheckAllFields();
        var user = AppManager.Instance.userInfo.user;
        var userInfo = JsonUtility.ToJson(user);
        PlayerPrefs.SetString("user", userInfo);
        //user update
    }

    public void CheckAllFields()
    {
        var user = AppManager.Instance.userInfo.user;
        if (!string.IsNullOrEmpty(email.text))
        {
            user.phone = email.text;
        }
        if (!string.IsNullOrEmpty(phone_number.text))
        {
            user.phone_number = phone_number.text;
        }
        if (!string.IsNullOrEmpty(region.text))
        {
            user.region = region.text;
        }
        if (!string.IsNullOrEmpty(city.text))
        {
            user.city = city.text;
        }
        if (!string.IsNullOrEmpty(organization.text))
        {
            user.company_name = organization.text;
        }
        if (!string.IsNullOrEmpty(function.text))
        {
            user.function = function.text;
        }
    }

    public void LogOut()
    {
        PlayerPrefs.DeleteAll();
        AppManager.Instance.SwitchScreen(0);
    }

    public void SendTarger()
    {
        WebHandler.Instance.SendTargetWrapper((resp) => { });
    }
}
