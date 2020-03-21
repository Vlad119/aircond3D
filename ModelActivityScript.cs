using UnityEngine;

public class ModelActivityScript : MonoBehaviour
{
    public GameObject ModelPref;
    public GameObject parent;
    public GameObject spacePref;

    public async void Create(GoodsScript.Series _series)
    {
        await new WaitUntil(() => AppManager.Instance);
        var AM = AppManager.Instance;
        var trans = parent.GetComponent<RectTransform>().transform;
        trans.position = new Vector3(trans.position.x, 0, trans.position.z);
        parent.transform.ClearChildren();
        foreach (var model in _series.model)
        {
            var name = Instantiate(ModelPref, parent.transform);
            name.GetComponent<ModelPrefScript>().Create(model);
        }
        Instantiate(spacePref, parent.transform);
    }
}
