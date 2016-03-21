using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[RequireComponent(typeof(IsoObject))]
[RequireComponent(typeof(IsoCollider))]
public class Draggable : MonoBehaviour
{
	IsoObject isoObject;
		
	void Awake() {
		isoObject = this.GetOrAddComponent<IsoObject>();
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButton(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			//avoids raycasting to ourself
			disableColldiers();

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
			{
				//did we hit a IsoObject
				var hitIsoObject = hit.collider.GetComponent<IsoObject>();
				if (hitIsoObject != null)
				{
					//calc the new height were we want to put our object at
					var newHeight = hitIsoObject.Position.z + ((1.1f * hitIsoObject.Size.z) / 2f) + (1.1f *isoObject.Size.z ) / 2;

					//calc the final position
					isoObject.Position = Isometric.screenToIsoPoint(Input.mousePosition, newHeight);
				}
			}
			else
			{
				//we didn't hit anything, keep the current height
				isoObject.Position = Isometric.screenToIsoPoint(Input.mousePosition, isoObject.Position.z);
			}
			//turn colliders back on
			enableColldiers();
		}
	}

	/// <summary>
	/// Disables all colliders attached to this gameObject
	/// </summary>
	private void disableColldiers()
	{
		//TODO avoid GetComponents call
		foreach (Collider col in GetComponents<Collider>())
			col.enabled = false;
	}

	/// <summary>
	/// Enables all colliders attached to this gameObject
	/// </summary>
	private void enableColldiers()
	{
		//TODO avoid GetComponents call
		foreach (Collider col in GetComponents<Collider>())
			col.enabled = true;
	}

}
	

	