using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Radio.ContactItem
{
	public class Knod : MonoBehaviour,IClick
	{
		public float rotaValue;

        Vector3 rota = new Vector3(0, 0, -90);
        InputAction Knob;
        bool OnClick=false;
        HighlightableObject highlightable;

        public TMPro.TextMeshProUGUI FM;
        public float FMValue;

        public string noiseFile = "Section/noise";
        public AudioClip noise;
        
        public float subNoise = 5f;
        public AudioSource audioPlayer;

        private void Start()
        {
            Knob = ValueManager.Instance.inputs.Player.KnobRota;
            highlightable = GetComponent<HighlightableObject>();
            noise = Resources.Load<AudioClip>(noiseFile);
            //audioPlayer = GetComponent<AudioSource>();
            //foreach(var i in ValueManager.Instance.setions)
            //{
            //    tar.Add(i.Rate);
            //}
        }
        public void KnodRotation(float level)
        {
            if (level != 0)
            {
                level = level / 120;
                rota += new Vector3(level * rotaValue, 0, 0);
                float curV = float.Parse(FM.text.Substring(3));
                //Debug.Log("curV:" + curV);
                curV += level * FMValue;
                if (inRighgtRate(curV))
                {
                    //Debug.Log("inRighgtRate");
                    if(audioPlayer.clip!=noise)
                        audioPlayer.clip = noise;
                    if (!audioPlayer.isPlaying)
                    {
                        //Debug.Log("Audio Play!");
                        audioPlayer.Play();
                    }
                }
                else
                {
                    //Debug.Log("Audio Stop!");
                    audioPlayer.Stop();
                }
                FM.text = "FM£º"+curV.ToString();
                this.transform.rotation = Quaternion.Euler(rota);
            }
        }

        bool inRighgtRate(float rate)
        {
            foreach (var i in ValueManager.Instance.setions)
            {
                if(Mathf.Abs(i.Rate- rate)< subNoise)
                {
                    return true;
                }
            }
            return false;
        }
        
        private void FixedUpdate()
        {
            if (OnClick)
            {
                float rota = Knob.ReadValue<float>();
                if (rota != 0)
                {
                    KnodRotation(rota);
                }
            }
        }

        public void EnterLeftClick()
        {
            OnClick=true;
            highlightable.ConstantOn();
        }

        public void ExitLeftClick()
        {
            OnClick = false;
            highlightable.ConstantOff();
            if (audioPlayer.isPlaying)
            {
                audioPlayer.Stop();
            }
        }

        public void EnterRightClick()
        {
            
        }

        public void ExitRightClick()
        {
            
        }
    }
}