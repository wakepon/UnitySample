using UnityEngine;
using System.Collections;

public class TestMain : MonoBehaviour {
    public int movieAward = 20;//ムービーの報奨額
    public SystemLanguage languageChoice = SystemLanguage.Japanese;
    TagProcessor tagProcessor;

	// Use this for initialization
	void Start () {
        Config.ChangeLanguage( languageChoice );

        tagProcessor = new TagProcessor();//タグプロセッサー作成
        TranslateManager.Instance.SetTagProcessor( tagProcessor );//タグプロセッサー設定
        tagProcessor.SetMoviewAward( movieAward );
        TranslateManager.Instance.Read( "Text/texts" );//Resourcesから読み込む
        TranslateManager.Instance.ApplyAllUIText();//ヒエラルキーにある全Textに適用する

        //ためしに値を変えてプリント
        tagProcessor.SetMoviewAward( 100 );
        Debug.Log( TranslateManager.Instance.GetText("2000") );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
