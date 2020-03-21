using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPrefab : MonoBehaviour
{
    public RawImage image;
    public TMP_Text date;
    public TMP_Text text;
    public Slider slider;
    public Button button;
    private bool eventAdd;








    public void ShowProfile()
    {



        //        AppManager.Instance.curentAd_id = id;
        //      AppManager.Instance.SwitchScreen(6);

        // AppManager.Instance.pickAndSearchBar.SetPlacesWrapper();
        //        Debug.Log(id);

    }

    private void ScrollCellIndex(int index)
    {
//        if (index < AppManager.Instance.cure.chat.Length)
  //      {

            //if (AppManager.Instance.chat.chat[index].isowner == "true")
            //{
            //     date.text = "Вы ("+AppManager.Instance.chat.chat[index].date + ")";
            //       text.text = "<b>"+AppManager.Instance.chat.chat[index].text+"</b>";
            //     }
            //       else
            //           {
            //date.text = AppManager.Instance.chat.chat[index].name + " " + AppManager.Instance.chat.chat[index].surname + ":" + AppManager.Instance.chat.chat[index].date + "";
            text.text = AppManager.Instance.curentUserInfo.user.reviews[index].text;
        date.text = AppManager.Instance.curentUserInfo.user.reviews[index].date;
        slider.value = AppManager.Instance.curentUserInfo.user.reviews[index].rating;



    }
    //}






}



