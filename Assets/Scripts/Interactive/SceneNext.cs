using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Radio
{
    public class SceneNext : MonoBehaviour, IClick
    {
        public TMPro.TextMeshProUGUI text;
        private Section sections;
        private int index = 0;

        private AudioSource audio;
        private void Start()
        {
            
            audio = GetComponent<AudioSource>();
        }

        bool start = true;

        IEnumerator EndAudio()
        {
            yield return new WaitForSeconds(sections.startRadio);
            var talk = sections.SectionPart[0].content.Split(new char[] { '£¬', '¡£' });
            
            //Debug.Log("Talk:"+talk.Length);
            float wait = (sections.endRadio - sections.startRadio) / (float)(talk.Length-1);
            while (index < talk.Length-1)
            {
                //Debug.Log("talk index:" + talk[index]);
                text.text = talk[index];
                index++;

                yield return new WaitForSeconds(wait);

            }
            index = 0;
            text.text = "";
            if (start)
            {
                ValueManager.Instance.NextLog();
                start = false;
            }
            if (ValueManager.Instance.sectionCtl >= 3)
            {
                ValueManager.Instance.NextLog();
            }
            
        }
        
        void NextTalk()
        {
            AudioClip ac;
            ValueManager.Instance.GetSection(out sections, out ac);
            if (sections!=null) {
                if (ac != audio.clip&& !audio.isPlaying)
                {
                    audio.clip = ac;
                    Debug.Log("Scene Audio Play");
                    audio.Play();
                    StartCoroutine(EndAudio());
                    RecordSection.Instance.PushInSections(sections);
                }
                
            }

        }
        public void EnterLeftClick()
        {
            
            NextTalk();
        }

        public void EnterRightClick()
        {
            
        }

        public void ExitLeftClick()
        {
            
        }

        public void ExitRightClick()
        {
            
        }
    }
}