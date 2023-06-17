using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Radio
{
	[System.Serializable]
	public struct OneTalk
    {
		public string speaker;
		public string content;
    }

	[CreateAssetMenu(fileName = "New Section", menuName = "ScriptableObject/Section")]
	public class Section : ScriptableObject
	{
		public float Rate;
		public string FileRoute;
		public float startRadio;
		public float endRadio;

		public string playerWord;
		[SerializeField]
		public List<OneTalk> SectionPart;
	}
}