using UnityEngine;

public class SeriesActivityScript : MonoBehaviour
{
    public GameObject SeriesPref;
    public GameObject parent;
    public GameObject spacePref;

    public async void Create(GoodsScript.CondBrand _condBrand)
    {
        await new WaitUntil(() => AppManager.Instance);
        var AM = AppManager.Instance;
        parent.transform.ClearChildren();
        foreach (var series in _condBrand.series)
        {
            var name = Instantiate(SeriesPref, parent.transform);
            name.GetComponent<SeriesPrefScript>().Create(series);
        }
        Instantiate(spacePref, parent.transform);
    }

    public async void Create2(GoodsScript.TeploBrands _teploBrand)
    {
        await new WaitUntil(() => AppManager.Instance);
        var AM = AppManager.Instance;
        parent.transform.ClearChildren();
        foreach (var series in _teploBrand.series)
        {
            var name = Instantiate(SeriesPref, parent.transform);
            name.GetComponent<SeriesPrefScript>().Create(series);
        }
        Instantiate(spacePref, parent.transform);
    }
}
