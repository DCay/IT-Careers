namespace Algorithms.Recursion
{
    public class Cell
    {
        public Cell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public Cell GetCellToRight()
        {
            return new Cell(this.Row, this.Column + 1);
        }

        public Cell GetCellToLeft()
        {
            return new Cell(this.Row, this.Column - 1);
        }

        public Cell GetAboveCell()
        {
            return new Cell(this.Row - 1, this.Column);
        }

        public Cell GetBelowCell()
        {
            return new Cell(this.Row + 1, this.Column);
        }

        public override string ToString()
        {
            return $"Cell: ({this.Row}, {this.Column})";
        }
    }
}
