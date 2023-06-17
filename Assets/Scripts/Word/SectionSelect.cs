using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace Radio
{
    /// <summary>
    /// 一段文本
    /// </summary>
    public class SectionSelect : MonoBehaviour, IClick
    {
        private TextMeshProUGUI[] meshPros;

        private bool chase = false;
        private float left = -110f, right = -81.8f-5.25f,lrsub= -76.55f+110f;
        private float top = 26.36f, bottom = 7.51f, tbsub = 26.36f - 7.51f;
        private Vector2 parentTrans=new Vector2(8.07694f,15.38797f);

        private int lineword = 20;
        public int Line=0;
        private float lineHeight = 0.54f;

        private Transform resetParent;
        private Vector3 resetPos;

        private bool RightPage = false;
        private float subPage = 15.79f;

        public Dialog dialog;


        private void Start()
        {
            resetParent = transform.parent;
            resetPos = transform.localPosition;

            meshPros = GetComponentsInChildren<TextMeshProUGUI>();
            dialog = ValueManager.Instance.dialog;
//            int l = meshPros[0].text.Length / lineword;
//            Line = meshPros[0].text.Length % lineword == 0 ? l : l + 1;
        }
        public void EnterLeftClick()
        {
            ValueManager.Instance.RightClickEvent.RemoveListener(RecordSection.Instance.CloseRecord);

            chase = !chase;
            if (!chase)
            {
                if (!dialog.gameObject.activeSelf)
                {
                    dialog.gameObject.SetActive(true);
                }
                if(PlayerWords!=null)
                    dialog.GetComponent<Dialog>().ShowString(PlayerWords, 2f);
                else
                    dialog.GetComponent<Dialog>().ShowString(meshPros[0].text, 2f);

                Ray ray = new Ray(this.transform.position,Vector3.left);
                RaycastHit info;
                //Debug.Log(pos);
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 2);

                if (Physics.Raycast(ray, out info, 10f))
                {
                    //Debug.Log("RayCollider:"+info.collider.gameObject.name);
                    SetSection click = info.collider.gameObject.GetComponent<SetSection>();
                    if (click != null)
                    {
                        click.AddToPage(this);
                        RightPage = true;
                    }
                    else
                    {
                        chase = !chase;
                    }
                }
                else
                    chase = !chase;
            }
            
        }

        public void ExitLeftClick()
        {
            ValueManager.Instance.RightClickEvent.RemoveListener(RecordSection.Instance.CloseRecord);
        }

        public string PlayerWords=null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="speaker">对话人</param>
        /// <param name="content">内容</param>
        /// <param name="index">将该物体index位置</param>
        public int SetContent(string speaker,string content,string playerwords,int index)
        {
            //Debug.Log(content);
            //Debug.Log("index:" + index);
            int l = content.Length / lineword;
            //Debug.Log("l:" + l);
            Line = l + (content.Length % lineword == 0 ? 0 : 1);
            //Debug.Log("Line:" + Line);
            this.transform.localPosition = new Vector3(0,-index * lineHeight, 0);
            //Debug.Log("localPos:" + transform.localPosition);
            this.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            Start();
            meshPros[0].text = content;
            meshPros[1].text = speaker;

            PlayerWords = playerwords;
            
            return Line;
            
        }

        private void FixedUpdate()
        {
            if (chase)
            {
                Vector3 pos = Mouse.current.position.ReadValue();
                if (RightPage)
                {
                    this.transform.localPosition = new Vector3(pos.x * lrsub / 1920f - parentTrans.x-subPage, pos.y * tbsub / 1080f - parentTrans.y);
                }
                else
                {
                    this.transform.localPosition = new Vector3(pos.x * lrsub / 1920f - parentTrans.x, pos.y * tbsub / 1080f - parentTrans.y);
                }
            }
        }


        public void Reset()
        {
            if (!chase)
            {
                ValueManager.Instance.RightClickEvent.RemoveListener(Reset);
                ValueManager.Instance.RightClickEvent.AddListener(RecordSection.Instance.CloseRecord);
                return;
            }
                
            if (RightPage)
            {
                GetComponentInParent<SetSection>().ResetSection(this);

            }
            this.transform.parent = resetParent;
            this.transform.localPosition = resetPos;
            chase = false;
            RightPage = false;

        }


        public void EnterRightClick()
        {
            ValueManager.Instance.RightClickEvent.AddListener(Reset);
            

        }

        public void ExitRightClick()
        {
            
            
        }
    }
}