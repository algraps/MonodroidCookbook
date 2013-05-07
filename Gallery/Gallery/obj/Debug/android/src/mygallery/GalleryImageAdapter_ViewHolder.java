package mygallery;


public class GalleryImageAdapter_ViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MyGallery.GalleryImageAdapter/ViewHolder, MyGallery, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GalleryImageAdapter_ViewHolder.class, __md_methods);
	}


	public GalleryImageAdapter_ViewHolder ()
	{
		super ();
		if (getClass () == GalleryImageAdapter_ViewHolder.class)
			mono.android.TypeManager.Activate ("MyGallery.GalleryImageAdapter/ViewHolder, MyGallery, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
