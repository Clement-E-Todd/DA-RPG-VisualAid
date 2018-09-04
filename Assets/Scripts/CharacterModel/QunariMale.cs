using UnityEngine;

public class QunariMale : CharacterModel
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
    protected override float leg2LengthMultiplier { get { return 1.25f; } }
    protected override Vector2 leg1TargetPosition
    {
        get
        {
            return new Vector2(
                0.81f - hips.transform.localScale.x,
                -0.44f
            );
        }
    }
    protected override Vector2 leg2TargetPosition
    {
        get
        {
            return new Vector2(
                -0.48f + hips.transform.localScale.x,
                0.34f
            );
        }
    }
    protected override Vector2 hipsTargetPosition
    {
        get
        {
            return new Vector2(0.22f, -0.75f + legLength * 3.5f);
        }
    }
    protected override Vector2 torsoTargetPosition
    {
        get
        {
            return hipsTargetPosition + new Vector2(0.04f, 0.43f + hips.transform.localScale.y);
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
                -0.15f - torso.transform.localScale.x * 0.1f,
                0.225f + torso.transform.localScale.y
            );
        }
    }
    protected override Vector2 headTargetPosition
    {
        get
        {
            return neckTargetPosition + new Vector2(
                neck.transform.localScale.x * 0.1f,
                neck.transform.localScale.y * 0.475f
            );
        }
    }

}
