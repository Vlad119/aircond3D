using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ProfileScreen : MonoBehaviour
{
    public Slider stars;
    public TMP_Text userName;
    public TMP_Text fromDate;
    public TMP_Text phoneNumber;
    public UserInfo userInfo;
    public RawImage avatar;
    public TMP_Text order_fail;
    public TMP_Text order_success;
    public GameObject userReviewBlock;
    public Slider userReviewStars;
    public TMP_InputField userReviewText;

    [SerializeField] private string avatar_url;

    public async void OnEnable()
    {
        await InitializeScreen();
        userReviewText.text = string.Empty;
    }

    public async void UserSendReview()
    {
        AppManager.Instance.ShowMessage("Отзыв отправлен на модерацию!");
        await WebHandler.Instance.ChatWraper(
        "?uid=" + AppManager.Instance.curent_userprofile_id + "&text=" + userReviewText.text + "&rating=" + userReviewStars.value + "&action=addreview",
        (repl) =>
        {
            // UpdateChat(repl);
        });
    }


    private async Task InitializeScreen()
    {

        while (WebHandler.Instance == null || AppManager.Instance == null || AppManager.Instance.userInfo.user.phone == "")
        await new WaitForSeconds(.01f);
        AppManager.Instance.LoadingScreen.SetActive(true);
        await WebHandler.Instance.GetUserDataWraper("?uid=" + AppManager.Instance.curent_userprofile_id,
            (resp) =>
         {
             //                Debug.Log(resp);
             JsonUtility.FromJsonOverwrite(resp, userInfo);
             //userName.text = userInfo.user.name + " " + userInfo.user.surname;
             //phoneNumber.text = userInfo.user.phone;
             stars.value = userInfo.user.rating;
             order_fail.text = "ПРОВАЛЬНО: " + userInfo.user.order_fail;
             order_success.text = "УСПЕШНО: " + userInfo.user.order_success;
             if (userInfo.user.uid == AppManager.Instance.userInfo.user.uid) userReviewBlock.SetActive(false);
             else userReviewBlock.SetActive(true);
             userReviewStars.value = userInfo.user.your_review_rating;
             userReviewText.text = userInfo.user.your_review_text;
             avatar_url = userInfo.user.avatar_image;
             if (avatar_url.Contains("."))
                 WebHandler.Instance.LoadImageWrapper((tex) =>
                 {
                     avatar.texture = tex;
                 }, userInfo.user.avatar_image);
             else
                 avatar.texture = Resources.Load<Texture>("avatar 1") as Texture;
         });
        AppManager.Instance.LoadingScreen.SetActive(false);
    }
}
