using System.Threading.Tasks;
using UnityEngine;

public class EquipmentActivityScript : MonoBehaviour
{


    private async Task InitializeScreen()
    {
        if (WebHandler.Instance == null || AppManager.Instance == null)
            await new WaitForSeconds(.01f);
    }

    private async Task BottomBar()
    {
        if (!AppManager.Instance.bottomBar.activeSelf)
            await new WaitForSeconds(.01f);
        AppManager.Instance.bottomBar.SetActive(true);
    }

    private async void OnEnable()
    {
        await InitializeScreen();
        var AM = AppManager.Instance;
        if(AM.activeScreenIndex!=0)
        {
            await BottomBar();
            if (AM.prevScreenIndex.Count == 1 && AM.activeScreenIndex == 6)
            {
                AM.backButton.SetActive(false);
            }
            else
            {
                AM.backButton.SetActive(true);
            }
        }
    }

    public void Cond()
    {
        var AM = AppManager.Instance;
        AM._equipment = 1;
        AM.SwitchScreen(13);
    }

    public void Teplo()
    {
        var AM = AppManager.Instance;
        AM._equipment = 0;
        AM.SwitchScreen(13);
    }
}
