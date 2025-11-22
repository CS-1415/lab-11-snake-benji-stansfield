namespace Lab11;

public struct Cell
{
    public int Row {get;}
    public int Column {get;}

    public Cell(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public override bool Equals(object? obj) => obj is Cell c && Row == c.Row && Column == c.Column;

    public override int GetHashCode() => HashCode.Combine(Row, Column);
    public override string ToString() => $"({Row},{Column})";
}
