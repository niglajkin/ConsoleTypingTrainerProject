namespace TypingTrainerProject.DataClasses;

public class Text(
    int number,
    string title,
    List<string> words
) {
    public int Number { get; } = number;
    public string Title { get; } = title;
    public List<string> Words { get; } = words;
}