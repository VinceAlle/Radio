using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Radio
{
    /// <summary>
    /// ��ӡֽ
    /// </summary>
	public class AddSection : MonoBehaviour
	{
		public GameObject sectionPrefab;
        float LinDt = 0.54f;
        int index = 0;
        float TopY = 0.54f;
        private int LineWords=22;

        private void Start()
        {
            //PageSection("");
        }

        /// <summary>
        /// ����Section����
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool PushSection(Section s)
        {
            GameObject gameObject = GameObject.Instantiate(sectionPrefab, this.transform);
            index += gameObject.GetComponent<SectionSelect>().SetContent(s.SectionPart[0].speaker, s.SectionPart[0].content,s.playerWord, index);
            if (index >= LineWords)
                return false;
            return true;
        }
    }
}