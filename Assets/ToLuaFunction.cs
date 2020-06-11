using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class ToLuaFunction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XLua.LuaEnv env = new LuaEnv();
        env.AddLoader(MyLoader);
        env.DoString("require 'CSharpCallLua'");//调用完才能取得里面的变量
        Person p = env.Global.Get<Person>("person");
        print(p.name + "-" + p.age);
        p.name = "Akimoto";
        env.DoString("print(person.name)");
        IPerson Ip = env.Global.Get<IPerson>("person");
        print("IP"+Ip.name);
        ListTest(env);
        env.Dispose();
        //int a = env.Global.Get<int>("a");//获取lua里面的全局变量a
        //print(a);
        //string str = env.Global.Get<string>("str");//获取lua里面的全局变量str
        //print(str);
        //bool isDie = env.Global.Get<bool>("isDie");//获取lua里面的全局变量isDie
        //print(isDie);
        //env.Dispose();

    }
    private byte[] MyLoader(ref string filePath)
    {
        string adsPath = Application.streamingAssetsPath + "/" + filePath + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(adsPath));
    }
    private void ListTest(LuaEnv env)
    {
        //通过DIctionary，List
        //Dictionary<string, object> dict = env.Global.Get<Dictionary<string, object>>("person");
        //foreach (string key in dict.Keys)
        //{
        //    print(key + " " + dict[key]);
        //}

        List<object> list = env.Global.Get<List<object>>("person");
        foreach (object o in list)
        {
            print(o);
        }
        LuaTable luaTable = env.Global.Get<LuaTable>("person");
        print(luaTable.Get<string>("name"));
        print(luaTable.Get<int>("age"));


        //访问lua中的全局函数
        //Action act1 = env.Global.Get<Action>("add");
        //act1();
        Add add = env.Global.Get<Add>("add");
        add(1, 2);

        LuaFunction func = env.Global.Get<LuaFunction>("sdd");
        func.Call(1, 2);
       

    }
    [CSharpCallLua]
    delegate void Add(int a, int b);
}
class Person
{
    public string name;
    public int age; 
}

[CSharpCallLua]
public interface IPerson
{
    string name { get; set; }
    int age { get; set; }
}
