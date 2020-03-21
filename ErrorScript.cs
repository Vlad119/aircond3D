using UnityEngine;

public class ErrorScript : MonoBehaviour
{
    public float timeRemaining = 2f;
    public GameObject error;

    private void OnEnable()
    {
        var AM = AppManager.Instance;
        timeRemaining = 2f;
    }

    void Update()
    {
        var AM = AppManager.Instance;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                error.SetActive(false);
            }
        }
    }
}
