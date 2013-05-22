package mymovies;


public class DictionaryProvider
	extends android.content.ContentProvider
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:()Z:GetOnCreateHandler\n" +
			"n_query:(Landroid/net/Uri;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;:GetQuery_Landroid_net_Uri_arrayLjava_lang_String_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_Handler\n" +
			"n_getType:(Landroid/net/Uri;)Ljava/lang/String;:GetGetType_Landroid_net_Uri_Handler\n" +
			"n_delete:(Landroid/net/Uri;Ljava/lang/String;[Ljava/lang/String;)I:GetDelete_Landroid_net_Uri_Ljava_lang_String_arrayLjava_lang_String_Handler\n" +
			"n_insert:(Landroid/net/Uri;Landroid/content/ContentValues;)Landroid/net/Uri;:GetInsert_Landroid_net_Uri_Landroid_content_ContentValues_Handler\n" +
			"n_update:(Landroid/net/Uri;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I:GetUpdate_Landroid_net_Uri_Landroid_content_ContentValues_Ljava_lang_String_arrayLjava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("MyMovies.DictionaryProvider, MyMovies, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DictionaryProvider.class, __md_methods);
	}


	public DictionaryProvider ()
	{
		super ();
		if (getClass () == DictionaryProvider.class)
			mono.android.TypeManager.Activate ("MyMovies.DictionaryProvider, MyMovies, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onCreate ()
	{
		return n_onCreate ();
	}

	private native boolean n_onCreate ();


	public android.database.Cursor query (android.net.Uri p0, java.lang.String[] p1, java.lang.String p2, java.lang.String[] p3, java.lang.String p4)
	{
		return n_query (p0, p1, p2, p3, p4);
	}

	private native android.database.Cursor n_query (android.net.Uri p0, java.lang.String[] p1, java.lang.String p2, java.lang.String[] p3, java.lang.String p4);


	public java.lang.String getType (android.net.Uri p0)
	{
		return n_getType (p0);
	}

	private native java.lang.String n_getType (android.net.Uri p0);


	public int delete (android.net.Uri p0, java.lang.String p1, java.lang.String[] p2)
	{
		return n_delete (p0, p1, p2);
	}

	private native int n_delete (android.net.Uri p0, java.lang.String p1, java.lang.String[] p2);


	public android.net.Uri insert (android.net.Uri p0, android.content.ContentValues p1)
	{
		return n_insert (p0, p1);
	}

	private native android.net.Uri n_insert (android.net.Uri p0, android.content.ContentValues p1);


	public int update (android.net.Uri p0, android.content.ContentValues p1, java.lang.String p2, java.lang.String[] p3)
	{
		return n_update (p0, p1, p2, p3);
	}

	private native int n_update (android.net.Uri p0, android.content.ContentValues p1, java.lang.String p2, java.lang.String[] p3);

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
