using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject generalSection;
    [SerializeField] private GameObject audioSection;
    [SerializeField] private GameObject graphicSection;
    [SerializeField] private GameObject inputSection;

    private enum SettingState
    {
        General,
        Audio,
        Graphic,
        Input
    }
    private SettingState _currentState = SettingState.General;

    public void ChangeSection(int index)
    {
        var state = (SettingState)index;
        
        SetSectionActive(_currentState, false);
        SetSectionActive(state, true);
        _currentState = state;
    }

    private void SetSectionActive(SettingState state, bool value)
    {
        switch (state)
        {
            case SettingState.General:
                generalSection.SetActive(value);
                break;
            case SettingState.Audio:
                audioSection.SetActive(value);
                break;
            case SettingState.Graphic:
                graphicSection.SetActive(value);
                break;
            case SettingState.Input:
                inputSection.SetActive(value);
                break;
            default:
                Debug.LogError($"{state}는 없는 상태임");
                break;
        }
    }
}
