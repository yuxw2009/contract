<%@ WebHandler Language="C#" Class="ContractTempHandler" %>

using System;
using System.Web;
using BLL;
using Model;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ContractTempHandler : IHttpHandler {

    private readonly ContactTempletBLL bll = new ContactTempletBLL();
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        //类型
        if (!string.IsNullOrEmpty(context.Request["ty"]))
        {
            int relt = 0;
            string ty = context.Request["ty"].ToString();
            if (ty == "0")//修改模板基础信息
            {
                string jsonStr=context.Request["jsonStr"].ToString();
                if (!string.IsNullOrEmpty(jsonStr))
                {
                    List<ContactTemplet> listMod = JsonConvert.DeserializeObject<List<ContactTemplet>>(jsonStr);//将JSON转成实体
                    relt=bll.UpdateContactTemplet(listMod);
                }
            }
            context.Response.Write(relt.ToString());
        }
        else
        {
          
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}