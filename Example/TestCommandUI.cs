
using UnityEngine;
using Habby.Guild;
using Habby;
using Habby.Guild.Data;
using UnityEngine.UI;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
public class TestCommandUI : MonoBehaviour 
{
    public class InitData : BaseData
    {
        public string imid = "";
        public string userId = "";
        public string guildUrl = "";

        public int startTransId = 1;
        public string netVersion = "60";
    }
    public InputField jsonDataInputField;
    public InputField imidInputField;
    public InputField cmdInputField;
    public InputField urlInputField;
    public InputField userInputField;
    public InputField transIdInput;
    public InputField versionInput;
    public Button sendbutton;
    public Button initBtn;

    public Button showLog;

    public Text LogView;
    public RectTransform contentView;

    //public string url = "http://172.16.18.241:3006";
    //public string userid = "5212369854336";
    private string datafile = "initData.txt";

    //[104,101,108,108,111,119,111,114,108,100]
    //{"customData":[104,101,108,108,111,119,111,114,108,100],"groupID":"1518"}
    //{"groupId":"1518","customData":"aGVsbG93b3JsZA=="}
    
    //https://test-badguys-guild.habby.com
    //IM appId : 40000010
    private InitData initData = new InitData();
    private string pathFile;
    private void Awake()
    {
        var ttt = new IMUserProfile();
        ttt.nickName = "223";
        ttt.faceIcon = "332";
        ttt.customData = "";
        GuildDLog.Log(BaseData.ToJson(ttt));

        string testJson = "{\"actionData\":\"{\\\"info\\\":{\\\"cumulativeIndividualScore\\\":0,\\\"cumulativePersonalContribution\\\":0,\\\"curWeekPraiseNum\\\":12,\\\"engagementPoints\\\":43330,\\\"individualScore\\\":139,\\\"joinGuildTime\\\":1632835657,\\\"joinedGuildCount\\\":6,\\\"loginTime\\\":1632805885,\\\"permissionLevel\\\":0,\\\"personalContribution\\\":0,\\\"praiseNum\\\":12,\\\"userDesc\\\":\\\"{\\\\\\\"head_frame\\\\\\\": 0, \\\\\\\"stage_level\\\\\\\": 3}\\\",\\\"userIcon\\\":\\\"0\\\",\\\"userId\\\":\\\"72061992084443292\\\",\\\"userName\\\":\\\"JI\u4f60\u592a\u7f8e\\\"},\\\"name\\\":\\\"JI\u4f60\u592a\u7f8e\\\"}\",\"actionType\":101,\"guildId\":\"c59hgi7op8ip13871dvg\",\"userId\":\"72061992084443292\"}";
        
        JObject obj = JObject.Parse(testJson);
        
        GuildDLog.Log(obj.ToString());

        var tactdata = obj.ToObject<ActionData>();

        Debug.Log(tactdata.ToJson());
        

        showLog.onClick.AddListener(OnShowLogClick);

        sendbutton.onClick.AddListener(SendCommand);
        initBtn.onClick.AddListener(InitSDKClick);
        

        Application.logMessageReceived += logCallback;
    }

    private void Start() 
    {
        pathFile = string.Format("{0}/{1}", Application.persistentDataPath, datafile);
        if (File.Exists(pathFile))
        {
            string tjson = File.ReadAllText(pathFile);
            initData.FromJson(tjson);

            imidInputField.text = initData.imid;
            userInputField.text = initData.userId;
            urlInputField.text = initData.guildUrl;

            transIdInput.text = initData.startTransId.ToString();
            versionInput.text = initData.netVersion;
        }
        else
        {
            initData.imid = imidInputField.text;
            initData.userId = userInputField.text;
            initData.guildUrl = urlInputField.text;

            int.TryParse(transIdInput.text,out int ttransid);
            initData.startTransId = ttransid;
            initData.netVersion = versionInput.text;
        }
    }

    void OnShowLogClick()
    {

    }

    void InitSDK()
    {
        GuildSDKManager.UnInit();
        
        GuildSettingSetData tsetting = new GuildSettingSetData()
        {
            UserID = userInputField.text,
            //IMAppID = 1400561884,//20000066
            IMAppID = long.Parse(imidInputField.text),//20000066
            Version = versionInput.text,
        };

        GuildSDKManager.IMLogLev = IMLogLevel.imLog_Test;
        
        

        GuildSDKManager.CreatTransIdDelgate = GetTransId;
        GuildSDKManager.OnReciveEvent += OnReciveMessageEvent;

        GuildSDKManager.SetGuildServerUrl(urlInputField.text);
        GuildSDKManager.InitSDK(tsetting);
        ((GuildSettingOverwrite)GuildSDKManager.Setting).UserSig = "ddf73f584bb38d275d3e4b61629905c672ba7ec547dd37f9e4328a2a4427c99e";
    }

    void OnReciveMessageEvent(MesssageEventBase pMsg)
    {
        Debug.LogFormat("OnReciveMessageEvent:{0}",pMsg.ToJson());
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
                OnActionRec(tmsg.actionElem.actionData);
                break;
        }
    }

    void OnActionRec(ActionData data)
    {
        Debug.Log(data.ToJson());
        switch (data.actionType)
        {
            case ActionType.fixedChatContent:
                {
                    var tdata = data.actionData;
                    Debug.Log(tdata);
                }
                break;

            case ActionType.applyJoinRequest:
                {

                }
                break;
            case ActionType.customAction:
                {

                }
                break;
            default:
                break;
        }
    }

    void InitSDKClick()
    {
        InitSDK();

        SaveData();
    }

    void SaveData()
    {
        try
        {
            initData.userId = userInputField.text;
            initData.guildUrl = urlInputField.text;
            initData.startTransId = int.Parse(transIdInput.text);
            initData.netVersion = versionInput.text;
            File.WriteAllText(pathFile, initData.ToJson());
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }

    }

    string GetTransId()
    {
        initData.startTransId ++;
        return initData.startTransId.ToString();
    }

    void SendCommand()
    {
        int tcmd = -1;
        int.TryParse(cmdInputField.text,out tcmd);
        GuildSDKManager.SendCommandByJson(tcmd,jsonDataInputField.text,
        (eventdata)=>{
            Debug.LogFormat("SendCommand Click:{0}",eventdata.ToJson());
        });

        transIdInput.text = initData.startTransId.ToString();
        SaveData();
    }


    StringBuilder logStrBuilder = new StringBuilder();
    bool isNeedRebuild = false;
    void logCallback(string log, string pStackTrace, UnityEngine.LogType pType)
    {
        //logStrBuilder.AppendLine(string.Format("<color=yellow>[{0}]</color>{1}",pType,log));
        
        //isNeedRebuild = true;
    }


    float rebuildStep = 0;
    private void Update()
    {
        if(rebuildStep > Time.realtimeSinceStartup) return;
        rebuildStep = Time.realtimeSinceStartup + 1;
        // if (isNeedRebuild)
        // {
        //     LogView.text = logStrBuilder.ToString();
        //     LayoutRebuilder.ForceRebuildLayoutImmediate(contentView);
        //     float theight = LayoutUtility.GetPreferredHeight(contentView);
        //     contentView.anchoredPosition = new Vector2(0, theight);
        //     isNeedRebuild = false;
        // }

    }
}