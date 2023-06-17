using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Radio
{
	public interface IClick 
	{
		void EnterLeftClick();
		void ExitLeftClick();

		void EnterRightClick();
		void ExitRightClick();
	}
}