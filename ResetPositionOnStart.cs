using UnityEngine;

public class ResetPositionOnStart : MonoBehaviour
{
    private RectTransform rectTransform;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.anchoredPosition = Vector3.zero;
    }
}