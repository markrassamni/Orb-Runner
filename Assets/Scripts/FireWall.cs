using System.Collections;
using UnityEngine;

public class FireWall : MonoBehaviour {
	
	[SerializeField] private Vector3 hitForce;
	[SerializeField] private float moveDelay;
	[SerializeField] private bool sideFire;
	private const float despawn_time = 15f;

	public Vector3 HitForce{
		get{ return hitForce; }
	}

	public float MoveDelay{
		get{ return moveDelay; }
	}
	
	void Update(){
		if (sideFire){
			transform.Translate(Vector3.left * Time.deltaTime * 8.5f);
		}
	}

	public IEnumerator Despawn(){
		yield return new WaitForSeconds(despawn_time);
		Destroy(gameObject);
	}
}
