using UnityEngine;

public class QunariFemale : CharacterModel
{
    protected override Color minHue
    {
        get
        {
            return new Color(0.745f, 0.624f, 0.357f); // Good human value
        }
    }
    protected override Color maxHue
    {
        get
        {
            return new Color(0.745f, 0.5f, 0.357f); // Good human value
        }
    }

    protected override float maxLightness { get { return 0.25f; } }
    protected override float maxDarkness { get { return 0.65f; } }

    protected override float leg1LengthMultiplier { get { return 1f; }}
    protected override float leg2LengthMultiplier { get { return 1.1f; } }
    protected override Vector2 leg1TargetPosition
    {
        get
        {
            return new Vector2(
                0.65f - hips.transform.localScale.x * 0.75f,
                -0.29f
            );
        }
    }
    protected override Vector2 leg2TargetPosition
    {
        get
        {
            return new Vector2(
                hips.transform.localScale.x * 0.6f,
                -0.2f
            );
        }
    }
    protected override Vector2 hipsTargetPosition
    {
        get
        {
            return new Vector2(0.22f, -1.2f + legLength * 4f);
        }
    }
    protected override Vector2 torsoTargetPosition
    {
        get
        {
            return hipsTargetPosition + new Vector2(-0.04f, -0.05f + hips.transform.localScale.y * 1.2f);
        }
    }
    protected override Vector2 arm1TargetPosition
    {
        get
        {
            return torsoTargetPosition + new Vector2(
                0.115f - torso.transform.localScale.x,
                0.115f + torso.transform.localScale.y
            );
        }
    }
    protected override Vector2 arm2TargetPosition
    {
        get
        {
            return torsoTargetPosition + new Vector2(
                -0.575f + torso.transform.localScale.x,
                torso.transform.localScale.y
            );
        }
    }
    protected override Vector2 neckTargetPosition
    {
        get
        {
            return torsoTargetPosition + new Vector2(
                -0.1f - torso.transform.localScale.x * 0.1f,
                0.05f + torso.transform.localScale.y
            );
        }
    }
    protected override Vector2 headTargetPosition
    {
        get
        {
            return neckTargetPosition + new Vector2(
                -0.1f + neck.transform.localScale.x * 0.1f,
                neck.transform.localScale.y * 0.3f
            );
        }
    }

}
