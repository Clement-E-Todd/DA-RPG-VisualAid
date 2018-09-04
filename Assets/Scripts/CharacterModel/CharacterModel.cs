using UnityEngine;

public abstract class CharacterModel : MonoBehaviour
{
    public BodyPart leg1;
    public BodyPart leg2;
    public BodyPart hips;
    public BodyPart torso;
    public BodyPart arm1;
    public BodyPart arm2;
    public BodyPart neck;
    public BodyPart head;

    protected abstract Color minHue { get; }
    protected abstract Color maxHue { get; }

    protected abstract float maxLightness { get; }
    protected abstract float maxDarkness { get; }

    public float legLength = 1f;
    public float armLength = 1f;

    protected abstract float leg1LengthMultiplier { get; }
    protected abstract float leg2LengthMultiplier { get; }

    protected abstract Vector2 leg1TargetPosition { get; }
    protected abstract Vector2 leg2TargetPosition { get; }
    protected abstract Vector2 hipsTargetPosition { get; }
    protected abstract Vector2 torsoTargetPosition { get; }
    protected abstract Vector2 arm1TargetPosition { get; }
    protected abstract Vector2 arm2TargetPosition { get; }
    protected abstract Vector2 neckTargetPosition { get; }
    protected abstract Vector2 headTargetPosition { get; }

    public void SetSkinTone(float hue, float lightness)
    {
        Color skinColor = Color.Lerp(minHue, maxHue, hue);

        if (lightness > 0.5f)
        {
            Color maxLightenedSkinColor = Color.Lerp(skinColor, Color.white, maxLightness);
            skinColor = Color.Lerp(skinColor, maxLightenedSkinColor, (lightness - 0.5f) * 2f);
        }
        else
        {
            Color maxDarkenedSkinColor = Color.Lerp(skinColor, Color.black, maxDarkness);
            skinColor = Color.Lerp(skinColor, maxDarkenedSkinColor, (0.5f - lightness) * 2f);
        }

        leg1.mainSprite.color = skinColor;
        leg2.mainSprite.color = skinColor;
        hips.mainSprite.color = skinColor;
        torso.mainSprite.color = skinColor;
        arm1.mainSprite.color = skinColor;
        arm2.mainSprite.color = skinColor;
        neck.mainSprite.color = skinColor;
        head.mainSprite.color = skinColor;
    }

    public void UpdateProportions()
    {
        leg1.transform.localScale = new Vector3(
            leg1.transform.localScale.x, 1f + (legLength - 1f) * leg1LengthMultiplier, 1f
        );
        leg2.transform.localScale = new Vector3(
            leg2.transform.localScale.x, 1f + (legLength - 1f) * leg2LengthMultiplier, 1f
        );

        arm1.transform.localScale = new Vector3(
            arm1.transform.localScale.x, 1f + (armLength - 1f) * leg1LengthMultiplier, 1f
        );
        arm2.transform.localScale = new Vector3(
            arm2.transform.localScale.x, 1f + (armLength - 1f) * leg2LengthMultiplier, 1f
        );

        leg1.transform.localPosition = leg1TargetPosition;
        leg2.transform.localPosition = leg2TargetPosition;
        hips.transform.localPosition = hipsTargetPosition;
        torso.transform.localPosition = torsoTargetPosition;
        arm1.transform.localPosition = arm1TargetPosition;
        arm2.transform.localPosition = arm2TargetPosition;
        neck.transform.localPosition = neckTargetPosition;
        head.transform.localPosition = headTargetPosition;
    }
}
