using System;
using System.Collections.Generic;

[Serializable]
public class Res
{
    public res res = new res();
}


[Serializable]
public class res
{
    public List<news> news = new List<news>();
}

[Serializable]
public class news
{
    public int id;
    public string name;
    public string desc;
}

