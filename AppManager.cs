using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using static GoodsScript;

public class AppManager : MonoBehaviour
{
    public Equipment equipment = new Equipment();
    [SerializeField] public Res res = new Res();
    public GameObject backgrond;
    [SerializeField] int codeLength = 4;
    [HideInInspector] public static AppManager Instance;
    public GameObject[] screens;
    public GameObject messages;
    public GameObject ar;
    public GameObject ui;
    public Light light;
    public Slider slider;
    public int index = 0;
    public GameObject fon_message;
    public TMP_Text fon_message_text;
    public GameObject LoadingScreen;
    public int activeScreenIndex;
    public List<int> prevScreenIndex;
    public string my_push_token;
    public string curent_userprofile_id;
    public UserInfo curentUserInfo;
    public UserInfo userInfo = new UserInfo();
    public int _equipment;
    public GameObject backButton;


    //debug
    public string phoneNumber = "";
    private WaitForSeconds transferAnimationLength = new WaitForSeconds(.6f);
    public GameObject bottomBar;
    public int brand_index;

    public void Light()
    {
        light.intensity = slider.value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }


    public void ShowMessage(string message)
    {
        fon_message_text.text = message;
        fon_message.SetActive(true);
    }


    public void GoAR()
    {
        backgrond.SetActive(false);
        SwitchScreen(7);
    }

    public void Messages()
    {
        backgrond.SetActive(true);
        SwitchScreen(5);
    }

    public void CloseMessage()
    {
        fon_message.SetActive(false);
    }


    private async void Start()
    {
        await InitializeScreen();
        index = 1;
        bottomBar.SetActive(false);
        Permission.RequestUserPermission(Permission.Camera);
        prevScreenIndex.Clear();
        prevScreenIndex.Add(activeScreenIndex);
    }


    private async Task InitializeScreen()
    {
        if (WebHandler.Instance == null || Instance == null || Instance.userInfo.access_token == "")
            await new WaitForSeconds(.01f);
    }


    private void Awake()
    {
        index = 1;
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        if (!PlayerPrefs.HasKey("user"))
        {
            bottomBar.SetActive(false);
            SwitchScreen(0);
        }
        else
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("user"), userInfo);
            bottomBar.SetActive(true);
            SwitchScreen(6);
        }
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;
                Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
                Firebase.Messaging.FirebaseMessaging.Subscribe("/topics/active_users");
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        my_push_token = token.Token;
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }
    public virtual void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message");
        var notification = e.Message.Notification;
        if (notification != null)
        {
            Debug.Log("title: " + notification.Title);
            Debug.Log("body: " + notification.Body);
        }
        if (e.Message.From.Length > 0)
            Debug.Log("from: " + e.Message.From);
        if (e.Message.Link != null)
        {
            Debug.Log("link: " + e.Message.Link.ToString());
        }
        if (e.Message.Data.Count > 0)
        {
            Debug.Log("data:");
            foreach (System.Collections.Generic.KeyValuePair<string, string> iter in
                e.Message.Data)
            {
                Debug.Log("  " + iter.Key + ": " + iter.Value);
            }
        }
    }

    public void CheckCode(TMP_InputField field)
    {
        if (field.text.Length == codeLength)
        {
            userInfo.user.code = int.Parse(field.text);
            WebHandler.Instance.LoginWraper((repl) =>
            {
                userInfo = JsonUtility.FromJson<UserInfo>(repl);
                SaveUserInfo(userInfo);
                userInfo.user.push_token = my_push_token;
                WebHandler.Instance.UpdateUserWrapper((resp) =>
                { }, JsonUtility.ToJson(Instance.userInfo)
                );

                SwitchScreen(6);
            });
        }
    }

    public void CheckCodeForNewMail(TMP_InputField field)
    {
        if (field.text.Length == codeLength)
        {
            userInfo.user.code = int.Parse(field.text);
            WebHandler.Instance.LoginWraper((repl) =>
            {
                userInfo = JsonUtility.FromJson<UserInfo>(repl);
                SaveUserInfo(userInfo);
                userInfo.user.push_token = my_push_token;
                WebHandler.Instance.UpdateUserWrapper((resp) =>
                { }, JsonUtility.ToJson(Instance.userInfo)
                );
                SwitchScreen(6);
            });
        }
    }


    public void SwitchScreen(int nextScreenIndex)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (i != nextScreenIndex)
            {
                screens[i].GetComponent<Canvas>().enabled = false;
                screens[i].SetActive(false);
            }
        }

        if (nextScreenIndex == -1)
        {
            activeScreenIndex = prevScreenIndex[prevScreenIndex.Count - 1];
            prevScreenIndex.RemoveAt(prevScreenIndex.Count - 1);
            screens[activeScreenIndex].GetComponent<Canvas>().enabled = true;
            screens[activeScreenIndex].SetActive(true);
            activeScreenIndex = prevScreenIndex[prevScreenIndex.Count - 1];
            if (prevScreenIndex.Count == 1)
            {
                prevScreenIndex.Add(activeScreenIndex);
            }
        }

        if (activeScreenIndex != nextScreenIndex || activeScreenIndex == 0)
        {
            if (nextScreenIndex != -1)
            {
                screens[nextScreenIndex].SetActive(true);
                screens[nextScreenIndex].GetComponent<Canvas>().enabled = true;
                prevScreenIndex.Add(activeScreenIndex);
                activeScreenIndex = nextScreenIndex;
            }
        }

        if (activeScreenIndex!=0)
        {
            if (activeScreenIndex == 2 || activeScreenIndex > 4)
            {
                bottomBar.SetActive(true);
            }
            else
            {
                bottomBar.SetActive(false);
            }
        }

        if (activeScreenIndex == 7)
        {
            backgrond.SetActive(false);
        }
    }


    public void SaveUserInfo(UserInfo userInfo)
    {
        var toSave = JsonUtility.ToJson(userInfo);
        PlayerPrefs.SetString("user", toSave);
    }

    public void LoadUser()
    {
        var toUnravel = PlayerPrefs.GetString("user");
        userInfo = JsonUtility.FromJson<UserInfo>(toUnravel);
    }



    public void BackButton()
    {
        SwitchScreen(-1);
    }

    public void Catalog()
    {
        backgrond.SetActive(true);
        SwitchScreen(6);
    }

    private void OnEnable()
    {
        index = 1;
        
    }

    public void Profile()
    {
        SwitchScreen(3);
        backgrond.SetActive(true);
    }

    public void History()
    {
        backgrond.SetActive(true);
        screens[11].GetComponent<NewsActivityScript>().GetNews();
        SwitchScreen(11);
    }
}