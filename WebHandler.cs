using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class WebHandler : MonoBehaviour
{
    public static WebHandler Instance;
    public delegate UnityWebRequest RequestCall();

    public string servAddress;
    public string registerEndpoint;
    public string loginEndpoint;

    public string adsEndpoint;
    public string chatEndpoint;
    public string catsEndpoint;

    public string addAdEndpoint;
    public string logoutEndpoint;

    public string buyPointsEndpoint;
    public string buyWaterEndpoint;
    public string userEndpoint;
    public string updateUserEndpoint;
    

    public string newsEndpoint;
    public string targetEndpoint;



    public List<string> img_cache_string;
    public List<Texture2D> img_cache_tex;


    public GameObject loadingScreen;
    public GameObject noInternet;

    private void Awake()
    {
        SingletonImplementation();
        loadingScreen.SetActive(false);
    }

    #region Requests
    private async void PostJson(string url, string dataString, UnityAction<string> DoIfSuccess = null, bool addTokenHeader = false)
    {
        var endUrl = servAddress + url;
        var req = await IRequestSend(() =>
        {
            var request = new UnityWebRequest(endUrl, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(dataString);
            var uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.uploadHandler = uploadHandler;
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            if (!addTokenHeader)
                request.SetRequestHeader("content-type", "application/json");
            else
            {
                request.SetRequestHeader("content-type", "application/json");
                request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
            }
            print(request.GetRequestHeader("content-type") + "   " + (addTokenHeader ? request.GetRequestHeader("token") : "") + "    " + request.url + "    " + Encoding.UTF8.GetString(request.uploadHandler.data));
            return request;
        });

        Debug.Log("All OK");
        Debug.Log("Status Code: " + req.responseCode);
        DoIfSuccess?.Invoke(req.downloadHandler.text);

    }


    private async Task PostMultipartAsync(string url, string textSctring, byte[] image = null, UnityAction<string> DoIfSuccess = null)
    {
        string endUrl = servAddress + url; /*"https://retailbonus.ru/13423.php";*/
        var req = await IRequestSend(() =>
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            if (image != null)
            {
                print("sending image");
                formData.Add(new MultipartFormFileSection("file", image, "name.png", "image/png"));
                formData.Add(new MultipartFormDataSection("text", textSctring));
            }
            else
            {
                print("sending text");
                formData.Add(new MultipartFormDataSection("text", textSctring));
            }

            UnityWebRequest request = UnityWebRequest.Post(endUrl, formData);
            request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
            return request;
        });

        Debug.Log("Request sent");
        Debug.Log("Status code: " + req.responseCode);
        DoIfSuccess?.Invoke(req.downloadHandler.text);

    }




    private async Task GetRequest(string url, UnityAction<string> DoIfSuccess = null)
    {
        var endUrl = servAddress + url;
        var req = await IRequestSend(() =>
        {
            var request = new UnityWebRequest(endUrl, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("token", AppManager.Instance.userInfo.access_token);
            return request;
        });

      //  Debug.Log("Request sent");
        Debug.Log("Status code: " + req.responseCode);
        DoIfSuccess?.Invoke(req.downloadHandler.text);
    }


    private async Task LoadImage(string url, UnityAction<Texture2D> DoIfSuccess)
    {


        if (!(url.Contains(".jpg") || url.Contains(".png")))
        {
            DoIfSuccess(Resources.Load<Texture2D>("logo"));

        }
        else
        {
            int i = 0;
            bool find = false;
            foreach (string s in img_cache_string)
            {
                if (s == url) { find = true;  break; }
                i++;
            }
            if (find)
            {
                DoIfSuccess(img_cache_tex[i]);
            }
            else
            {
                var req = await IRequestSend(() =>
                {
                    UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
                    return request;
                });

                Debug.Log("Loading image"+ url);
                var tex = DownloadHandlerTexture.GetContent(req);
                tex.Apply();

                img_cache_string.Add(url);
                img_cache_tex.Add(tex);

                DoIfSuccess(tex);
            }
        }
    }

    public async Task<UnityWebRequest> IRequestSend(RequestCall data)
    {
        UnityWebRequest request;//= data();

//        do
  //      {
            request = data();
         //   loadingScreen.SetActive(true);

            await request.SendWebRequest();
        //    loadingScreen.SetActive(false);

            Debug.Log(request.isNetworkError);
            
            /*
            if (request.responseCode==403)
            {
                AppManager.Instance.SwitchScreen(0);
                AppManager.Instance.bottomBar.SetActive(false);
                PlayerPrefs.DeleteAll();
                AppManager.Instance.userInfo = new UserInfo();
                SceneManager.LoadScene(0);
            }
 */
             if (request.error != null)
            {
                print(request.error);
                print(request.downloadHandler.text);
               // noInternet.SetActive(true);
        //        await new WaitWhile(() => { return noInternet.activeSelf == true; });
            }
    //    }
      //  while (request.error != null) ;

        Debug.Log(request.downloadHandler.text);
        if (request.error == null)
            return request;
        else return null;

    }
    #endregion


    public async void SendTargetWrapper(UnityAction<string> afterFininish)
    {
        await GetRequest(targetEndpoint, afterFininish);
    }

    public async Task GetTNewsWrapper(UnityAction<string> afterFininish)
    {
        await GetRequest(newsEndpoint, afterFininish);
    }

    public async void UpdateUserWrapper(UnityAction<string> afterFinish, string data)
    {
        PostJson(updateUserEndpoint, data, afterFinish, true);
    }
    
    public async Task LoadImageWrapper(UnityAction<Texture2D> afterFinish, string imgUrl)
    {
        await LoadImage(imgUrl, afterFinish);
    }
        
    public void RegisterWraper()
    {
        var userJSON = JsonUtility.ToJson(AppManager.Instance.userInfo.user);
        PostJson(registerEndpoint, userJSON);
    }

    public void LoginWraper(UnityAction<string> afterFinish)
    {
        var userJSON = JsonUtility.ToJson(AppManager.Instance.userInfo.user);
        PostJson(loginEndpoint, userJSON, afterFinish);
    }

    public void BuyPointsWraper(int pointsToBuy, UnityAction<string> afterFinish)
    {
        var points = new Points(pointsToBuy);
        PostJson(buyPointsEndpoint, JsonUtility.ToJson(points), afterFinish, true);
    }

    public void BuyWaterWraper(int waterToBuy, int vodomatID, UnityAction<string> afterFinish)
    {
        var water = new Water(waterToBuy, vodomatID);
        PostJson(buyWaterEndpoint, JsonUtility.ToJson(water), afterFinish, true);
    }

    public async Task ChatWraper(string param, UnityAction<string> afterfinish)
    {
        await GetRequest(chatEndpoint + param, afterfinish);
    }


    public async Task GetCatsWraper(UnityAction<string> afterfinish)
    {
        await GetRequest(catsEndpoint, afterfinish);
    }

    public async Task LogoutWrapper(UnityAction<string> afterFinish)
    {
        await GetRequest(logoutEndpoint, afterFinish);
    }


    public async Task GetAdsWraper(string param, UnityAction<string> afterfinish)
    {
        await GetRequest(adsEndpoint + param, afterfinish);
    }
       
    public async Task AddAdWrapper(UnityAction<string> afterFinish, string data)
    {
        PostJson(addAdEndpoint, data, afterFinish, true);
    }
    
    public async Task GetUserDataWraper(string param, UnityAction<string> afterFinish)
    {
        await GetRequest(userEndpoint + param, afterFinish);
    }
       
    private void SingletonImplementation()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        else if (Instance != this)
            Destroy(this);
    }
}
