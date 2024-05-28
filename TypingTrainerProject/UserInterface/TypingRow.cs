namespace TypingTrainerProject.UserInterface;

public static class TypingRow {
    private const int PaddingLeft = 20;
    private const int PaddingRight = PaddingLeft - 2;

    public static int RowLength => Console.WindowWidth - PaddingLeft - PaddingRight;

    public static int RowLeft => PaddingLeft;
    public static int RowTop => 2;

    public static (string initString, int lastWordNumber)
        GetInitStringAndLastWordNumber(List<string> allExerciseWords) {
        var charCount = 0;
        var initialString = "";
        var lastWordNumber = 0;

        foreach (var word in allExerciseWords) {
            if (charCount + word.Length < RowLength) {
                charCount += word.Length;
                initialString += word;
                lastWordNumber++;
            }
            else break;
        }

        return (initialString, lastWordNumber);
    }

    public static void Clean() {
        Console.SetCursorPosition(RowLeft, RowTop);
        Console.Write(new string(' ', Console.WindowWidth)); // Clear the sentence
        Console.SetCursorPosition(RowLeft, RowTop);
    }
    
    public static void DisplayString(string line) {
        Console.SetCursorPosition(RowLeft, RowTop);
        Console.WriteLine(line);
        Console.SetCursorPosition(RowLeft, RowTop);
    }

    public static void HighlightRightAnswer(char expectedChar) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(expectedChar);
        Console.ResetColor();
    }

    public static void HighlightWrongAnswer(char expectedChar) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(
            expectedChar.Equals(' ')
                ? '_'
                : expectedChar
        );
        Console.ResetColor();
    }
    
}