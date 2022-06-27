using UnityEngine;

public class TipsWindow : MonoBehaviour
{
    [SerializeField] GameObject windowClose;

    public void OpenTips()
    {
        windowClose.SetActive(true);
    }

    public void CloseTips()
    {
        windowClose.SetActive(false);
    }
}
