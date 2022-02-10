using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
	private Vector3 startPosition;

    private void Start()
    {
		startPosition = transform.position;
	}

    // Before rendering each frame..
    void Update()
	{
		// Rotate the game object that this script is attached to by 15 in the X axis,
		// 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
		// rather than per frame.
		transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
		transform.position = startPosition + new Vector3(0.0f, 0.0f, Mathf.Sin(Time.time)/1.5f);
	}
}