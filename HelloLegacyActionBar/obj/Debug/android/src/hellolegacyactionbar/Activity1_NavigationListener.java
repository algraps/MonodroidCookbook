package hellolegacyactionbar;


public class Activity1_NavigationListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.app.ActionBar.OnNavigationListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onNavigationItemSelected:(IJ)Z:GetOnNavigationItemSelected_IJHandler:Android.App.ActionBar/IOnNavigationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("HelloLegacyActionBar.Activity1/NavigationListener, HelloLegacyActionBar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Activity1_NavigationListener.class, __md_methods);
	}


	public Activity1_NavigationListener ()
	{
		super ();
		if (getClass () == Activity1_NavigationListener.class)
			mono.android.TypeManager.Activate ("HelloLegacyActionBar.Activity1/NavigationListener, HelloLegacyActionBar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onNavigationItemSelected (int p0, long p1)
	{
		return n_onNavigationItemSelected (p0, p1);
	}

	private native boolean n_onNavigationItemSelected (int p0, long p1);

	java.util.ArrayList refList;
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
