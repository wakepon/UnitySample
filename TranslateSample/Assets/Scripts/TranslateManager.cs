using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;

public class TranslateText {
    public string jp;
    public string eng;
    public TranslateText( string jp_, string eng_ )
    {
        jp  = jp_;
        eng = eng_;
    }
}

public class TranslateManager : Singleton< TranslateManager > {
    private Dictionary<string, TranslateText> texts = new Dictionary<string, TranslateText>();

    ITagProcessor tagProcessor = null;

    //------------------------------------------------------------------------------
    //Resourceフォルダ以下にあるExcelデータを読み込む
    public void Read( string path )
    {
        TextAsset text_asset = Resources.Load<TextAsset>(path);
        Debug.Assert( text_asset != null );

        XmlDocument xmldoc = new XmlDocument ();
        xmldoc.LoadXml ( text_asset.text );

        var nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
        nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");

        XmlNodeList rows = xmldoc.GetElementsByTagName("Row");

        foreach (XmlNode row in rows){
            XmlNodeList datas = row.SelectNodes("ss:Cell/ss:Data", nsmgr);

            string key = datas[0].InnerText;
            string jp  = datas[1].InnerText;
            string eng = datas[2].InnerText;

            //改行コードを変換する
            //手元のExcelを使ってxml形式で保存すると改行が\r(CR)になっているので、
            //それを変換する
            jp = jp.Replace( "\r", "\n" );
            eng = eng.Replace( "\r", "\n" );

            texts[ key ] = new TranslateText( jp, eng );
        }
    }


    //------------------------------------------------------------------------------
    //ヒエラルキーにあるテキスト仕込んだキーから変換
	public void ApplyAllUIText()
	{
        Text[] text_components = Resources.FindObjectsOfTypeAll<Text>();
        for (var i = 0; i < text_components.Length; ++i) {
            InterpretText( ref text_components[i] );
        }
	}

    //------------------------------------------------------------------------------
    //タグプロセッサーを設定
	public void SetTagProcessor( ITagProcessor processor )
    {
        tagProcessor = processor;
    }

    //------------------------------------------------------------------------------
    //キーからテキスト取得
    public string GetText( string key )
    {
        string str = getTextRaw(key);
        if( tagProcessor != null )
        {
            str = tagProcessor.Process( str );
        }
        return str;
    }

    //------------------------------------------------------------------------------
    //Textクラスを与えて、キーが入ってたら翻訳データを挿入
    public void InterpretText( ref Text t )
    {
        if( t.text.StartsWith( "$$" ) )
        {
            string key = t.text.Substring(2).TrimEnd();//たまに間違って改行が入っている場合があって気づきにくいのでTrimEndしておく
            t.text = GetText( key );
        }
    }

    //------------------------------------------------------------------------------
    //タグ解釈していない、そのままのテキスト取得
    private string getTextRaw( string key )
    {
        // Debugしやすいように Application.systemLanguage を直接見ずにConfigというクラス経由で参照
        if( Config.Language == SystemLanguage.Japanese )
        {
            return texts[key].jp;
        }
        else
        {
            return texts[key].eng;
        }
    }
}
