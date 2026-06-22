using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject backPanel;
    [SerializeField] private GameObject saveLoadPanel;
    [SerializeField] private GameObject settingsPanel;

    private enum TitleState
    {
        Title,
        SaveLoad,
        Settings
    }
    private TitleState _currentState = TitleState.Title;

    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ChangePanel(int index)
    {
        var state = (TitleState)index;

        SetPanelActive(_currentState, false);
        SetPanelActive(state, true);

        backPanel.SetActive(state != TitleState.Title);

        _currentState = state;
    }

    private void SetPanelActive(TitleState state, bool value)
    {
        switch (state)
        {
            case TitleState.Title:
                titlePanel.SetActive(value);
                break;
            case TitleState.SaveLoad:
                saveLoadPanel.SetActive(value);
                break;
            case TitleState.Settings:
                settingsPanel.SetActive(value);
                break;
            default:
                Debug.LogError($"{state}는 없는 상태임");
                break;
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
