using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ReviewsScreen : MonoBehaviour
{

    [SerializeField] private LoopScrollRect loopScrollRect;
    //    public TMP_InputField text;
    public async void OnEnable()
    {
        await InitializeScreen();
    }

    private async Task InitializeScreen()
    {
        while (WebHandler.Instance == null || AppManager.Instance == null || AppManager.Instance.userInfo.user.phone == "")
            await new WaitForSeconds(.01f);
        await WebHandler.Instance.GetUserDataWraper("?uid=" + AppManager.Instance.curent_userprofile_id
            , (resp) =>
            {
                //                Debug.Log(resp);
                JsonUtility.FromJsonOverwrite(resp, AppManager.Instance.curentUserInfo);
                loopScrollRect.totalCount = AppManager.Instance.curentUserInfo.user.reviews.Count;
                loopScrollRect.RefillCells();

            });
    }
}
