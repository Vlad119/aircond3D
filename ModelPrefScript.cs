using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModelPrefScript : MonoBehaviour
{
    public TMP_Text name;
    public Image main_img;
    public GoodsScript.Models model;

    public void Create(GoodsScript.Models _model)
    {
        name.text = _model.name;
        main_img.sprite = _model.main_image;
        model = _model;
    }

    public void GetModel()
    {
        var AM = AppManager.Instance;
        AM.screens[10].GetComponent<InfoActivityScript>().Create(model);
        AM.SwitchScreen(10);
    }
}
