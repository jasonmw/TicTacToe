namespace TicTacToe {
    public class PlaceOnTheBoard {
        public bool IsEmpty {
            get { return !TakenBy.HasValue; }
        }

        public int? TakenBy { get; set; }
    }
}