using System.Text;
using TMPro;
using Unity.Profiling;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ProfilerStats : MonoBehaviour
{
    [SerializeField] private DisplayState _displayState = DisplayState.DevBuildOnly;
    [SerializeField] private float _fpsRefreshRate = 0.2f;

    private enum DisplayState
    {
        Hide,
        EditorOnly,
        DevBuildOnly,
        Display
    }

    private ProfilerRecorder _triangleRecorder;
    private ProfilerRecorder _drawCallsRecorder;
    private ProfilerRecorder _verticesRecorder;
    private TMP_Text _statOverlay;

    private int _framesCount;
    private float _framesTime;
    private float _lastFPS;

    private void Awake()
    {
        _statOverlay = GetComponent<TMP_Text>();

        if (_displayState == DisplayState.Hide)
            gameObject.SetActive(false);
        else if (_displayState == DisplayState.EditorOnly)
        {
#if !UNITY_EDITOR
            gameObject.SetActive(false);
#endif
        }
        else if (_displayState == DisplayState.DevBuildOnly)
            gameObject.SetActive(Debug.isDebugBuild);
    }

    private void OnEnable()
    {
        _triangleRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
        _drawCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
        _verticesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
    }

    private void OnDisable()
    {
        _triangleRecorder.Dispose();
        _drawCallsRecorder.Dispose();
        _verticesRecorder.Dispose();
    }

    private void Update()
    {
        StringBuilder sb = new StringBuilder(500);

        _framesCount++;
        _framesTime += Time.unscaledDeltaTime;
        if (_framesTime > _fpsRefreshRate)
        {
            float fps = _framesCount / _framesTime;
            _lastFPS = fps;
            _framesCount = 0;
            _framesTime = 0;
        }
        sb.AppendLine($"FPS: {_lastFPS:F2}");
        sb.AppendLine($"Verts: {_verticesRecorder.LastValue / 1000}k");
        sb.AppendLine($"Tris: {_triangleRecorder.LastValue / 1000}k");
        sb.AppendLine($"DrawCalls: {_drawCallsRecorder.LastValue}");

        _statOverlay.text = sb.ToString();
    }
}
