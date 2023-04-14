using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class JumperAgent : Agent
{
	public float jumpForce = 0.1f;
	private Rigidbody rigidbody = null;
	private bool canJump = true;

	public override void Initialize()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (transform.position.y < 0.5f)
		{
			AddReward(-1);
			ResetGame();
		}
	}

	public override void OnActionReceived(ActionBuffers actionBuffers)
	{
		if (Mathf.FloorToInt(actionBuffers.DiscreteActions[0]) == 1)
			Jump();
	}

	public override void OnEpisodeBegin()
	{
		ResetGame();
	}

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		var actions = actionsOut.DiscreteActions;
		actions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		sensor.AddObservation(transform.localPosition);
	}

	private void Jump()
	{
		if (canJump)
		{
			AddReward(0.1f);
			rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			canJump = false;
		}
	}

	public void ResetGame()
	{
		gameObject.transform.position = new Vector3(0, 0.5f, 0);
		foreach (Transform child in GameObject.FindGameObjectWithTag("Object").transform)
		{
			Destroy(child.gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Floor")
		{
			AddReward(0.01f);
			canJump = true;
		}
		else if (collision.transform.tag == "Obstacle")
		{
			AddReward(-1.0f);
			EndEpisode();
		}
	}
}