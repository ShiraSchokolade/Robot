using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotSceneManager : MonoBehaviour {

    public GameObject robot;

    private Animator animator;
    private RobotJointManager jointManager;

    // dont toggle this at the moment, use the two diffrent scenes instead. 
    // because the robots are not using the same maya model (the one with the animation)
    public bool useCustomAnimation = true;

    private bool animationIsPlaying = false;
    private bool hasBeenPlaced = false;

    private TapToPlace tapToPlace;

	void Start () {

        jointManager = robot.GetComponent<RobotJointManager>();

        if (!useCustomAnimation)
        {
            if (robot.GetComponent<Animator>())
            {
                animator = robot.GetComponent<Animator>();
                animator.speed = 0f;
            }
            else
            {
                Debug.Log("Robot misses Animator Component");
            }
        }

        if (robot.GetComponent<TapToPlace>())
        {
            tapToPlace = robot.GetComponent<TapToPlace>();
        }
        else
        {
            Debug.Log("Robot misses TapToPlace Component");
        }
    }
	

	void Update () {

        // placement mode -> stop animation
        if (tapToPlace.IsBeingPlaced && hasBeenPlaced)
        {
            hasBeenPlaced = false;

            if (animationIsPlaying)
            {
                if (!useCustomAnimation)
                    animator.speed = 0f;
                else
                    jointManager.StopJointMovement();
                animationIsPlaying = false;
            }
        }
        // static mode -> play animation
        else if(!tapToPlace.IsBeingPlaced && !hasBeenPlaced)
        {
            if (!useCustomAnimation)
                animator.speed = 1f;
            else
                jointManager.StartJointMovement();
            animationIsPlaying = true;
            hasBeenPlaced = true;
        }		
	}

    public void ChangeAnimationState()
    {
        if (!animationIsPlaying)
        {
            if (!useCustomAnimation)
                animator.speed = 1f;
            else
                jointManager.StartJointMovement();
            animationIsPlaying = true;
        }
        else
        {
            if (!useCustomAnimation)
                animator.speed = 0f;
            else
                jointManager.StopJointMovement();
            animationIsPlaying = false;
        }
    }

    /// <summary>
    /// stops robot movement
    /// </summary>
    public void ResetRobot()
    {
        if (!useCustomAnimation)
            animator.speed = 0;
        else
            jointManager.StopJointMovement();
    }
}
