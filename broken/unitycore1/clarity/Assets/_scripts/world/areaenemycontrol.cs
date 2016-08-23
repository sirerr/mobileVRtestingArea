using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class areaenemycontrol : MonoBehaviour {

	//poolarea
	public GameObject poolareaobj;
	public List <Transform> poolareas = new List<Transform>();
	public int poolareacount = 5;
	private float randomXmin = -300;
	private float randomXmax = 300;
	private float randomymin = -300;
	private float randomymax = 210;
	private float randomZmin = -700;
	private float randomZmax = 700;

	//enemy object
	public List <GameObject> enemylist = new List<GameObject>();
	public Transform enemyspawnpoint;
	public int enemycount = 1;
	public GameObject enemyobj;




	// Use this for initialization
	void Awake () {

		for(int i=1;i<=poolareacount;i++)
		{
			Vector3 vec3;
			vec3.x = Random.Range(randomXmin,randomXmax);
			vec3.y = Random.Range(randomymin,randomymax);
			vec3.z = Random.Range(randomZmin,randomZmax);

			GameObject enemypoolarea = Instantiate(poolareaobj,transform.position,transform.rotation) as GameObject;
			enemypoolarea.transform.SetParent(transform);
			enemypoolarea.transform.localPosition = vec3;
			poolareas.Add(enemypoolarea.transform);
			print("made and moved");
		}

		StartCoroutine(enemyreleasewait());
	}

	IEnumerator enemyreleasewait()
	{
		yield return new WaitForSeconds(2f);
		releaseenemies();
	}

	private void releaseenemies()
	{
		//test code
		GameObject tester = Instantiate(enemyobj,enemyspawnpoint.position,Quaternion.identity) as GameObject;
		tester.GetComponent<enemystats>().currentarea = transform;
		for(int i =0;i<poolareas.Count;i++)
		{
			tester.GetComponent<enemyAI>().pooltargets.Add(poolareas[i]);
		}

		enemylist.Add(tester);
		//test code


	}


	
	// Update is called once per frame
	void Update () {
	
	}



}
