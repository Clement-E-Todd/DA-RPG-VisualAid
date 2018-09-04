using UnityEngine;
using UnityEngine.UI;

public class ColorPanel : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Image preview;

    public Color GetColor()
    {
        return new Color(redSlider.value, greenSlider.value, blueSlider.value);
    }

    public void OnColorSliderValueChanged()
    {
        preview.color = GetColor();
    }
}
