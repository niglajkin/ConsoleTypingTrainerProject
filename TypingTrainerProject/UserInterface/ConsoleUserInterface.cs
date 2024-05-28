using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.UserInterface;

public class ConsoleUserInterface : IUserInterface {
    public string GetUserInput(string[] askingInputMassages, Predicate<string>[] correctInputConditions) {
        var inputPartsNumber = askingInputMassages.Length;
        var finalUserInput = "";

        var currentInputPart = 1;
        while (currentInputPart <= inputPartsNumber) {
            var partIndex = currentInputPart - 1;
            var askingInputMassage = askingInputMassages[partIndex];
            var correctInputCondition = correctInputConditions[partIndex];

            Console.WriteLine(askingInputMassage);

            var (cursorLeft, cursorTop) = Console.GetCursorPosition();
            var gettingInputContinue = true;

            while (gettingInputContinue) {
                var userInput = Console.ReadLine();

                if (userInput == null) continue;

                if (correctInputCondition(userInput)) {
                    var noFirstPart = partIndex > 0;
                    var inputIsExit = userInput == "exit";
                    var exitToMainMenu = !noFirstPart && inputIsExit;
                    var exitToPreviousPart = noFirstPart && inputIsExit;

                    if (exitToMainMenu) {
                        Console.Clear();
                        return "exit";
                    }
                    
                    if (exitToPreviousPart) {
                        currentInputPart -= 1;
                        break;
                    }
                    
                    finalUserInput += (noFirstPart ? " " : "") + userInput;
                    gettingInputContinue = false;
                    currentInputPart++;
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

        var greetingSentenceLeft = (Console.WindowWidth - greetingSentence.Length) / 2;
        var greetingSentenceTop = Console.CursorTop;

        Console.SetCursorPosition(greetingSentenceLeft, greetingSentenceTop);
        Console.WriteLine(greetingSentence);


        const bool doNotPrintPressedKey = true;

        var words = exercise.Words;
        var (displayedString, nextWordNumber) = TypingRow.GetInitStringAndLastWordNumber(words);

        foreach (var word in words) {
            TypingRow.Clean();
            TypingRow.DisplayString(displayedString);

            var currentCharIndex = 0;
            foreach (var expectedChar in word) {
                var correctCharEntered = false;
                var currentLeft = TypingRow.RowLeft + currentCharIndex;

                while (!correctCharEntered) {
                    var inputChar = Console.ReadKey(doNotPrintPressedKey).KeyChar;

                    Console.SetCursorPosition(currentLeft, TypingRow.RowTop);

                    if (inputChar == expectedChar) {
                        TypingRow.HighlightRightAnswer(expectedChar);
                        currentCharIndex++;
                        correctCharEntered = true;
                    }
                    else {
                        TypingRow.HighlightWrongAnswer(expectedChar);
                        Console.SetCursorPosition(currentLeft, TypingRow.RowTop);
                    }
                }
            }

            displayedString = displayedString[word.Length..];

            var wordsRemain = nextWordNumber < words.Count;
            if (wordsRemain) {
                var fitsInTypingRow = displayedString.Length + words[nextWordNumber].Length < TypingRow.RowLength;
                if (fitsInTypingRow) {
                    displayedString += words[nextWordNumber];
                    nextWordNumber++;
                }
            }
        }

        TypingRow.Clean();

        var finalMessage = "Congratulations! You have just completed typing exercise. Press any key to continue";
        TypingRow.DisplayString(finalMessage);

        Console.ReadKey();
        Console.Clear();
    }
}