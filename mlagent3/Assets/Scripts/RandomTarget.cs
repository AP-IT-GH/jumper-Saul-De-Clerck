using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTarget : MonoBehaviour
{
	public GameObject targetPrefab;
	public Transform parentTransform;
	public float spawnInterval = 2f;
	public float destroyXPos = -5f;
	private float timer = 0f;

	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= spawnInterval)
		{
			GameObject newTarget = Instantiate(targetPrefab, new Vector3(5, 0.5f, 0f), Quaternion.identity);
			newTarget.transform.Rotate(new Vector3(90f, 0f, 0f));
			newTarget.transform.parent = parentTransform;
			timer = 0f;
		}

		foreach (Transform child in parentTransform)
		{
			child.position -= new Vector3(5f, 0f, 0f) * Time.deltaTime; 
			if (child.position.x < destroyXPos)
			{
				Destroy(child.gameObject);
			}
		}
	}
}
