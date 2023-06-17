using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Radio
{
	[CreateAssetMenu(fileName = "New SelfLog", menuName = "ScriptableObject/SelfLog")]
	public class SelfLog : ScriptableObject
	{
		public List<string> LogContent;
	}
}