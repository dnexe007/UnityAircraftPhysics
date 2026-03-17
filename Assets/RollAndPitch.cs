using UnityEngine;

public static class RollAndPitch
{
    public struct RollAndPitchData
    {
        public float roll;
        public float pitch;
        public Vector3 rightHorizontalVector;

        public RollAndPitchData(float roll, float pitch,  Vector3 rightHorizontalVector)
        {
            this.roll = roll;
            this.pitch = pitch;
            this.rightHorizontalVector = rightHorizontalVector;
        }
    }

    public static RollAndPitchData CalculateRollAndPitch(Transform transform)
    {
        Vector3 rightHorizontalVector = Vector3.Cross(Vector3.up, transform.forward).normalized;
        if (rightHorizontalVector == Vector3.zero)
            rightHorizontalVector = transform.right;

        float roll = Vector3.SignedAngle(transform.right, rightHorizontalVector, transform.forward);

        Vector3 fdProject = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        if (fdProject == Vector3.zero)
            fdProject = Vector3.forward;
        float pitch = Vector3.Angle(fdProject, transform.forward) * Mathf.Sign(transform.forward.y);


        return new RollAndPitchData(
            roll,
            pitch, 
            rightHorizontalVector
        );
    }
}
