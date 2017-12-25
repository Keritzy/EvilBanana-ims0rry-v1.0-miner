// Decompiled with JetBrains decompiler
// Type: t.Properties.Resources
// Assembly: svchost, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6FDD5C0B-9A59-4045-B263-5782D96B1E77
// Assembly location: C:\Users\gorno\Desktop\Dumped_fix.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace t.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  public class Resources
  {
    private static CultureInfo resourceCulture;
    private static ResourceManager resourceMan;

    public static byte[] AudioHD
    {
      get
      {
        return (byte[]) t.Properties.Resources.ResourceManager.GetObject("AudioHD", t.Properties.Resources.resourceCulture);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
      get
      {
        return t.Properties.Resources.resourceCulture;
      }
      set
      {
        t.Properties.Resources.resourceCulture = value;
      }
    }

    public static byte[] msvcp140
    {
      get
      {
        return (byte[]) t.Properties.Resources.ResourceManager.GetObject("msvcp140", t.Properties.Resources.resourceCulture);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static ResourceManager ResourceManager
    {
      get
      {
        if (t.Properties.Resources.resourceMan == null)
          t.Properties.Resources.resourceMan = new ResourceManager("t.Properties.Resources", typeof (t.Properties.Resources).Assembly);
        return t.Properties.Resources.resourceMan;
      }
    }

    public static byte[] vcruntime140
    {
      get
      {
        return (byte[]) t.Properties.Resources.ResourceManager.GetObject("vcruntime140", t.Properties.Resources.resourceCulture);
      }
    }

    internal Resources()
    {
    }
  }
}
