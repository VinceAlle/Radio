using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Radio
{
	public class ValueManager : MonoBehaviour
	{
        private static ValueManager _instance;
        public static ValueManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<ValueManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject(typeof(ValueManager) + "_Singleton");
                        _instance = obj.AddComponent<ValueManager>();
                        GameObject.DontDestroyOnLoad(obj);
                    }
                }
                return _instance;
            }
        }

        public InputManager inputs;
        public UnityEvent LeftClickEvent;
        public UnityEvent RightClickEvent;
        public int LineWords = 22;

        public Dialog dialog;

        public TMPro.TextMeshProUGUI FM;

        public List<Section> setions;
        private string behindSection = "Section/Day1/";

        private bool DontChange = false;
        private IClick _cur;
        public IClick curClick
        {
            set
            {
                if (!DontChange)
                {
                    if (_cur != null)
                    {
                        _cur.ExitLeftClick();
                        _cur.ExitRightClick();
                    }
                    _cur = value;
                    _cur.EnterLeftClick();
                    _cur.EnterRightClick();
                }
            }
            get
            {
                if(_cur!=null)
                    return _cur;
                return null;
            }
        }

        public int sectionCtl = 0;


        public void GetSection(out Section section, out AudioClip clip)
        {
            float rate = float.Parse(FM.text.Substring(3));
            foreach (var i in setions)
            {
                if (Mathf.Abs(i.Rate - rate) < 5f)
                {
                    section = i;
                    clip = Resources.Load<AudioClip>(behindSection + i.FileRoute);
                    //section.SectionPart[0].content.Replace("£¬", ",");
                    //section.SectionPart[0].content.Replace("¡£", ".");
                    //section.SectionPart[0].content.Replace("£¿", "?");
                    //section.SectionPart[0].content.Replace("£¡", "!");
                    sectionCtl++;
                    return;
                }
            }
            section = null;
            clip = null;
        }


        private void Awake()
        {
            inputs = new InputManager();
            inputs.Enable();
            inputs.Player.Choose.started += (InputAction.CallbackContext obj) =>
            {
                LeftClickEvent.Invoke();
                //Debug.Log("Left Mouse Button");
            };
            inputs.Player.Cancel.started += (InputAction.CallbackContext obj) =>
              {
                  RightClickEvent.Invoke();
                  //Debug.Log("Right Mouse Button");
              };

            setions = new List<Section>();

            for(int i = 1; i < 10; i++)
            {
                var s = Resources.Load<Section>(behindSection + i.ToString());
                if (s != null)
                {
                    setions.Add(s);
                }
                else
                    break;
            }
            Debug.Log("setions:" + setions.Count);

        }

        public void SetDontChange(bool change)
        {
            DontChange = change;
        }

        public void NextLog()
        {
            dialog.gameObject.SetActive(true);
            dialog.NextLog();
        }
    }
}