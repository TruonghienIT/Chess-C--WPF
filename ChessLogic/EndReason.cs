namespace ChessLogic
{
    public enum EndReason
    {
        Checkmate, //Chiếu tướng
        Stalemate,
        FiftyMoveRule, //50 nước đi
        InsufficientMaterial, //Không đủ quân
        ThreefoldRepetition, //Lặp lại thế cờ 3 lần
        Timeout //Hết thời gian
    }
}
