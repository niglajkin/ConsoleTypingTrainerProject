using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.TrainingModes;

public abstract class TrainingMode {
    protected static List<string> AllSymbols => [
        "a", "s", "d", "f", "g", "h", "j", "k", "l", ";",
        "q", "w", "e", "r", "t", "y", "u", "i", "o", "p",
        "z", "x", "c", "v", "b", "n", "m", ",", ".", "/",

        "!", "?", "-", "@", "[", "]", "(", ")", ":", "=",

        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
    ];

    public abstract string Name { get; }

    public abstract string Description { get; }

    public abstract int Number { get; }

    public abstract string[] GettingInputMassage { get; }

    public abstract Predicate<string>[] CorrectInputCondition { get; }

    public abstract Exercise? GetExercise(string userChoice);
}