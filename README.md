hy-frame
========

# 包含功能
核心代码全在 HY.Frame.Core 项目中

## 项目依赖
log4net, Newtonsoft.Json, 

## 扩展方法
在命名空间 HY.Frame.Core

## 常用工具
HY.Frame.Core.CacheData缓存 
HY.Frame.Core.Toolkit.*


## 路径映射
配置说明
### II6
>system.web/httpHandlers/&lt;add verb="*" path="*.c" validate="false" type="HY.Frame.Core.MAgentFactory, HY.Frame.Core"/>

### II7
>system.webServer/handlers/&lt;add name="cls" verb="*" path="*.c" resourceType="Unspecified" type="HY.Frame.Core.MAgentFactory, HY.Frame.Core" />

### 路径映射
/name/space/class/method.c

method 要标记HY.Frame.Core.WebApiAttribute 才能访问

method 标记ActionBefore(After)HandlerAttribute 实现过滤器功能

method 的方法可以使用简单类型的参数,参数值会尝试从form和query中寻找,并赋值(trim)

method 中尽量避免使用HttpContent上下文, 应该包装成其他工具进行调用Json

method 的返回值有多种选择 HY.Frame.Core.ResResult, HY.Frame.Core.JsonResult, HY.Frame.Core.ResJsonResult, System.String

自定义的返回类型默认返回ObjectExtensions.ToJson(obj), 也可以在Application_Start中使用HY.Frame.Core.ToJSON(Type ty, IJSON i)进行注册, 框架已经为DataTable进行了注册

# HY.Auth 组件

提供基本的角色菜单和权限配置

## 功能依赖
HY.Frame.Core, /App_data/Auth.xml,
### 功能调用
var u = new HY.Auth.AuthedUser(认证用户的角色); u.GetNodes() 得到本角色所属的菜单
## 界面配置展示
依赖 ext/bootstrap.js 和 ext/resources/css/ext-all.css,   
在界面调用&lt;%@ Register Assembly="HY.Auth" TagPrefix="uc" Namespace="HY.Auth" %>   
映射&lt;add name="nav" verb="*" path="*.NavConfig" resourceType="Unspecified" type="HY.Auth.NavConfigHandler, HY.Auth" />



# OData v3
在 .web项目中要使用v3, 需要nuget添加引用 WCF Data Services Client & WCF Data Services Server

