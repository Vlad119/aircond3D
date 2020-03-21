using UnityEngine;

public class BrandsActivityScript : MonoBehaviour
{
    public GameObject BrandPref;
    public GameObject parent;
    public GameObject spacePref;


    private async void OnEnable()
    {
        await new WaitUntil(() => AppManager.Instance);
        var AM = AppManager.Instance;
        parent.transform.ClearChildren();
        if (AM._equipment == 1)
        {
            ForeachCondBrand();
        }
        else
        {
            ForeachTeploBrand();
        }
        Instantiate(spacePref, parent.transform);
    }

       
    public void ForeachCondBrand()
    {
        var AM = AppManager.Instance;
        foreach (var brand in AM.equipment.condBrand)
        {
            var name = Instantiate(BrandPref, parent.transform);
            name.GetComponent<BrandPrefScript>().Create(brand);
        }
    }


    public void ForeachTeploBrand()
    {
        var AM = AppManager.Instance;
        foreach (var brand in AM.equipment.teploBrand)
        {
            var name = Instantiate(BrandPref, parent.transform);
            name.GetComponent<BrandPrefScript>().Create2(brand);
        }
    }
}

