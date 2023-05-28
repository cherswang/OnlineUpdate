using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Runtime.InteropServices;//弹窗依赖
//webclient依赖
using System.Net;
using System.IO;
//
using UnityEngine.Networking;
using Newtonsoft.Json;
using LitJson;

public class GameMain : MonoBehaviour
{
    int checkCount = 0;
    public Text textState;
    public Text textLog;
    public Button testReadButton;
    //public Button testWriteButton;
    //public Button testDeleteButton;
    public Slider slider;  //进度条

    public Text Load_Data;  //加载数
    public Slider my_Slider;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        //每次检查累积到一定次数则去启动主程序
        //InvokeRepeating("CheckMainProRun", 2f,2f);
        //获取绑定的按钮，并添加事件监听
        testReadButton.onClick.AddListener(IOTestRead);
        //testWriteButton.onClick.AddListener(IOTestWrite);
        //testDeleteButton.onClick.AddListener(IOTestDelete);
        StartCoroutine(beginloading());
        PersonToJson();
        JsonToPerson();
        StartCoroutine(GetData());
    }

    //检查主程序是否运行中。如果超过3次未运行则启动
    void CheckMainProRun()
    {
        UnityEngine.Debug.Log("### CheckMainProRun");
        if(ProcessIsExist()==false)
        {
            checkCount++;
            textState.text = "<color='#FF0000'>未发现主程序运行</color>"; //"未发现主程序运行";
        }
        else
        {
            textState.text = "<color='#00FF00'>主程序运行中</color>"; 
        }

        if(checkCount>3)
        {
            checkCount = 0;
            StartProcess();
        }
    }

    public bool ProcessIsExist()
    {
        Process[] ps = Process.GetProcessesByName("4UAIGC");

        if (ps.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void StartProcess()
    {
        try
        {
            string str = System.Environment.CurrentDirectory;
            UnityEngine.Debug.Log("############str:" + str);
            string file = str + "\\4UAIGC.exe";

          //  string file = Application.streamingAssetsPath + @"/danmu4u/danmu-exe/danmu.exe";
            UnityEngine.Debug.Log("############file:" + file);
            Process myprocess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(file);
            myprocess.StartInfo = startInfo;
            myprocess.StartInfo.CreateNoWindow = false;
            myprocess.Start();
            //   return true;
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("出错原因000：" + ex.Message);
            textLog.text = ex.Message;
        }
    }
    public void IOTestWrite()
    {
        UnityEngine.Debug.Log("write");
        //存储文件的路径和文件名
        string file = "E:\\test.txt";
        string[] data = new string[2];
        data[0] = "第一条信息";
        data[1] = "第二条信息";

        //将两条数据写入E盘的test.dat文件中
        System.IO.File.WriteAllLines(file,data);


        //输入任意键退出
        Console.ReadKey();
    }

    [Obsolete]
    public void IOTestRead()
    {
        UnityEngine.Debug.Log("read");
        //存储文件的路径和文件名
        string file = "E:\\test.txt";

        //判断文件是否存在
        if (System.IO.File.Exists(file))
        {
            //用于存储读入的数据
            string[] data = new string[2];

            //将test文件中的数据读出来
            data = System.IO.File.ReadAllLines(file);

            //打印数据
            Console.WriteLine(data);
            Console.WriteLine(data[0]);
            Console.WriteLine(data[1]);
        }
        int returnNum;
        returnNum = Messagebox.MessageBox(IntPtr.Zero, "点击按钮去下载最新版本！", "温馨提示", 1);
        UnityEngine.Debug.Log(returnNum);
        if (returnNum == 1)
        {
            UnityEngine.Debug.Log("确定");
            //WebClient webClient = new WebClient();
            //webClient.Encoding = Encoding.UTF8;

            ////这里使用DownloadString方法，如果是不需要对文件的文本内容做处理，直接保存，那么可以直接使用功能DownloadFile(url,savepath)直接进行文件保存。
            //string outText = webClient.DownloadString("http://222.222.185.194:8800/wy_eh_api/scms/article-app/getListByTagId?tagId=2&page=0&pageSize=10");
            //File.WriteAllText("E:\\test1.html", outText);

            string str = System.Environment.CurrentDirectory;
            UnityEngine.Debug.Log("############str:" + str);
            string file1 = str + "\\dotNetFx45_Full_setup.exe";

            //  string file = Application.streamingAssetsPath + @"/danmu4u/danmu-exe/danmu.exe";
            UnityEngine.Debug.Log("############file:" + file1);
            Process myprocess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(file1);
            myprocess.StartInfo = startInfo;
            myprocess.StartInfo.CreateNoWindow = false;
            myprocess.Start();
        }
        else
        {
            UnityEngine.Debug.Log("取消");
            //System.Environment.Exit(System.Environment.ExitCode);
            //https://dn.kmplayer.com/Dn/kmp64x/KMP64_2023.4.26.13.exe
            //string downloadFileUrl = "https://dn.kmplayer.com/Dn/kmp64x/KMP64_2023.4.26.13.exe";
            //StartCoroutine(DownloadFile(downloadFileUrl));
        }

        //输入任意键退出
        Console.ReadKey();
    }

    public void IOTestDelete()
    {
        UnityEngine.Debug.Log("delete");
        //存储文件的路径和文件名
        string file = "E:\\test.txt";

        //判断文件是否存在
        if (System.IO.File.Exists(file))
        {
            //删除
            System.IO.File.Delete(file);
        }

        //输入任意键退出
        Console.ReadKey();
    }

    IEnumerator beginloading()
    {
        for (float i = 0; i < 100;) //从零开始，到100结束
        {
            i += UnityEngine.Random.Range(0.1f, 1.5f); //每次循环累加数浮点数（从0.1到1.5随机）
            slider.value = i; //将累加的数赋予进度条的值
            Load_Data.text = (int)(slider.value) + "%"; //将进度条值转化成int类型加上百分比赋予加载数。
            //yield return new WaitForEndOfFrame();
            yield return null;
            if (slider.value >= 100)
            {
                //if(testReadButton.IsActive == false)
                //{

                //}
                testReadButton.gameObject.SetActive(true);
                UnityEngine.Debug.Log("slider.value");
                int returnNum;
                returnNum = Messagebox.MessageBox(IntPtr.Zero, "点击确定去安装最新版本！", "温馨提示", 0);
                UnityEngine.Debug.Log(returnNum);
                if (returnNum == 1)
                {
                    UnityEngine.Debug.Log("确定");
                    string str = System.Environment.CurrentDirectory;
                    UnityEngine.Debug.Log("############str:" + str);
                    string file1 = str + "\\dotNetFx45_Full_setup.exe";
                    //  string file = Application.streamingAssetsPath + @"/danmu4u/danmu-exe/danmu.exe";
                    UnityEngine.Debug.Log("############file:" + file1);
                    Process myprocess = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo(file1);
                    myprocess.StartInfo = startInfo;
                    myprocess.StartInfo.CreateNoWindow = false;
                    myprocess.Start();
                }
            }
        }

        yield return null;
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="url">下载的地址</param>
    /// <returns></returns>
    [Obsolete]
    IEnumerator DownloadFile(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        UnityEngine.Debug.Log(url);
        UnityEngine.Debug.Log(request);
        request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            print("当前的下载发生错误" + request.error);
            yield break;
        }
        while (!request.isDone)
        {
            print("当前的下载进度为：" + request.downloadProgress);
            UnityEngine.Debug.Log(request.downloadProgress);
            UnityEngine.Debug.Log("1");
            my_Slider.value = request.downloadProgress;
            yield return 0;
        }
        if (request.isDone)
        {
            UnityEngine.Debug.Log("2");
            my_Slider.value = 1;
            //将下载的文件写入
            using (FileStream fs = new FileStream(Application.streamingAssetsPath + "/Test.MP4", FileMode.Create))
            {
                byte[] data = request.downloadHandler.data;
                fs.Write(data, 0, data.Length);
            }
        }
    }

    //加载路径图片，循环解析json
    [Obsolete]
    IEnumerator GetData()//Action action
    {
        yield return new WaitForSeconds(0.2f);
        //链接就不写了
        UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://222.222.185.194:8800/wy_eh_api/basic/apk/latest/version/");
        //UnityWebRequest unityWebRequest = UnityWebRequest.Get(jsonUrl);
        yield return unityWebRequest.SendWebRequest();
        if (!unityWebRequest.isNetworkError)
        {
            string data = unityWebRequest.downloadHandler.text;
            JsonData jd = JsonMapper.ToObject<JsonData>(data);

            UnityEngine.Debug.Log("jd' data: " + data);
            int code = (int)jd["code"];
            UnityEngine.Debug.Log(code);

            string appDownload = (string)jd["data"]["appDownload"];
            UnityEngine.Debug.Log(appDownload);

            string versionnumber = (string)jd["data"]["versionNumber"];
            UnityEngine.Debug.Log(versionnumber);

            UnityEngine.Debug.Log(Application.version);
        }
    }

     public static void PersonToJson()
    {
        Person bill = new Person();

        bill.Name = "William Shakespeare";
        bill.Age  = 51;
        bill.Birthday = new DateTime(1564, 4, 26);

        string json_bill = JsonMapper.ToJson(bill);

        Console.WriteLine(json_bill);
        UnityEngine.Debug.Log(json_bill);
    }

    public static void JsonToPerson()
    {
        //加@是为了保证
        string json = @"
            {
                ""Name""     : ""Thomas More"",
                ""Age""      : 57,
                ""Birthday"" : ""02/07/1478 00:00:00""
            }";

        Person thomas = JsonMapper.ToObject<Person>(json);

        Console.WriteLine("Thomas' age: {0}", thomas.Age);
        UnityEngine.Debug.Log("Thomas' age: "+thomas.Age);
    }
}

public class Messagebox
{
    [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr handle, String message, String title, int type);
}

public class VersionData
{
    public string data { get; set; }

    public string sign { get; set; }

    public string code { get; set; }

    public string msg { get; set; }
}

public class Person
{
    // C# 3.0 auto-implemented properties
    public string   Name     { get; set; }
    public int      Age      { get; set; }
    public DateTime Birthday { get; set; }
}


