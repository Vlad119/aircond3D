using UnityEngine;

public class infoScrollScript : MonoBehaviour
{
    public GameObject[] info;

    public void ChoiseInfo()
    {
        var AM = AppManager.Instance;
        CloseAllInfo();
        switch (AM.index)
        {
            case 1: { info[0].SetActive(true); break; }
            case 2: { info[1].SetActive(true); break; }
            case 3: { info[2].SetActive(true); break; }
            case 4: { info[3].SetActive(true); break; }
            case 5: { info[4].SetActive(true); break; }
            case 6: { info[5].SetActive(true); break; }
            case 7: { info[6].SetActive(true); break; }
        }
    }

    public void CloseAllInfo()
    {
        foreach (GameObject info in info)
        {
            info.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ChoiseInfo();
    }

    public void Open3D()
    {
        AppManager.Instance.SwitchScreen(7);
    }
}
