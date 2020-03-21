using UnityEngine;

public class NewsActivityScript : MonoBehaviour
{
    public GameObject newsPref;
    public GameObject parent;
    public GameObject space;


    public async void GetNews()
    {
        parent.transform.ClearChildren();
        var AM = AppManager.Instance;
        var WH = WebHandler.Instance;
        await WH.GetTNewsWrapper((resp) =>
         {
             AM.res = JsonUtility.FromJson<Res>(resp);
         });
        foreach (var news in AM.res.res.news)
        {
            var pref = Instantiate(newsPref,parent.transform);
            pref.GetComponent<NewsPrefScript>().Create(news);
        }
    }
}
