using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class Helloworld02 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextAsset ta = Resources.Load<TextAsset>("helloworld.lua");//helloworld.lua.txt
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.DoString(ta.text);
        luaEnv.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
