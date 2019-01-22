using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJointManager : MonoBehaviour {

    public RobotJoint joint1;
    public RobotJoint joint2;
    public RobotJoint joint3;
    public RobotJoint joint4;
    public RobotJoint joint5;
    public RobotJoint joint6;
    public RobotJoint joint7;

    private List<RobotJoint> jointList;
    private IEnumerator coroutine;

    void Start () {
        //StartCoroutine(JointMovements());
        coroutine = MoveJoints();
        jointList = new List<RobotJoint>() { joint1, joint2, joint3, joint4, joint5, joint6, joint7 };
    }

    public void StartJointMovement()
    {
        StartCoroutine(coroutine);
    }

    private IEnumerator MoveJoints()
    {
        joint1.MoveJoint(100f);
        yield return new WaitForSecondsRealtime(3f);
        joint2.MoveJoint(50f);
        yield return new WaitForSecondsRealtime(3f);
        joint3.MoveJoint(100f);
    }
	
    public void StopJointMovement()
    {
        StopCoroutine(coroutine);
        ResetJoints();
    }

    public void ResetJoints()
    {
        foreach (RobotJoint joint in jointList)
        {
            joint.Reset();
        }
    }

}
