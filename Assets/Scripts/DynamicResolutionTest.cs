using UnityEngine;
using UnityEngine.UI;

public class DynamicResolutionTest : MonoBehaviour
{
    private RenderTexture _renderTexture;

    [SerializeField]
    private Texture2D _tex;

    /// <summary>
    /// RenderTexture確認用UI
    /// </summary>
    [SerializeField]
    private RawImage _previewRenderTexture;

    private Camera _camera;


    /// <summary>
    /// 毎フレームSclableBufferの値を書き換えてみるテストフラグ
    /// </summary>
    private bool _isUpdateScalableBuffer = false;

    private float _updateScale = 0;

    void Start()
    {
        _camera = Camera.main;

        _renderTexture = new RenderTexture(300, 300, 24);

        // デフォルトでダイナミックスケールON
        _renderTexture.useDynamicScale = true;

        _previewRenderTexture.texture = _renderTexture;
    }

    void Blit()
    {
        // RenderTextureへ書き込み
        Graphics.Blit(_tex, _renderTexture);
    }

    void BlitFrameBuffer()
    {
        // カメラのレンダーターゲットにRenderTextureをセットして描画
        _camera.targetTexture = _renderTexture;
        _camera.Render();

        // カメラのレンダーターゲットを元のフレームバッファへ戻す
        _camera.targetTexture = null;

        Debug.Log("cam.allowDynamicResolution : " + _camera.allowDynamicResolution);
    }

    void Update()
    {
        if (_isUpdateScalableBuffer)
        {
            if (_updateScale > 1.0f)
            {
                _updateScale = 0.01f;
            }

            ScalableBufferManager.ResizeBuffers(_updateScale, _updateScale);
            _updateScale += 0.01f;
        }
    }

    void OnGUI()
    {
        float buttonW = 300f;
        float buttonH = 100f;

        if (GUILayout.Button("W0.1 : H0.1", GUILayout.Width(buttonW), GUILayout.Height(buttonH)))
        {
            ResizeBuffers(0.1f);
        }

        if (GUILayout.Button("W0.5 : H0.5", GUILayout.Width(buttonW), GUILayout.Height(buttonH)))
        {
            ResizeBuffers(0.5f);
        }

        if (GUILayout.Button("W1.0 : H1.0", GUILayout.Width(buttonW), GUILayout.Height(buttonH)))
        {
            ResizeBuffers(1.0f);
        }


        if (GUILayout.Button("Blit", GUILayout.Width(buttonW), GUILayout.Height(buttonH)))
        {
            Blit();
        }

        /*
        if (GUILayout.Button("RT Toggle : " + _renderTexture.useDynamicScale, GUILayout.Width(buttonW),
            GUILayout.Height(buttonH)))
        {
            // RenderTextureのDynamicResolution切り替え
            _renderTexture.useDynamicScale = _renderTexture.useDynamicScale == false;
        }

        if (GUILayout.Button("Camera Toggle : " + _camera.allowDynamicResolution, GUILayout.Width(buttonW),
            GUILayout.Height(buttonH)))
        {
            // CameraのDynamicResolution切り替え
            _camera.allowDynamicResolution = _camera.allowDynamicResolution == false;
        }

        if (GUILayout.Button("Blit FrameBuffer", GUILayout.Width(buttonW), GUILayout.Height(buttonH)))
        {
            BlitFrameBuffer();
        }

        if (GUILayout.Button("Toggle OnUpdate Scalable : " + _isUpdateScalableBuffer, GUILayout.Width(buttonW),
            GUILayout.Height(buttonH)))
        {
            _isUpdateScalableBuffer = _isUpdateScalableBuffer == false;
        }


        if (GUILayout.Button("CameraTargetTexture is Null", GUILayout.Width(buttonW), GUILayout.Height(buttonH)))
        {
            // targetTextureにnull突っ込むとSclableBufferが使えなくなる
            _camera.targetTexture = null;
        }

        if (GUILayout.Button("Screen.SetResolution", GUILayout.Width(buttonW), GUILayout.Height(buttonH)))
        {
            int w = Screen.width / 10;
            int h = Screen.height / 10;
            Screen.SetResolution(w, h, FullScreenMode.ExclusiveFullScreen);
        }
        */
    }

    // ResizeBufferしてRenderTextureの更新
    void ResizeBuffers(float scale)
    {
        ScalableBufferManager.ResizeBuffers(scale, scale);
        Blit();
    }
}
