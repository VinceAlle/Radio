using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Radio
{
	public class Dialog : MonoBehaviour
	{
		public TMPro.TextMeshProUGUI uGUI;

        private string fileNmae = "SelfLog/";
        int index = 1;
        private void Start()
        {
            NextLog();
        }
        SelfLog sl;
        int sl_i;

        public SetSection setSections;

        public void NextLog()
        {
            try
            {
                Debug.Log("FileName:" + (fileNmae + index));
                sl = Resources.Load<SelfLog>(fileNmae + index++);
                Debug.Log("Count:" + sl.LogContent.Count);
                sl_i = 0;
                ValueManager.Instance.LeftClickEvent.AddListener(Next);
                ValueManager.Instance.SetDontChange(true);
                Next();
            }
            catch
            {

                this.gameObject.SetActive(false);
            }
        }

        public void Next()
        {
            try
            {
                if (sl.LogContent.Count <= sl_i)
                {
                    ValueManager.Instance.SetDontChange(false);
                    this.gameObject.SetActive(false);
                    ValueManager.Instance.LeftClickEvent.RemoveListener(Next);
                    return;
                }
                //Debug.Log(sl.LogContent[sl_i]);
                uGUI.text = sl.LogContent[sl_i++];
            }
            catch
            {
                Debug.Log("Self is End");
            }
            
        }
        private float curTime = 0f;
        private void FixedUpdate()
        {
            if (strQuene.Count > 0)
            {
                if (curTime < 0.1f)
                {
                    uGUI.text = strQuene.Peek().Key;
                }
                else if (curTime > strQuene.Peek().Value)
                {
                    strQuene.Dequeue();
                    curTime = 0f;
                    Debug.Log("RecordCtl:" + RecordSection.Instance.sectionsBuf.Count);
                    if (setSections.sections.Count >= 3)
                    {
                        NextLog();
                    }
                    else if (strQuene.Count == 0)
                        this.gameObject.SetActive(false);
                }
                curTime += Time.fixedDeltaTime;
            }

        }

        Queue<KeyValuePair<string,float>> strQuene=new Queue<KeyValuePair<string, float>>();

        public void ShowString(string str,float time)
        {
            strQuene.Enqueue(new KeyValuePair<string, float>(str, time));
        }
    }
}