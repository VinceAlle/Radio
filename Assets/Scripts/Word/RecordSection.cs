using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Radio
{
	public class RecordSection : MonoBehaviour
	{
        private static RecordSection _instance;
        public static RecordSection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<RecordSection>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject(typeof(RecordSection) + "_Singleton");
                        _instance = obj.AddComponent<RecordSection>();
                        GameObject.DontDestroyOnLoad(obj);
                    }
                }
                return _instance;
            }
        }

        AddSection leftPage;
        SetSection rightPage;

        public GameObject LeftPage;
        public List<Section> sectionsBuf=new List<Section>();

        private GameObject childGb=null;

        

        private void Awake()
        {
            leftPage = GetComponentInChildren<AddSection>();
            rightPage = GetComponentInChildren<SetSection>();
            leftPage.gameObject.SetActive(false);
            rightPage.gameObject.SetActive(false);
        }

        public void PushInSections(Section s)
        {
            sectionsBuf.Add(s);
        }

        public void ClearSection()
        {
            sectionsBuf.Clear();
        }

        public void ShowRecord()
        {
            CreateLeftPage();
            rightPage.gameObject.SetActive(true);
        }

        private int lastBufIndex = 0;

        public void CreateLeftPage()
        {
            if (childGb == null)
            {
                childGb = GameObject.Instantiate(LeftPage, this.transform);
            }
            else
                childGb.SetActive(true);
            if (lastBufIndex != sectionsBuf.Count)
            {
                
                for(int i= lastBufIndex;i< sectionsBuf.Count;i++)
                {
                    childGb.GetComponent<AddSection>().PushSection(sectionsBuf[i]);
                }
                lastBufIndex = sectionsBuf.Count;
            }
            
        }

        bool start = true;
        public void CloseRecord()
        {
            if (start)
            {
                ValueManager.Instance.NextLog();
                start = false;
            }
            if (childGb != null)
                childGb.SetActive(false);
            rightPage.gameObject.SetActive(false);
        }

  //      public GameObject Section;
		//public float MaxBottom = -640;
		//public float OneLine = 32;
	}
}