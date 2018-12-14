using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	public List<GameObject> visibleTargets = new List<GameObject>();

	[Range(0,20)] public float viewRadius;
	[Range(0,360)] public float viewAngle;
	public LayerMask targetMask;

	// Use this for initialization
	void Start () {
		
		StartCoroutine("FindTargetsWithDelay", .3f);
	}

	IEnumerator FindTargetsWithDelay(float delay) {

		while(true) {

			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}


	void FindVisibleTargets() {

		visibleTargets.Clear();

		Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

		foreach(Collider2D targetCollider in targetsInRange) {

			GameObject targetObject = targetCollider.gameObject;

			if(targetObject == gameObject)
				continue;
			
			if(!IsObjectVisible(targetObject))
				continue;

			visibleTargets.Add(targetObject);
		}
	}

	private bool IsObjectVisible(GameObject targetObject) {

		Vector2 directionToTarget = (targetObject.transform.position - transform.position).normalized;
		float directionAngle = Vector2.Angle(transform.up, directionToTarget);

		if(directionAngle > viewAngle / 2f)
			return false;

		float distance = Vector2.Distance(transform.position, targetObject.transform.position);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionToTarget, distance);

		foreach(RaycastHit2D hit in hits) {

			if(hit.transform.gameObject == gameObject) {

				continue;
			}
				
			if(hit.transform.gameObject != targetObject) {
				
				return false;
			}	
		}

		return true;
	}

	public bool VisibleObjectsContains(GameObject obj) {

		return visibleTargets.Contains(obj);
	}
	private void OnDrawGizmosSelected() {
		
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, viewRadius);
	}
}
