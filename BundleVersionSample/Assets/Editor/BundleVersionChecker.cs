using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class BundleVersionChecker
{
    const string ClassName = "CurrentBundleVersion";
    const string TargetCodeFile = "Assets/" + ClassName + ".cs";

    static BundleVersionChecker () {
        string bundle_version = PlayerSettings.bundleVersion;

        string last_version = CurrentBundleVersion.version;
        if (last_version != bundle_version) {
            Debug.Log ("バージョンが新しくなりました！" + TargetCodeFile + "内に書かれたバージョンを" + last_version +"から" + bundle_version + "に変えます。" );
            CreateNewBuildVersionClassFile (bundle_version);
        }
    }

    static string CreateNewBuildVersionClassFile (string bundle_version) {
        using (StreamWriter writer = new StreamWriter (TargetCodeFile, false)) {
            try {
                string code = GenerateCode (bundle_version);
                writer.WriteLine ("{0}", code);
            } catch (System.Exception ex) {
                string msg = " threw:\n" + ex.ToString ();
                Debug.LogError (msg);
                EditorUtility.DisplayDialog ("Error クラスを作りなおし中にエラーが発生しました！", msg, "OK");
            }
        }
        return TargetCodeFile;
    }

    static string GenerateCode (string bundle_version) {
        string code = "public static class " + ClassName + "\n{\n";
        code += System.String.Format ("\tpublic static readonly string version = \"{0}\";", bundle_version);
        code += "\n}\n";
        return code;
    }
}
