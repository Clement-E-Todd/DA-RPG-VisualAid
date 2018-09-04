using UnityEngine;

public abstract class CharacterModel : MonoBehaviour
{
    public Transform leg1;
    public Transform leg2;
    public Transform hips;
    public Transform torso;
    public Transform arm1;
    public Transform arm2;
    public Transform neck;
    public Transform head;

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

        leg1.Find("Sprite").GetComponent<SpriteRenderer>().color = skinColor;
        leg2.Find("Sprite").GetComponent<SpriteRenderer>().color = skinColor;
        hips.Find("Sprite").GetComponent<SpriteRenderer>().color = skinColor;
        torso.Find("Sprite").GetComponent<SpriteRenderer>().color = skinColor;
        arm1.Find("Sprite").GetComponent<SpriteRenderer>().color = skinColor;
        arm2.Find("Sprite").GetComponent<SpriteRenderer>().color = skinColor;
        neck.Find("Sprite").GetComponent<SpriteRenderer>().color = skinColor;
        head.Find("Sprite").GetComponent<SpriteRenderer>().color = skinColor;
    }

    public void UpdateProportions()
    {
        leg1.localScale = new Vector3(
            leg1.localScale.x, 1f + (legLength - 1f) * leg1LengthMultiplier, 1f
        );
        leg2.localScale = new Vector3(
            leg2.localScale.x, 1f + (legLength - 1f) * leg2LengthMultiplier, 1f
        );

        arm1.localScale = new Vector3(
            arm1.localScale.x, 1f + (armLength - 1f) * leg1LengthMultiplier, 1f
        );
        arm2.localScale = new Vector3(
            arm2.localScale.x, 1f + (armLength - 1f) * leg2LengthMultiplier, 1f
        );

        leg1.localPosition = leg1TargetPosition;
        leg2.localPosition = leg2TargetPosition;
        hips.localPosition = hipsTargetPosition;
        torso.localPosition = torsoTargetPosition;
        arm1.localPosition = arm1TargetPosition;
        arm2.localPosition = arm2TargetPosition;
        neck.localPosition = neckTargetPosition;
        head.localPosition = headTargetPosition;
    }
}
