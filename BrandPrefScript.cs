using TMPro;
using UnityEngine;

public class BrandPrefScript : MonoBehaviour
{
    public TMP_Text name;
    public GoodsScript.CondBrand condBrand;
    public GoodsScript.TeploBrands teploBrand;

    public void Create(GoodsScript.CondBrand _brand)
    {
        name.text =_brand.cond_name;
        condBrand = _brand;
    }

    public void Create2(GoodsScript.TeploBrands _brand)
    {
        name.text = _brand.name;
        teploBrand = _brand;
    }


    public void GetBrand()
    {
        var AM = AppManager.Instance;
        if (AM._equipment == 1)
        {
            AM.screens[8].GetComponent<SeriesActivityScript>().Create(condBrand);
            AM.SwitchScreen(8);
        }
        else
        {
            AM.screens[8].GetComponent<SeriesActivityScript>().Create2(teploBrand);
            AM.SwitchScreen(8);
        }
    }
}
