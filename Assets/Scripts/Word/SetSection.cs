using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Radio
{
    /// <summary>
    /// һ�ż�¼ҳ Record
    /// </summary>
    public class SetSection : MonoBehaviour
    {
        public float topParam = 1.08f;
        public float bottomParam = -11.88f;
        private float dtParam = 0.54f;
        public List<SectionSelect> sections=new List<SectionSelect>();

        int index=1;
        private void Start()
        {
            //sections = GetComponentsInChildren<SectionSelect>();
            //for(int i = 0; i < sections.Length; i++)
            //{
            //    index += sections[i].Line;
            //}
            //Debug.Log("Index:" + index);
        }

        private bool isOnPage(SectionSelect s)
        {
            foreach(var i in sections)
            {
                if (s == i)
                    return true;
            }
            return false;
        }

        public void AddToPage(SectionSelect s)
        {
            if (sections.Count>0&& isOnPage(s))
            {
                s.gameObject.transform.parent = this.gameObject.transform;
                int i_t = 1;
                for (int i = 0; i < sections.Count && s != sections[i]; i++, i_t += sections[i].Line) ;
                s.gameObject.transform.localPosition = new Vector3(0, topParam - i_t * dtParam, 0);
            }
            else
            {
                //Debug.Log("Right:" + s.Line);
                sections.Add(s);
                s.gameObject.transform.parent = this.gameObject.transform;
                s.gameObject.transform.localPosition = new Vector3(0, topParam - index * dtParam, 0);
                index += s.Line;
                
            }
        }

        public void ResetSection(SectionSelect s)
        {
            int ind = sections.IndexOf(s);
            int i_t = 1;
            for(int i = 0; i < sections.Count; i++)
            {
                if (i > ind)
                {
                    sections[i].gameObject.transform.localPosition = new Vector3(0, topParam - i_t * dtParam, 0);
                }
                if(i!=ind)
                    i_t += sections[i].Line;
            }
            index = i_t;
            sections.RemoveAt(ind);
            
        }



    }
}