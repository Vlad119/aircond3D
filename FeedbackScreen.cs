using TMPro;
using UnityEngine;

public class FeedbackScreen : MonoBehaviour
{
    public TMP_InputField subject;
    public TMP_InputField text;

    public void SendFeedback()
    {

        AppManager.Instance.ShowMessage("Сообщение отправлено!");
        WebHandler.Instance.ChatWraper("?uid=" +
            AppManager.Instance.userInfo.user.uid + 
            "&text=" + text.text + "&subject=FEEDBACK&action=feedback",
       (repl) => { });
    }
}
