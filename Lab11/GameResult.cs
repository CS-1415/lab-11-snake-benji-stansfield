namespace Lab11;

public class GameResult
{
    public bool Finished {get;}
    public string Winner {get;}

    public GameResult(bool finished, string winner)
    {
        Finished = finished;
        Winner = winner;
    }
}