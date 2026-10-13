using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    RectTransform _HUDTextPanel;
    [SerializeField]
    TMP_Text _hudInfo;
    [SerializeField]
    TMP_Text _ammoCount;
    [SerializeField]
    TMP_Text _score;
    [SerializeField]
    Image _crosshair;

    public static HUDController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void SetHUD(string command)
    {
        _HUDTextPanel.gameObject.SetActive(true);
        _hudInfo.text = command;
    }

    public void ClearHUD()
    {
        _HUDTextPanel.gameObject.SetActive(false);
        _hudInfo.text = string.Empty;
    }

    public void SetAmmo(int ammo)
    {
        if (!_ammoCount.gameObject.activeInHierarchy)
            _ammoCount.gameObject.SetActive(true);

        _ammoCount.text = $"Ammo count: {ammo}";
    }

    internal void SetCrosshair()
    {
        _crosshair.gameObject.SetActive(true);
    }
}
