using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RandomTarget : MonoBehaviour
{
	public List<GameObject> targets;
	private int counter = 0;
	private Vector3 position = new Vector3(4.5f, 0.8f, 0f);
	private quaternion rotation = new quaternion(45f, 0f, 0f, 0f);
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		counter++;
		if (counter%100 == 0)
		{
			GameObject go;
			go = Instantiate(targets[UnityEngine.Random.Range(0, targets.Count)]);
			go.GetComponent<Transform>().position = position;
			go.transform.parent = GameObject.FindGameObjectWithTag("Object").transform;
			targets.Add(go);
		}

		foreach (var item in targets)
		{
			item.GetComponent<Transform>().position -= new Vector3(0.05f, 0, 0);

			//not working?????
			//if (item.gameObject.transform.position.y < 10)
			//{
			//	Destroy(item);
			//}
		}
	}
}
