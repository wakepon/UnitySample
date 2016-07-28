using UnityEngine;
using System.IO;

public class Config : Singleton< Config >
{
	private static SystemLanguage _language = Application.systemLanguage;

    public static SystemLanguage Language { get { return _language; } }

    // if you want to change language setting, please use this
	public static void ChangeLanguage ( SystemLanguage lang ) {
        _language = lang;
    }
}

