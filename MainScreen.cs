using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    public static MainScreen Instance;
    [SerializeField] private LoopScrollRect loopScrollRect;
    [SerializeField] private Transform Content;
    //    [SerializeField] private HistoryContent historyContent;
    public TMP_Text CanHelpText;
    [SerializeField] private GameObject noAds;
    public bool need_reload  = true;

    private async void Start()
    {
        Instance = this;
    }
}
