# UpdateService

**联系作者：419731519（QQ）**

## UpdateService介绍
- 这个插件能帮助到你在任何脚本里，也能方便的控制**Update**,**LateUpdate**,**FixUpdate**的先后顺序
- 当使用这个插件时，会自动生成一个隐藏的游戏物体，以启动**UpdateService**
- 使用单向链表跟对象池的方式以优化代码的执行调用
- **Order**越小，越先被调用
- 觉得我的插件能帮助到你，麻烦帮我点个Star支持一下❤️

## How To
- manifest.json中添加插件路径
```json
{
  "dependencies": {
	"com.leeframework.updateservice":"https://e.coding.net/ggdevlee/leeframework/UpdateService.git#1.0.0"
  }
}
```

- 引入命名空间
```csharp
using LeeFramework.Update;
```

- Register

```csharp
private void Cb100()
{
	Debug.Log("100");
}

UpdateSvc.instance.Register(UpdateType.Update, Cb100, 100);
```

- Unregister

```csharp
UpdateSvc.instance.Unregister(UpdateType.Update, Cb100, 100);
```