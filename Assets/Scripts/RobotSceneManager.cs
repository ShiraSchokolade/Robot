using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSceneManager : MonoBehaviour {

    public GameObject robot;

    private static string standardAnimation = "SmoothAnim";
    private static string noAnimation = "Static";

    private Animator animator;
    private bool animationIsPlaying = false;
    private bool hasBeenPlaced = false;

    private TapToPlace tapToPlace;

	void Start () {

        if (robot.GetComponent<Animator>())
        {
            animator = robot.GetComponent<Animator>();
            animator.speed = 0f;
        }
        else
        {
            Debug.Log("Robot misses Animator Component");
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

        if (tapToPlace.IsBeingPlaced && hasBeenPlaced)
        {
            hasBeenPlaced = false;

            if (animationIsPlaying)
            {
                animator.speed = 0f;
                animationIsPlaying = false;
            }
        }
        else if(!tapToPlace.IsBeingPlaced && !hasBeenPlaced)
        {
            animator.speed = 1f;
            animationIsPlaying = true;
            hasBeenPlaced = true;
        }		
	}

    public void ChangeAnimationState()
    {
        if (!animationIsPlaying)
        {
            animator.speed = 1f;
            animationIsPlaying = true;
        }
        else
        {
            animator.speed = 0f;
            animationIsPlaying = false;
        }
    }

    public void ResetRobot()
    {
        animator.speed = 0;

    }
}
