using TMPro;
using UnityEngine;

public class SeriesPrefScript : MonoBehaviour
{
    public TMP_Text name;
    public GoodsScript.Series series;


    public void Create(GoodsScript.Series _series)
    {
        name.text = _series.series_name;
        series = _series;
    }

    public void GetSeries()
    {
        var AM = AppManager.Instance;
        {
            AM.screens[9].GetComponent<ModelActivityScript>().Create(series);
            AM.SwitchScreen(9);
        }
    }
}
