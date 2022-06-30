using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Habby.Guild;
using Habby;
using com.tencent.imsdk.unity;
using Habby.Guild.Data;
using Habby.Guild.Module;
using Habby.Guild.Command;
using UnityEngine.UI;
using Newtonsoft.Json;
public class TestGuildSDK : MonoBehaviour
{
    public Transform testBtnPlan;
    private void Awake()
    {

        InitTestBtn();
    }

    void InitTestBtn()
    {
        {
            Button tbtn = testBtnPlan.Find("Creat").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildCreatRequestData tdata  = new GuildCreatRequestData();
                tdata.guildName = "测试公会名字";
                tdata.guildLanguage = "中文";
                tdata.guildContext = "测试公会说明文字.";
                tdata.guildLogo = "测试logo";
                tdata.guildNotice = "测试logo";
                tdata.guildRequestLevel = 0;
                tdata.guildOpenType = 1;
                GuildSDKManager.ControlModule.CreatGuild(tdata,null);
            });
        }

        {
            Button tbtn = testBtnPlan.Find("Join").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.ControlModule.JoinGuild("223334",null);
            });
        }

        {
            Button tbtn = testBtnPlan.Find("Quit").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.ControlModule.QuitGuild(null);
            });
        }

        {
            Button tbtn = testBtnPlan.Find("ApplyJoin").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.ControlModule.ApplyJoin("7bc957e0-b251-11eb-9d81-e990df96df61",null);
            });
        }

        {
            Button tbtn = testBtnPlan.Find("ResurJoin").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.ControlModule.RefuseJoin("09906900-b31b-11eb-bc19-e7a5bfc7a5a3",null);
            });
        }

        {
            Button tbtn = testBtnPlan.Find("Kick").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.ControlModule.RemovePlayer("huangyu1",null);
            });
        }

        {
            Button tbtn = testBtnPlan.Find("Head").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.ControlModule.HandOverPlayer("1233455",null);
            });
        }

        {
            Button tbtn = testBtnPlan.Find("memberlist").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.InfoModule.GetGuildMembers(GuildSDKManager.Setting.GuildID,0,20,null);
            });
        }

        {
            Button tbtn = testBtnPlan.Find("guildinfo").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.InfoModule.GetGuildInfo(GuildSDKManager.Setting.GuildID,(ad)=>{
                    Debug.Log(ad.ToJson());
                });
            });
        }

        {
            Button tbtn = testBtnPlan.Find("seachGuild").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.InfoModule.GetGuildList("",0,10,null);
            });
        }

        {
            Button tbtn = testBtnPlan.Find("updateinfo").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                UpdateGuildInfoRequest tobj = new UpdateGuildInfoRequest();
                tobj.guildName = "测试修改公会名字222";
                tobj.guildContext = "测试说明22";
                tobj.guildLogo = "测试logo22";
                tobj.guildNotice = "测试公告2";
                tobj.guildRequestLevel = 1;
                tobj.guildOpenType = 0;
                tobj.guildLanguage = "英文";

                GuildSDKManager.InfoModule.UpdateGuildInfo(tobj,null);
            });
        }

        {
            Button tbtn = testBtnPlan.Find("joinrequest").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                var tdata = new Habby.Guild.Command.CommandJoinRequestList.Data();
 
                GuildSDKManager.SendCommand((int)GuildCommand.getJoinRequestList,tdata,(pObject =>
                {
                    Debug.Log(111);
                }));
            });
        }

        {
            Button tbtn = testBtnPlan.Find("itemhelp").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.SendCommand((int)GuildCommand.getItemHelpList);
            });
        }



        {
            Button tbtn = testBtnPlan.Find("testHtp").GetComponent<Button>();
            tbtn.onClick.AddListener(() =>
            {
                GuildSDKManager.SendCommand((int)GuildCommand.interactiveSendGuildText, "发送了一条测试消息." + System.DateTime.Now.ToString(),
                    (eventData) => {
                        Debug.Log("测试发送消息返回: " + eventData.ToJson());
                    });
                //RequestPathObject treqpath = new RequestPathObject("login");
                //GuildSettingRequest treq = new GuildSettingRequest();

                //string tjson = JsonConvert.SerializeObject(treq);
                //var tdata = System.Text.Encoding.UTF8.GetBytes(tjson);
                //Habby.Net.HttpNet.SendPost(treqpath.GetRequestUrl(), tdata, (code, response, error) => {
                //    Debug.LogFormat("code = {0}, response = {1}, error = {2}",code,response,error);

                //    if(code == 0)
                //    {
                //        DefaultResponse<GuildSettingResponse> tobj = JsonConvert.DeserializeObject<DefaultResponse<GuildSettingResponse>>(response);
                //        Debug.Log(tobj.ToJson());
                //    }
                //});
            });
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        //UnityEngine.SystemInfo.deviceUniqueIdentifier
        //            UserID = "test",
            //UserSig = "eJyrVgrxCdYrSy1SslIy0jNQ0gHzM1NS80oy0zLBwiWpxSVQ8eKU7MSCgswUJStDEwMDUwMjUxMjiExJZm4qUNTM0NzSzNDQ3AwimlpRkFkEFDcytTQyMDCAGpKZDjQ13D3Kt9TQ1LMwJ7sy39fdI9IjzDvIxNk1z9HH0tzNKTvZLMopPyrZQz8p3VapFgC18zBR",
           // GuildID = "@TGS#2LOW32CHB",
           //"1400502542",
        GuildSettingSetData tsetting = new GuildSettingSetData()
        {
            UserID = "5212369854336",
            IMAppID = 1400561884,
            Version = "12345",
        };

        GuildSDKManager.SetGuildServerUrl("http://172.16.18.241:3006");
        GuildSDKManager.OnReciveEvent += OnReciveMessageEvent;
        GuildSDKManager.CreatTransIdDelgate = () => { return "7890"; };
        GuildSDKManager.InitSDK(tsetting);

        GuildLogin();

        ActionData ttestData = new ActionData();
        ttestData.actionData = "{\"test\":222222}";

        Debug.Log(ttestData.ToJson());
    }

    void GuildLogin()
    {

        UserContentInfo tinfo = new UserContentInfo();
        tinfo.headIcon = "61002";
        tinfo.nickName = "测试登录玩家1";


        GuildSDKManager.Login(tinfo,(data) =>
        {
             Debug.Log(data.ToJson());
            if (data.code == 0)
            {
                //test init code
                //GuildSDKManager.Setting.GuildID = "testguidid-3344555";
                // GuildSDKManager.SendCommand((int)GuildCommand.IMlogin, null, (eventData) => {
                //     if(eventData.code == 0)
                //     {

                //     }
                // });
                //GuildSDKManager.InteractiveModule.Login();
            }
            else
            {

            }

        });
    }
    void OnReciveMessageEvent(MesssageEventBase pMsg)
    {
        Debug.Log(pMsg.ToJson());
        switch (pMsg.type)
        {
            case MessageEventType.onReciveNewMessage:
                ONReciveMessage(pMsg);
                break;
        }
    }
    void ONReciveMessage(MesssageEventBase pMsg)
    {
        GuildMessageEntity tmsg = (GuildMessageEntity)pMsg.data;
        switch (tmsg.type)
        {
            case GuildMessageType.action:
                Debug.Log(tmsg.actionElem.actionData.ToJson());
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
