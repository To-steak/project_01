using UnityEditor;
using UnityEngine;

public class ScriptableObjectEditor : EditorWindow
{
    private enum EditorTab { Player, Enemy, Weapon }
    private EditorTab _currentTab = EditorTab.Player;

    private ScriptableObject[] _assets;
    private ScriptableObject _selectedAsset;
    private Editor _assetEditor;

    private Vector2 _scrollPosList;
    private Vector2 _scrollPosDetail;

    private GUIStyle _buttonStyle;
    private GUIStyle _refreshStyle;

    [MenuItem("Tools/Scriptable Object Editor")]
    public static void ShowWindow()
    {
        GetWindow<ScriptableObjectEditor>("Editor");
    }

    private void OnEnable()
    {
        LoadAssets();
        InitStyle();
    }

    private void InitStyle()
    {
        _buttonStyle = new GUIStyle()
        {
            fontSize = 12,
            fixedHeight = 28,
            alignment = TextAnchor.MiddleLeft,
            padding = new RectOffset(10, 10, 0, 0),
            normal = { background = Texture2D.grayTexture, textColor = Color.white },
            hover = { background = MakeTex(new Color(0.4f, 0.4f, 0.4f)), textColor = Color.white }
        };

        _refreshStyle = new GUIStyle()
        {
            fontSize = 12,
            fixedHeight = 28,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            normal = { background = MakeTex(new Color(0.2f, 0.5f, 0.2f)), textColor = Color.white },
            hover = { background = MakeTex(new Color(0.3f, 0.6f, 0.3f)), textColor = Color.white }
        };
    }

    private void LoadAssets()
    {
        string filter = _currentTab switch
        {
            EditorTab.Player => "t:PlayerConfig",
            EditorTab.Enemy => "t:EnemyConfig",
            EditorTab.Weapon => "t:WeaponSO",
            _ => ""
        };

        string[] guids = AssetDatabase.FindAssets(filter);
        _assets = new ScriptableObject[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            _assets[i] = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
        }
    }

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        _currentTab = (EditorTab)GUILayout.Toolbar(
            (int)_currentTab,
            new string[] { "Player", "Enemy", "Weapon" },
            GUILayout.Height(30)
        );

        if (EditorGUI.EndChangeCheck())
        {
            _selectedAsset = null;
            if (_assetEditor != null) DestroyImmediate(_assetEditor);
            LoadAssets();
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        DrawLeftPanel();
        DrawRightPanel();
        EditorGUILayout.EndHorizontal();
    }

    private Texture2D MakeTex(Color color)
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, color);
        tex.Apply();
        return tex;
    }

    private void DrawLeftPanel()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(220), GUILayout.ExpandHeight(true));
        _scrollPosList = EditorGUILayout.BeginScrollView(_scrollPosList);

        if (GUILayout.Button("Refresh", _refreshStyle))
        {
            LoadAssets();
        }

        EditorGUILayout.Space();

        if (_assets != null)
        {
            foreach (var asset in _assets)
            {
                if (asset == null) continue;

                GUI.backgroundColor = (_selectedAsset == asset) ? Color.cyan : Color.white;

                if (GUILayout.Button(asset.name, _buttonStyle))
                {
                    _selectedAsset = asset;
                    if (_assetEditor != null) DestroyImmediate(_assetEditor);
                    _assetEditor = Editor.CreateEditor(_selectedAsset);
                }

                GUI.backgroundColor = Color.white;
                GUILayout.Space(4); // 버튼 사이 간격
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void DrawRightPanel()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        _scrollPosDetail = EditorGUILayout.BeginScrollView(_scrollPosDetail);

        if (_selectedAsset != null && _assetEditor != null)
        {
            EditorGUILayout.LabelField(_selectedAsset.name, EditorStyles.boldLabel);
            EditorGUILayout.Space();
            _assetEditor.OnInspectorGUI();
        }
        else
        {
            GUILayout.FlexibleSpace();
            GUIStyle style = new GUIStyle(EditorStyles.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 12,
                normal = { textColor = Color.gray }
            };
            GUILayout.Label("← asset을 선택하세요", style);
            GUILayout.FlexibleSpace();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}