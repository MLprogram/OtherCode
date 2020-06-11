using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class Helloworld : MonoBehaviour
{
    private LuaEnv luaEnv;
    void Start()
    {
        luaEnv = new LuaEnv();//创建lua运行环境
        luaEnv.DoString("print('Hello world')");//在()里面写lua语句


        LuaEnv env = new LuaEnv();
        env.AddLoader(MyLoader);
        env.DoString("require 'test007'");
        env.Dispose();

    }
    private byte[] MyLoader(ref string filePath)
    {
        string adsPath = Application.streamingAssetsPath + "/" + filePath + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(adsPath));
    }
    private void OnDestroy()
    {
        luaEnv.Dispose();//释放lua环境
    }
 
}
