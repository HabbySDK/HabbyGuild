// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using Habby.Guild;
// using Habby;
// using Habby.Guild.Data;
// using Habby.Guild.Module;
// using Habby.Guild.Command;
// using UnityEngine.UI;
// using Newtonsoft.Json;
// using System.IO;
// public class TestUniWebView : MonoBehaviour
// {
//     public class InitData : BaseData
//     {
//         public string infoUrl = "";
//         public string userId = "";
//         public string guildUrl = "";
//     }

//     public WebViewController webView;
//     public Button infoBtn;
//     public Button hideViewBtn;
//     public Button showViewBtn;
//     public Button logBtn;

//     public InputField userInput;
//     public InputField urlInput;
//     public InputField webUrlInput;
//     public Button InitSDKBtn;


//     private InitData initData = new InitData();
//     private string datafile = "initData.txt";
//     private string pathFile = "";
//     private void Awake() {
//         SRDebug.Init();
//         LogToFile.InitLogCallback();

//         pathFile = string.Format("{0}/{1}", Application.persistentDataPath, datafile);
//         if (File.Exists(pathFile))
//         {
//             string tjson = File.ReadAllText(pathFile);
//             initData.FromJson(tjson);
//         } 

//         infoBtn.onClick.AddListener(()=>{
//             webView.LoadURL(initData.infoUrl);
//         });

//         hideViewBtn.onClick.AddListener(()=>{
//             webView.HideView();
//         });

//         showViewBtn.onClick.AddListener(()=>{
//             webView.ShowView();
//         });

//         logBtn.onClick.AddListener(()=>{
//             webView.HideView();
//             SRDebug.Instance.ShowDebugPanel();
//         });

//         InitSDKBtn.onClick.AddListener(()=>{
//             initData.infoUrl = webUrlInput.text;
//             initData.userId = userInput.text;
//             initData.guildUrl = urlInput.text;
//             GuildSDKManager.SetUser(initData.userId);
//             GuildSDKManager.SetGuildServerUrl(initData.guildUrl);


//             File.WriteAllText(pathFile,initData.ToJson());

//         });

//         if(string.IsNullOrEmpty(initData.infoUrl))
//         {
//             initData.infoUrl = webUrlInput.text;
//         }

//         if(string.IsNullOrEmpty(initData.userId))
//         {
//             initData.userId = userInput.text;
//         }

//         if(string.IsNullOrEmpty(initData.guildUrl))
//         {
//             initData.guildUrl = urlInput.text;
//         }

//         webUrlInput.text = initData.infoUrl;
//         userInput.text = initData.userId;
//         urlInput.text = initData.guildUrl;

//         webView.InitView();

//         GuildSettingSetData tsetting = new GuildSettingSetData()
//         {
//             UserID = initData.userId,
//             IMAppID = 1400502542,
//             Version = "12345",
//         };
//         GuildSDKManager.SetGuildServerUrl(initData.guildUrl);
//         GuildSDKManager.OnReciveEvent += OnReciveMessageEvent;
//         GuildSDKManager.CreatTransIdDelgate = () => { return "7890"; };
//         GuildSDKManager.InitSDK(tsetting);
        
//     }


//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     void GuildLogin()
//     {

//         UserContentInfo tinfo = new UserContentInfo();
//         tinfo.headIcon = "61002";
//         tinfo.nickName = "测试登录玩家1";

        
//         GuildSDKManager.Login(tinfo,(data) =>
//         {
//             if (data.code == 0)
//             {
//                 //test init code
//                 //GuildSDKManager.Setting.GuildID = "testguidid-3344555";
                
//                 GuildSDKManager.SendCommand((int)GuildCommand.IMlogin);
//                 //GuildSDKManager.InteractiveModule.Login();
//             }
//             else
//             {

//             }

//         });
//     }
//     void OnReciveMessageEvent(MesssageEventBase pMsg)
//     {
//         DLog.Log("OnReciveMessageEvent:" + pMsg.ToJson());
//         switch (pMsg.type)
//         {
//             case MessageEventType.onReciveNewMessage:
//                 ONReciveMessage((MessageEventData<GuildMessageEntity>)pMsg);
//                 break;
//             case MessageEventType.interactiveSystem:
//                 OnSystemMessage((MessageEventData<InteractiveSystemMessage>)pMsg);
//                 break;
//         }
//     }
//     void ONReciveMessage(MessageEventData<GuildMessageEntity> pMsg)
//     {
//         GuildMessageEntity tmsg = (GuildMessageEntity)pMsg.data;


//         switch (tmsg.type)
//         {
//             case GuildMessageType.action:
//                 {
//                     ActionElemMessageEntity tcustom = tmsg.actionElem;
//                     DLog.Log(tcustom.actionData);
//                 }
//                 break;
//             default:
//                 DLog.Log(tmsg.messageData.ToJson());
//                 break;
//         }
//     }
//     void OnSystemMessage(MessageEventData<InteractiveSystemMessage> pData)
//     {
//         InteractiveSystemMessage pMsg = (InteractiveSystemMessage)pData.data;
//         // switch (pMsg.type)
//         // {
//         //     case InteractiveSystemMessageType.login:
//         //         {
//         //             if (pData.code == 0)
//         //             {
//         //                 var tdata = new Habby.Guild.Command.CommandMemberMessageList.Data();
//         //                 tdata.count = 10;
//         //                 tdata.msgID = "";
//         //                 GuildSDKManager.SendCommand((int)GuildCommand.interactiveMemberMessageList,tdata);

//         //                 string tjson= "{count:1,msgID:\"\"}";
//         //                 GuildSDKManager.SendCommand((int)GuildCommand.interactiveMemberMessageList,null,tjson);
//         //             }
//         //             else
//         //             {
//         //                 GuildSDKManager.InteractiveModule.Login();
//         //             }

//         //         }
//         //         break;
//         // }



//     }
// }
