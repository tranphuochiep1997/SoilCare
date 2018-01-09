package md5ba50a4524fe383aa42943e8189f31479;


public class initial3Activity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("SoilCare.Android.initial3Activity, SoilCare.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", initial3Activity.class, __md_methods);
	}


	public initial3Activity ()
	{
		super ();
		if (getClass () == initial3Activity.class)
			mono.android.TypeManager.Activate ("SoilCare.Android.initial3Activity, SoilCare.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
