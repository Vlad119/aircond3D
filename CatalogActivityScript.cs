using UnityEngine;

public class CatalogActivityScript : MonoBehaviour
{
    public GameObject cond;
    public GameObject teplo;

    public void Cond()
    {
        AppManager.Instance.SwitchScreen(8);
    }

    public void Teplo()
    {
        //AppManager.Instance.SwitchScreen();
    }
}
