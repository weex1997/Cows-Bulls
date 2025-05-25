using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public TMP_Dropdown dropDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropDown); });//dropDown listener
    }

    public void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                break;
            case 1:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
                break;
            default:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                break;
        }
    }
}
