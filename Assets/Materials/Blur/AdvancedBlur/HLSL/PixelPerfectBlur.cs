using UnityEngine;

[ExecuteInEditMode]
public class PixelPerfectBlur : MonoBehaviour
{
    [Range(0.0f, 0.015f)]
    [SerializeField] private float m_blurAmount;
    [SerializeField] private Material mat;

    private readonly int _BlurY = Shader.PropertyToID("BlurY");
    private readonly int _BlurX = Shader.PropertyToID("BlurX");

    void Start()
    {
        // Initialize the function
        SetPixelSize();
    }

    // Corrects the pixel aspect regarding to the screen
    void SetPixelSize()
    {
        float aspect = (float)Screen.width / (float)Screen.height;

        float scaleX = m_blurAmount;
        float scaleY = m_blurAmount;

        if(aspect > 1f)
            scaleX /= aspect;
        else
            scaleY *= aspect;

        mat.SetFloat(_BlurY, scaleY);
        mat.SetFloat(_BlurX, scaleX);
    }
}