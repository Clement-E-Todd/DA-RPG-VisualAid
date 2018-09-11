using UnityEngine;
using UnityEngine.UI;

public class ColorPresetButton : MonoBehaviour
{
    public ColorPanel colorPanel;

    public void OnClicked()
    {
        Image image = GetComponent<Image>();

        colorPanel.redSlider.value = image.color.r;
        colorPanel.greenSlider.value = image.color.g;
        colorPanel.blueSlider.value = image.color.b;

        colorPanel.OnColorSliderValueChanged();
    }
}
