using UnityEngine;
using UnityEngine.UI;

public class CharacterEditor : MonoBehaviour
{
    public CharacterModel currentModel;

    public Slider skinHueSlider;
    public Slider skinLightnessSlider;

    public Slider headWidthSlider;
    public Slider headHeightSlider;

    public Slider neckWidthSlider;
    public Slider neckHeightSlider;

    public Slider armThicknessSlider;
    public Slider armLengthSlider;

    public Slider torsoWidthSlider;
    public Slider torsoHeightSlider;

    public Slider hipWidthSlider;
    public Slider hipHeightSlider;

    public Slider legThicknessSlider;
    public Slider legLengthSlider;

    private void Start()
    {
        OnSkinSliderValueChanged();
        OnProportionSliderValueChanged();
    }

    public void OnSkinSliderValueChanged()
    {
        currentModel.SetSkinTone(skinHueSlider.value, skinLightnessSlider.value);
    }

    public void OnProportionSliderValueChanged()
    {
        currentModel.leg1.transform.localScale = new Vector3(legThicknessSlider.value, 1f, 1f);
        currentModel.leg2.transform.localScale = new Vector3(legThicknessSlider.value, 1f, 1f);
        currentModel.legLength = legLengthSlider.value;

        currentModel.arm1.transform.localScale = new Vector3(armThicknessSlider.value, 1f, 1f);
        currentModel.arm2.transform.localScale = new Vector3(armThicknessSlider.value, 1f, 1f);
        currentModel.armLength = armLengthSlider.value;

        currentModel.hips.transform.localScale = new Vector3(hipWidthSlider.value, hipHeightSlider.value, 1f);
        currentModel.torso.transform.localScale = new Vector3(torsoWidthSlider.value, torsoHeightSlider.value, 1f);
        currentModel.neck.transform.localScale = new Vector3(neckWidthSlider.value, neckHeightSlider.value, 1f);
        currentModel.head.transform.localScale = new Vector3(headWidthSlider.value, headHeightSlider.value, 1f);

        currentModel.UpdateProportions();
    }
}
