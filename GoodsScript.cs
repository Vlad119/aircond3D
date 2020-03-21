using System;
using System.Collections.Generic;
using UnityEngine;

public class GoodsScript : MonoBehaviour
{
    [Serializable]
    public class Equipment
    {
        public List<CondBrand> condBrand = new List<CondBrand>();
        public List<TeploBrands> teploBrand = new List<TeploBrands>();
    }


    [Serializable]
    public class TeploBrands
    {
        public string name;
        public List<Series> series = new List<Series>();
    }


    [Serializable]
    public class CondBrand
    {
        public string cond_name;
        public List<Series> series = new List<Series>();
    }


    [Serializable]
    public class Series
    {
        public string series_name;
        public List<Models> model = new List<Models>();
    }


    [Serializable]
    public class Models
    {
        public int index;
        public string name;
        public Sprite main_image;
        public List<Sprite> img;
        public InsideBlock insideBlock = new InsideBlock();
        public OutsideBlock outsideBlock = new OutsideBlock();       
    }


    [Serializable]
    public class InsideBlock
    {
        public string сooling_capacity;
        public string heat_output;
        public string power_consumption_cooling;
        public string power_consumption_heating;
        public string energy_EER_COP;
        public string seasonal_energy_efficiency_SEER_SCOP;
        public string energy_efficiency_class_SEER_SCOP;
        public string sound_pressure_cooling;
        public string sound_pressure_heating;
        public string air_consumption_cooling;
        public string air_consumption_heating;
        public string dimensions;
        public string weight;
        public string drain_diameter;
    }


    [Serializable]
    public class OutsideBlock
    {
        public string sound_pressure_cooling;
        public string sound_pressure_heating;
        public string sound_power;
        public string dimensions;
        public string weight;
        public string power_supply;
        public string pipe_diameter_liquid_gas;
        public string minimum_pipe_length;
        public string max_pipe_length_height_difference;
        public string temperature_range_outdoor_air_cooling;
        public string temperature_range_outdoor_air_heating;
        public string refrigerant;
    }
}