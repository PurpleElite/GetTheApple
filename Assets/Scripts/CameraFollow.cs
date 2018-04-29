using UnityEngine;
using System.Collections;
using System;

public class CameraFollow : MonoBehaviour 
{
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.


	private Transform player;       // Reference to the player's transform.


    void Awake()
    {
        // Setting up the reference.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

	bool CheckYMargin()
	{
        // Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}


	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
		// By default the target y coordinates of the camera are it's current y coordinates.
		float targetY = transform.position.y;

		// If the player has moved beyond the y margin...
		if(CheckYMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

		// The target y coordinate should not be larger than the maximum or smaller than the minimum.
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(transform.position.x, (float)Math.Round(targetY, 2), transform.position.z);
	}
}
