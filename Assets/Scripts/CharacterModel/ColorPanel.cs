using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ColorPanel : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Image preview;

    public EventTrigger.TriggerEvent OnValueChanged;

    public Color GetColor()
    {
        return new Color(redSlider.value, greenSlider.value, blueSlider.value);
    }

    public void OnColorSliderValueChanged()
    {
        preview.color = GetColor();

        BaseEventData eventData = new BaseEventData(EventSystem.current);
        eventData.selectedObject = gameObject;
        OnValueChanged.Invoke(eventData);
    }
}
