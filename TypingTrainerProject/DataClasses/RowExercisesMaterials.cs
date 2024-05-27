namespace TypingTrainerProject.DataClasses;

public class RowExercisesMaterials(
    List<string[]> symbols,
    int rowNumber
) {
    public List<string[]> Symbols { get; } = symbols;
    public int RowNumber { get; } = rowNumber;
}
