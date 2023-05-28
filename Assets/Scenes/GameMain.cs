using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Runtime.InteropServices;//��������
//webclient����
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
    public Slider slider;  //������

    public Text Load_Data;  //������
    public Slider my_Slider;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        //ÿ�μ���ۻ���һ��������ȥ����������
        //InvokeRepeating("CheckMainProRun", 2f,2f);
        //��ȡ�󶨵İ�ť��������¼�����
        testReadButton.onClick.AddListener(IOTestRead);
        //testWriteButton.onClick.AddListener(IOTestWrite);
        //testDeleteButton.onClick.AddListener(IOTestDelete);
        StartCoroutine(beginloading());
        PersonToJson();
        JsonToPerson();
        StartCoroutine(GetData());
    }

    //����������Ƿ������С��������3��δ����������
    void CheckMainProRun()
    {
        UnityEngine.Debug.Log("### CheckMainProRun");
        if(ProcessIsExist()==false)
        {
            checkCount++;
            textState.text = "<color='#FF0000'>δ��������������</color>"; //"δ��������������";
        }
        else
        {
            textState.text = "<color='#00FF00'>������������</color>"; 
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
            UnityEngine.Debug.Log("����ԭ��000��" + ex.Message);
            textLog.text = ex.Message;
        }
    }
    public void IOTestWrite()
    {
        UnityEngine.Debug.Log("write");
        //�洢�ļ���·�����ļ���
        string file = "E:\\test.txt";
        string[] data = new string[2];
        data[0] = "��һ����Ϣ";
        data[1] = "�ڶ�����Ϣ";

        //����������д��E�̵�test.dat�ļ���
        System.IO.File.WriteAllLines(file,data);


        //����������˳�
        Console.ReadKey();
    }

    [Obsolete]
    public void IOTestRead()
    {
        UnityEngine.Debug.Log("read");
        //�洢�ļ���·�����ļ���
        string file = "E:\\test.txt";

        //�ж��ļ��Ƿ����
        if (System.IO.File.Exists(file))
        {
            //���ڴ洢���������
            string[] data = new string[2];

            //��test�ļ��е����ݶ�����
            data = System.IO.File.ReadAllLines(file);

            //��ӡ����
            Console.WriteLine(data);
            Console.WriteLine(data[0]);
            Console.WriteLine(data[1]);
        }
        int returnNum;
        returnNum = Messagebox.MessageBox(IntPtr.Zero, "�����ťȥ�������°汾��", "��ܰ��ʾ", 1);
        UnityEngine.Debug.Log(returnNum);
        if (returnNum == 1)
        {
            UnityEngine.Debug.Log("ȷ��");
            //WebClient webClient = new WebClient();
            //webClient.Encoding = Encoding.UTF8;

            ////����ʹ��DownloadString����������ǲ���Ҫ���ļ����ı�����������ֱ�ӱ��棬��ô����ֱ��ʹ�ù���DownloadFile(url,savepath)ֱ�ӽ����ļ����档
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
            UnityEngine.Debug.Log("ȡ��");
            //System.Environment.Exit(System.Environment.ExitCode);
            //https://dn.kmplayer.com/Dn/kmp64x/KMP64_2023.4.26.13.exe
            //string downloadFileUrl = "https://dn.kmplayer.com/Dn/kmp64x/KMP64_2023.4.26.13.exe";
            //StartCoroutine(DownloadFile(downloadFileUrl));
        }

        //����������˳�
        Console.ReadKey();
    }

    public void IOTestDelete()
    {
        UnityEngine.Debug.Log("delete");
        //�洢�ļ���·�����ļ���
        string file = "E:\\test.txt";

        //�ж��ļ��Ƿ����
        if (System.IO.File.Exists(file))
        {
            //ɾ��
            System.IO.File.Delete(file);
        }

        //����������˳�
        Console.ReadKey();
    }

    IEnumerator beginloading()
    {
        for (float i = 0; i < 100;) //���㿪ʼ����100����
        {
            i += UnityEngine.Random.Range(0.1f, 1.5f); //ÿ��ѭ���ۼ�������������0.1��1.5�����
            slider.value = i; //���ۼӵ��������������ֵ
            Load_Data.text = (int)(slider.value) + "%"; //��������ֵת����int���ͼ��ϰٷֱȸ����������
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
                returnNum = Messagebox.MessageBox(IntPtr.Zero, "���ȷ��ȥ��װ���°汾��", "��ܰ��ʾ", 0);
                UnityEngine.Debug.Log(returnNum);
                if (returnNum == 1)
                {
                    UnityEngine.Debug.Log("ȷ��");
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
    /// ����
    /// </summary>
    /// <param name="url">���صĵ�ַ</param>
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
            print("��ǰ�����ط�������" + request.error);
            yield break;
        }
        while (!request.isDone)
        {
            print("��ǰ�����ؽ���Ϊ��" + request.downloadProgress);
            UnityEngine.Debug.Log(request.downloadProgress);
            UnityEngine.Debug.Log("1");
            my_Slider.value = request.downloadProgress;
            yield return 0;
        }
        if (request.isDone)
        {
            UnityEngine.Debug.Log("2");
            my_Slider.value = 1;
            //�����ص��ļ�д��
            using (FileStream fs = new FileStream(Application.streamingAssetsPath + "/Test.MP4", FileMode.Create))
            {
                byte[] data = request.downloadHandler.data;
                fs.Write(data, 0, data.Length);
            }
        }
    }

    //����·��ͼƬ��ѭ������json
    [Obsolete]
    IEnumerator GetData()//Action action
    {
        yield return new WaitForSeconds(0.2f);
        //���ӾͲ�д��
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
        //��@��Ϊ�˱�֤
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


