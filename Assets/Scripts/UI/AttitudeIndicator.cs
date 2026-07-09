using UnityEngine;
using TMPro;

public class AttitudeIndicator : MonoBehaviour
{
    [SerializeField] private float MaxPitchRange = -487;
    [Range(-1, 1)][SerializeField] private float currentValue;
    [SerializeField] private float PitchSpeed = 5;
    [SerializeField] private float RollSpeed = 5;

    private float currentPitchRange;
    private float currentRollAngle;

    private UIManager root;

    private RectTransform pitchTransform;
    private RectTransform rollTransform;
    private RectTransform dot;
    private TMP_Text rollText;
    private TMP_Text AOAText;
    private void Start()
    {
        dot = transform.Find("Dot").GetComponent<RectTransform>();
        rollText = transform.Find("RollText").GetComponentInChildren<TMP_Text>();
        AOAText = transform.Find("AOAText").GetComponentInChildren<TMP_Text>();
        rollTransform = transform.Find("Roll").GetComponent<RectTransform>();
        pitchTransform = rollTransform.Find("Pitch").GetComponent<RectTransform>();

        root = GetComponentInParent<UIManager>();
    }

    private void LateUpdate()
    {
        SetPitch();
        SetRoll();
        SetDotAndAOA();
    }

    private void SetDotAndAOA()
    {
        AOAText.text = $"AOA: {Mathf.Round(root.Aircraft.VerticalAOA)}";
        Vector2 dotTargetPosition = new Vector2(
            root.Aircraft.HorizontalAOA / 90,
            root.Aircraft.VerticalAOA / 90
        ) * MaxPitchRange;

        dot.anchoredPosition = Vector2.Lerp(
            dot.anchoredPosition,
            dotTargetPosition,
            PitchSpeed * Time.deltaTime
        );
    }
    private void SetPitch()
    {
        float targetPitchRange = root.Aircraft.Pitch / 90 * MaxPitchRange;

        currentPitchRange = Mathf.Lerp(
            currentPitchRange,
            targetPitchRange,
            PitchSpeed * Time.deltaTime
        );

        pitchTransform.anchoredPosition = Vector2.up * currentPitchRange;
    }


    private void SetRoll()
    {
        currentRollAngle = Mathf.LerpAngle(currentRollAngle, root.Aircraft.Roll, RollSpeed * Time.deltaTime);

        if (currentRollAngle > 180) currentRollAngle -= 360;
        if (currentRollAngle < -180) currentRollAngle += 360;

        rollTransform.localEulerAngles = Vector3.forward * currentRollAngle;
        rollText.text = $"ROLL: {Mathf.Round(currentRollAngle)}";
    }
}
