// Decompiled with JetBrains decompiler
// Type: Properties.Resources
// Assembly: svchost, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6FDD5C0B-9A59-4045-B263-5782D96B1E77
// Assembly location: C:\Users\gorno\Desktop\Dumped_fix.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (Properties.Resources.resourceMan == null)
          Properties.Resources.resourceMan = new ResourceManager("Properties.Resources", typeof (Properties.Resources).Assembly);
        return Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return Properties.Resources.resourceCulture;
      }
      set
      {
        Properties.Resources.resourceCulture = value;
      }
    }

    internal static byte[] AudioHD
    {
      get
      {
        return (byte[]) Properties.Resources.ResourceManager.GetObject("AudioHD", Properties.Resources.resourceCulture);
      }
    }

    internal static byte[] msvcp140
    {
      get
      {
        return (byte[]) Properties.Resources.ResourceManager.GetObject("msvcp140", Properties.Resources.resourceCulture);
      }
    }

    internal static byte[] vcruntime140
    {
      get
      {
        return (byte[]) Properties.Resources.ResourceManager.GetObject("vcruntime140", Properties.Resources.resourceCulture);
      }
    }

    internal Resources()
    {
    }
  }
}
