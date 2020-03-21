using TMPro;
using UnityEngine;

public class NewsPrefScript : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text desc;
    public void Create(news _news)
    {
        name.text = _news.name;
        desc.text = _news.desc;
    }
}
