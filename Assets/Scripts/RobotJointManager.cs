using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls joint movement
/// </summary>
public class RobotJointManager : MonoBehaviour {

    public RobotJoint joint1;
    public RobotJoint joint2;
    public RobotJoint joint3;
    public RobotJoint joint4;
    public RobotJoint joint5;
    public RobotJoint joint6;
    public RobotJoint joint7;

    private List<RobotJoint> jointList;
    private Coroutine coroutine;

    private bool jointAnimationIsPlaying = false;

    void Start () {

        jointList = new List<RobotJoint>() { joint1, joint2, joint3, joint4, joint5, joint6, joint7 };
    }

    public void StartJointMovement()
    {
        coroutine = StartCoroutine(MoveJoints());
        jointAnimationIsPlaying = true;
    }

    /// <summary>
    /// use this method to create custom animations
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveJoints()
    {
        joint1.MoveJoint(160f);
        yield return new WaitForSecondsRealtime(1.7f);
        joint2.MoveJoint(-30f);
        yield return new WaitForSecondsRealtime(1f);
        joint3.MoveJoint(-30f);
        yield return new WaitForSecondsRealtime(0.6f);
        joint4.MoveJoint(-85f);
        yield return new WaitForSecondsRealtime(1f);
        joint5.MoveJoint(70f);
        yield return new WaitForSecondsRealtime(1f);
        joint6.MoveJoint(90f);
        yield return new WaitForSecondsRealtime(1f);
        joint7.MoveJoint(150f);
        yield return new WaitForSecondsRealtime(1f);

        // go back to origin
        joint1.MoveJoint(0f);
        joint2.MoveJoint(0f);
        joint3.MoveJoint(0f);
        joint4.MoveJoint(0f);
        joint5.MoveJoint(0f);
        joint6.MoveJoint(0f);
        joint7.MoveJoint(0f);

        yield return new WaitForSecondsRealtime(1f);

        // start animation again
        if (jointAnimationIsPlaying)
        {
            coroutine = StartCoroutine(MoveJoints());
        }

    }
	
    public void StopJointMovement()
    {
        StopCoroutine(coroutine);
        ResetJoints();

        jointAnimationIsPlaying = false;
    }

    public void ResetJoints()
    {
        foreach (RobotJoint joint in jointList)
        {
            joint.Reset();
        }
    }

}
