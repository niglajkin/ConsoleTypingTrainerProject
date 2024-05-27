namespace TypingTrainerProject.DataClasses;

public class Exercise(
    List<string> words
) {
    public List<string> Words { get; } = words;
}