namespace Core
{
    using Common;

    internal class CellAbsent : Сell
    {
        public CellAbsent(Сell cell)
            : base(cell.Row, cell.Column)
        {
            Status = CellStatus.Absent;
        }
    }
}
