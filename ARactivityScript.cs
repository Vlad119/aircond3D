using UnityEngine;

public class ARactivityScript : MonoBehaviour
{
    public GameObject[] model;

    public void ChoiseModel()
    {
        var AM = AppManager.Instance;
        CloseAllModels();
        if (AM.index != null)
        {
            switch (AM.index)
            {
                case 1: { model[0].SetActive(true); break; }
                case 2: { model[1].SetActive(true); break; }
                case 3: { model[2].SetActive(true); break; }
                case 4: { model[3].SetActive(true); break; }
                case 5: { model[4].SetActive(true); break; }
                case 6: { model[5].SetActive(true); break; }
                case 7: { model[6].SetActive(true); break; }
            }
        }
    }

    public void CloseAllModels()
    {
        foreach (GameObject model in model)
        {
            model.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ChoiseModel();
    }
}
