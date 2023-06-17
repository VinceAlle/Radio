using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Radio
{
	public class Click : MonoBehaviour
	{
        public void ClickEvent()
        {
			Vector3 pos = Mouse.current.position.ReadValue();
			
			Ray ray = Camera.main.ScreenPointToRay(pos);
			RaycastHit info;
			//Debug.Log(pos);
            Debug.DrawRay(ray.origin,ray.direction, Color.red,10);

            if (Physics.Raycast(ray, out info, 10f))
            {
				IClick click = info.collider.gameObject.GetComponent<IClick>();
                if (click != null)
                {
                    //Debug.Log(info.collider.gameObject.name);
                    ValueManager.Instance.curClick = click;
                }
            }
        }


	}
}