using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.UserInterface;

public class ConsoleUserInterface : IUserInterface {
    public string GetUserInput(string[] askingInputMassages, Predicate<string>[] correctInputConditions) {
        var arraysLength = askingInputMassages.Length;
        var finalUserInput = "";

        for (var i = 0; i < arraysLength; i++) {
            var askingInputMassage = askingInputMassages[i];
            var correctInputCondition = correctInputConditions[i];

            Console.WriteLine(askingInputMassage);

            var (cursorLeft, cursorTop) = Console.GetCursorPosition();
            var gettingInputContinue = true;

            while (gettingInputContinue) {
                var userInput = Console.ReadLine();

                if (userInput == null) continue;
                if (correctInputCondition(userInput)) {
                    var moreThanOneInput = i > 0;
                    finalUserInput += (moreThanOneInput ? " " : "") +  userInput;
                    gettingInputContinue = false;
                }
                else {
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    Console.WriteLine(new string(' ', userInput.Length));
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                }
            }

            Console.Clear();
        }

        return finalUserInput;
    }

    public void RunExercise(Exercise exercise) {
        const string greetingSentence = "Type the following sentence:";
        const int paddingLeft = 20;
        const int paddingRight = paddingLeft - 2;

        var typingRowLength = Console.WindowWidth - paddingLeft - paddingRight;

        var greetingSentenceLeft = (Console.WindowWidth - greetingSentence.Length) / 2;
        var greetingSentenceTop = Console.CursorTop;

        var typingRowLeft = Console.CursorLeft + paddingLeft;
        var typingRowTop = Console.CursorTop + 2;

        Console.SetCursorPosition(greetingSentenceLeft, greetingSentenceTop);
        Console.WriteLine(greetingSentence);


        const bool doNotPrintPressedKey = true;

        var words = exercise.Words;
        var (displayedString, nextWordNumber) = GetInitStringLastAndWordNumber(words, typingRowLength);

        foreach (var word in words) {
            CleanTypingRow(typingRowTop);
            DisplayString(displayedString, typingRowLeft, typingRowTop);

            var currentCharIndex = 0;
            foreach (var expectedChar in word) {
                var correctCharEntered = false;
                var currentTypingRowLeft = typingRowLeft + currentCharIndex;

                while (!correctCharEntered) {
                    var inputChar = Console.ReadKey(doNotPrintPressedKey).KeyChar;

                    Console.SetCursorPosition(currentTypingRowLeft, typingRowTop);

                    if (inputChar == expectedChar) {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(inputChar);
                        Console.ResetColor();
                        currentCharIndex++;
                        correctCharEntered = true;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(
                            expectedChar.Equals(' ')
                                ? '_'
                                : expectedChar
                        );
                        Console.ResetColor();
                        Console.SetCursorPosition(currentTypingRowLeft, typingRowTop);
                    }
                }
            }

            displayedString = displayedString[word.Length..];

            var wordsRemain = nextWordNumber < words.Count;
            if (wordsRemain) {
                var fitsInTypingRow = displayedString.Length + words[nextWordNumber].Length < typingRowLength;
                if (fitsInTypingRow) {
                    displayedString += words[nextWordNumber];
                    nextWordNumber++;
                }
            }
        }

        CleanTypingRow(typingRowTop);
        DisplayString(
            "Congratulations! You have just completed typing exercise. Press any key to continue",
            typingRowLeft,
            typingRowTop
        );
        Console.ReadKey();
        Console.Clear();
    }


    private void CleanTypingRow(int typingRowTop) {
        Console.SetCursorPosition(0, typingRowTop);
        Console.Write(new string(' ', Console.WindowWidth)); // Clear the sentence
        Console.SetCursorPosition(0, typingRowTop);
    }

    private (string initString, int lastWordNumber) GetInitStringLastAndWordNumber(List<string> allWords,
        int typingRowLength) {
        var charCount = 0;
        var initialString = "";
        var lastWordNumber = 0;

        foreach (var word in allWords) {
            if (charCount + word.Length < typingRowLength) {
                charCount += word.Length;
                initialString += word;
                lastWordNumber++;
            }
            else break;
        }

        return (initialString, lastWordNumber);
    }

    private void DisplayString(string line, int left, int top) {
        Console.SetCursorPosition(left, top);
        Console.WriteLine(line);
        Console.SetCursorPosition(left, top);
    }
}