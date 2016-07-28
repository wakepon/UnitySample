using UnityEngine;

public class TagProcessor : ITagProcessor {
    protected int movieAward;

    public void SetMoviewAward( int award )
    {
        movieAward = award;
    }

    //タグを処理する
    public string Process( string text )
    {
        const string cTagWatchingMovieAward = "#{MovieAward}";

        if( text.Contains( cTagWatchingMovieAward ) )
        {
            //タグを置き換える
            text = text.Replace( cTagWatchingMovieAward, movieAward.ToString() );
        }

        return text;
    }
}
