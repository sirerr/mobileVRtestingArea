using UnityEngine;
using System.Collections;

public class enemyattackaction : MonoBehaviour {

	public float speed =5;
	Rigidbody rbody;


	public virtual void OnCollisionEnter(Collision col)
	{
		Destroy(gameObject);

	}

	public virtual void OnEnable()
	{
		rbody = GetComponent<Rigidbody>();
		rbody.velocity = speed * transform.forward;
	}
}
