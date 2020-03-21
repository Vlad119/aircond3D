using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GoodsScript;

public class InfoActivityScript : MonoBehaviour
{
    public Models model;
    public Image img1;
    public Image img2;
    public Image img3;
    public Image img4;
    public Image img5;
    public Image img6;
    public Image img7;
    public Image img8;
    public TMP_Text name;
    public int index;
    //inside
    public TMP_Text сooling_capacity;
    public TMP_Text heat_output;
    public TMP_Text power_consumption_cooling;
    public TMP_Text power_consumption_heating;
    public TMP_Text energy_EER_COP;
    public TMP_Text seasonal_energy_efficiency_SEER_SCOP;
    public TMP_Text energy_efficiency_class_SEER_SCOP;
    public TMP_Text sound_pressure_cooling;
    public TMP_Text sound_pressure_heating;
    public TMP_Text air_consumption_cooling;
    public TMP_Text air_consumption_heating;
    public TMP_Text dimensions;
    public TMP_Text weight;
    public TMP_Text drain_diameter;
    //outside
    public TMP_Text sound_pressure_cooling2;
    public TMP_Text sound_pressure_heating2;
    public TMP_Text sound_power;
    public TMP_Text dimensions2;
    public TMP_Text weight2;
    public TMP_Text power_supply;
    public TMP_Text pipe_diameter_liquid_gas;
    public TMP_Text minimum_pipe_length;
    public TMP_Text max_pipe_length_height_difference;
    public TMP_Text temperature_range_outdoor_air_cooling;
    public TMP_Text temperature_range_outdoor_air_heating;
    public TMP_Text refrigerant;

    public void Create(Models _model)
    {
        model = _model;
        try
        {
            img1.sprite = _model.img[0];
            img2.sprite = _model.img[1];
            img3.sprite = _model.img[2];
            img4.sprite = _model.img[3];
            img5.sprite = _model.img[4];
            img6.sprite = _model.img[5];
            img7.sprite = _model.img[6];
            img8.sprite = _model.img[7];
        }
        catch
        { }
        
        name.text = _model.name;
        index = _model.index;
        //inside
        сooling_capacity.text = _model.insideBlock.сooling_capacity;
        heat_output.text = _model.insideBlock.heat_output;
        power_consumption_cooling.text = _model.insideBlock.power_consumption_cooling;
        power_consumption_heating.text = _model.insideBlock.power_consumption_heating;
        energy_EER_COP.text = _model.insideBlock.energy_EER_COP;
        seasonal_energy_efficiency_SEER_SCOP.text = _model.insideBlock.seasonal_energy_efficiency_SEER_SCOP;
        energy_efficiency_class_SEER_SCOP.text = _model.insideBlock.energy_efficiency_class_SEER_SCOP;
        sound_pressure_cooling.text = _model.insideBlock.sound_pressure_cooling;
        sound_pressure_heating.text = _model.insideBlock.sound_pressure_heating;
        air_consumption_cooling.text = _model.insideBlock.air_consumption_cooling;
        air_consumption_heating.text = _model.insideBlock.air_consumption_heating;
        dimensions.text = _model.insideBlock.dimensions;
        weight.text = _model.insideBlock.weight;
        drain_diameter.text = _model.insideBlock.drain_diameter;
        //outside
        sound_pressure_cooling2.text = _model.outsideBlock.sound_pressure_cooling;
        sound_pressure_heating2.text = _model.outsideBlock.sound_pressure_heating;
        sound_power.text = _model.outsideBlock.sound_power;
        dimensions2.text = _model.outsideBlock.dimensions;
        weight2.text = _model.outsideBlock.weight;
        power_supply.text = _model.outsideBlock.power_supply;
        pipe_diameter_liquid_gas.text = _model.outsideBlock.pipe_diameter_liquid_gas;
        minimum_pipe_length.text = _model.outsideBlock.minimum_pipe_length;
        max_pipe_length_height_difference.text = _model.outsideBlock.max_pipe_length_height_difference;
        temperature_range_outdoor_air_cooling.text = _model.outsideBlock.temperature_range_outdoor_air_cooling;
        temperature_range_outdoor_air_heating.text = _model.outsideBlock.temperature_range_outdoor_air_heating;
        refrigerant.text = _model.outsideBlock.refrigerant;
    }

    public void Open3D()
    {
        var AM = AppManager.Instance;
        AM.backgrond.SetActive(false);
        AM.index = index;
        AM.SwitchScreen(7);
    }

    private async void OnEnable()
    {
        await new WaitUntil(() => AppManager.Instance);
        AppManager.Instance.backgrond.SetActive(true);
    }
}
