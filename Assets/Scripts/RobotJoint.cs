using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJoint : MonoBehaviour {

    public int jointLimit = 170;
    public BoundsExtensions.Axis rotationAxis = BoundsExtensions.Axis.Y;
    public float jointSpeed = 1f;

    private Transform joint;
    private float currentDegree;
    private float targetDegree;

    private bool moving = false;
    private bool movePositive = true;

	void Start () {

        joint = this.transform;
    }
	
	void Update () {

        if (moving)
        {
            if (currentDegree <= targetDegree - 1f || currentDegree >= targetDegree + 1f)   // compare to a range, or it might miss the targetDegree
            {
                if (movePositive && currentDegree < jointLimit)
                {
                    currentDegree += jointSpeed;
                    RotateJoint(jointSpeed);
                }
                else if (!movePositive && currentDegree > (jointLimit * -1f))
                {
                    currentDegree -= jointSpeed;
                    RotateJoint(-jointSpeed);
                }
                else
                {
                    print(this.name + " Joint Limit Exeeded");
                    moving = false;
                }
            }
            // give the angle a clear value
            else
            {
                currentDegree = targetDegree;
                if (rotationAxis == BoundsExtensions.Axis.Y)
                {
                    joint.localEulerAngles = new Vector3(0, currentDegree, 0);
                }
                else if (rotationAxis == BoundsExtensions.Axis.Z)
                {
                    joint.localEulerAngles = new Vector3(0, 0, currentDegree);
                }

                moving = false;
            }        
        }		
	}

    /// <summary>
    /// triggers joint movement
    /// </summary>
    /// <param name="degree">joints target degree</param>
    public void MoveJoint(float degree)
    {
        moving = true;
        targetDegree = degree;

        if (degree > currentDegree)
            movePositive = true;
        else
            movePositive = false;
    }

    /// <summary>
    /// rotate joint around its axis
    /// </summary>
    /// <param name="value">amount to rotate</param>
    private void RotateJoint(float value)
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

    /// <summary>
    /// reset angle to 0
    /// </summary>
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
