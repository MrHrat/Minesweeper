namespace Core
{
    using Common;

    internal class CellExplosion : Сell
    {
        public CellExplosion(Сell cell)
            : base(cell.Row, cell.Column)
        {
            Status = CellStatus.Explosion;
        }
    }
}
