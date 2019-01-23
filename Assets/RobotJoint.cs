using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJoint : MonoBehaviour {

    public int jointLimit = 170;
    public BoundsExtensions.Axis rotationAxis = BoundsExtensions.Axis.Y;
    public float jointSpeed = 1f;

    private Transform joint;
    private float rotationStartValue;
    private float currentDegree;
    private float targetDegree;

    private bool moving = false;
    private bool movePositive = true;

	void Start () {

        joint = this.transform;

        if (rotationAxis == BoundsExtensions.Axis.Y)
            rotationStartValue = joint.eulerAngles.y;
        else if (rotationAxis == BoundsExtensions.Axis.Z)
            rotationStartValue = joint.eulerAngles.z;
    }
	
	void Update () {

        if (moving)
        {
            if (currentDegree <= targetDegree - 1f || currentDegree >= targetDegree + 1f)
            {
                if (movePositive && currentDegree < jointLimit)
                {
                    currentDegree += jointSpeed;
                    RotateJointTo(jointSpeed);
                }
                else if (!movePositive && currentDegree > (jointLimit * -1f))
                {
                    currentDegree -= jointSpeed;
                    RotateJointTo(-jointSpeed);
                }
                else
                {
                    moving = false;
                }
            }
            else
            {
                moving = false;
            }        
        }		
	}

    public void MoveJoint(float degree)
    {
        moving = true;
        targetDegree = degree;

        if (degree > currentDegree)
            movePositive = true;
        else
            movePositive = false;
    }

    private void RotateJointTo(float value)
    {
        float axisValue;

        if (rotationAxis == BoundsExtensions.Axis.Y)
        {
            axisValue = value;
            joint.Rotate(new Vector3(0, axisValue, 0));
        }
        else if (rotationAxis == BoundsExtensions.Axis.Z)
        {
            axisValue = value;
            joint.Rotate(new Vector3(0,0, axisValue));
        }
    }

    public void Reset()
    {
        
        moving = false;

        if (rotationAxis == BoundsExtensions.Axis.Y)
            joint.eulerAngles = new Vector3(joint.localEulerAngles.x, 0, joint.localEulerAngles.z);
        else if (rotationAxis == BoundsExtensions.Axis.Z)
        {
            joint.eulerAngles = new Vector3(joint.localEulerAngles.x, joint.localEulerAngles.y, 0);
        }
        currentDegree = 0;
    }
}
