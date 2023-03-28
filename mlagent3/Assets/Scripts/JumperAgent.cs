using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class JumperAgent : Agent
{
	public override void OnEpisodeBegin()
	{
		this.transform.localPosition = new Vector3(0, 0.5f, 0);
		this.transform.localRotation = new Quaternion(0, 0, 0, 0);
		this.transform.localRotation = Quaternion.identity;
	}
	public override void CollectObservations(VectorSensor sensor)
	{
		// Target en Agent posities
		sensor.AddObservation(this.transform.localPosition);

	}
	public float speedMultiplier = 0.1f;
	public override void OnActionReceived(ActionBuffers actionBuffers)
	{
		// Acties, size = 2
		Vector3 controlSignal = Vector3.zero;
		controlSignal.y = actionBuffers.ContinuousActions[0];

		//transform.localPosition += new Vector3(controlSignal.x, jump, controlSignal.z) * Time.deltaTime * speedMultiplier;
		transform.Translate(controlSignal * speedMultiplier);

		// Van het platform gevallen?
		if (this.transform.localPosition.y < 0)
		{
			SetReward(-0.5f);
			EndEpisode();
		}

	}
	public override void Heuristic(in ActionBuffers actionsOut)
	{
		var continuousActionsOut = actionsOut.ContinuousActions;
		continuousActionsOut[0] = Input.GetAxis("Jump");
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Obstacle")
		{
			//Destroy(GameObject.FindGameObjectWithTag("Obstacle").transform.GetChild(0).gameObject);
			EndEpisode();
		}
	}
}
