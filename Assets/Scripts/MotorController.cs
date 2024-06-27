using System;
using Unity.Mathematics;
using UnityEngine;

public class MotorController : MonoBehaviour
{
    private static MotorController _singleton;
    public static MotorController Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Debug.Log("MotorController instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    [SerializeField] private Transform leftMotorPadel;
    [SerializeField] private Transform rightMotorPadel;

    [SerializeField] private float leftAngle;
    [SerializeField] private float rightAngle;

    [SerializeField] private float speedZPos = 200f;
    [SerializeField] private float speedZNeg = 100f;
    [SerializeField] private float speedX = 100f;

    [SerializeField] private AnimationCurve speedCurveZPos;
    [SerializeField] private AnimationCurve speedCurveZNeg;
    [SerializeField] private AnimationCurve speedCurveXPos;
    [SerializeField] private AnimationCurve speedCurveXNeg;

    [SerializeField] private float springG = 1f;
    [SerializeField] private float springSpeed = 100f;

    [SerializeField] private float maxAngle = 40f;

    private float longG = 0f;
    private float latG = 0f;

    void Awake()
    {
        Singleton = this;
    }

    void FixedUpdate()
    {
        UpdateAngles();

        leftMotorPadel.eulerAngles = new Vector3(0f,0f,leftAngle);
        rightMotorPadel.eulerAngles = new Vector3(0f,0f,rightAngle);
    }

    private void UpdateAngles()
    {
        float zRotPos = longG > 0 ? longG : 0;
        float zRotNeg = longG < 0 ? longG : 0;
        float xRotPos = latG > 0 ? latG : 0;
        float xRotNeg = latG < 0 ? latG : 0;

        float leftDelta = 0f;
        float rightDelta = 0f;

        leftDelta += zRotPos * speedCurveZPos.Evaluate(leftAngle / maxAngle * 2) * speedZPos;
        rightDelta += zRotPos * speedCurveZPos.Evaluate(rightAngle / maxAngle * 2) * speedZPos;
        leftDelta += zRotNeg * speedCurveZNeg.Evaluate(leftAngle / maxAngle * 2) * speedZNeg;
        rightDelta += zRotNeg * speedCurveZNeg.Evaluate(rightAngle / maxAngle * 2) * speedZNeg;

        leftDelta -= xRotPos * speedCurveXPos.Evaluate(leftAngle / maxAngle * 2) * speedX;
        rightDelta += xRotPos * speedCurveXPos.Evaluate(rightAngle / maxAngle * 2) * speedX;
        leftDelta -= xRotNeg * speedCurveXNeg.Evaluate(leftAngle / maxAngle * 2) * speedX;
        rightDelta += xRotNeg * speedCurveXNeg.Evaluate(rightAngle / maxAngle * 2) * speedX;

        float fdt = Time.fixedDeltaTime;
        // leftDelta += -(leftAngle - Mathf.MoveTowards(leftAngle, 0f, springSpeed));
        // rightDelta += -(rightAngle - Mathf.MoveTowards(rightAngle, 0f, springSpeed));

        if (leftDelta > 0 && leftAngle + leftDelta * fdt > maxAngle)
            leftDelta = 0;
        if (leftDelta < 0 && leftAngle + leftDelta * fdt < -maxAngle)
            leftDelta = 0;
        if (rightDelta > 0 && rightAngle + rightDelta * fdt > maxAngle)
            rightDelta = 0;
        if (rightDelta < 0 && rightAngle + rightDelta * fdt < -maxAngle)
            rightDelta = 0;
        
        leftAngle += leftDelta * fdt;
        rightAngle += rightDelta * fdt;

        leftAngle = springG * leftAngle;
        rightAngle = springG * rightAngle;

        longG = 0f;
        latG = 0f;
    }

    public void SetLeftAngle(float angle)
    {
        leftAngle = angle;
    }

    public void SetRightAngle(float angle)
    {
        rightAngle = angle;
    }

    public void SetG(float longG, float latG)
    {
        this.longG = longG;
        this.latG = latG;
    }
}
