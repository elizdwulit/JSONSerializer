using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JSONProjectWPF4dot8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The path of the input file
        /// </summary>
        string inputFilePath = string.Empty;

        /// <summary>
        /// The contents of the input file as a string
        /// </summary>
        string fileContentsStr = string.Empty;

        /// <summary>
        /// Stack used to store modification history
        /// </summary>
        Stack<JSONObject> historyStack = new Stack<JSONObject>();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens a message box
        /// </summary>
        /// <param name="title">title of message box</param>
        /// <param name="message">message to show</param>
        private void showMessageBox(string title, string message)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(message, title, button, icon, MessageBoxResult.OK);
        }

        /// <summary>
        /// Get the file contents as a string
        /// </summary>
        /// <returns>contents of input file as string</returns>
        private string getFileContentsAsString()
        {
            string filePath = String.IsNullOrEmpty(inputFilePath) ? fileChooserTextBox.Text : inputFilePath;
            if (String.IsNullOrEmpty(filePath))
            {
                showMessageBox("Error", "Empty File Path. Please provide a path to an input file.");
            }
            else
            {
                return FileReader.convertFileToString(inputFilePath);
            }
            return string.Empty;
        }

        /// <summary>
        /// Create a json object from file contents
        /// </summary>
        /// <param name="inputStr">string to convert</param>
        /// <returns>new JSONObject</returns>
        private JSONObject createJSONObject(string inputStr)
        {
            string fileContents = getFileContentsAsString();
            JSONObject jsonObj = JSONParser.getJsonObject(fileContents);
            return jsonObj;
        }

        private void ResultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // do nothing
        }

        private void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "txt";
            //openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory());
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                inputFilePath = openFileDialog.FileName;
                fileChooserTextBox.Text = inputFilePath;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "JSONParserResults"; // default
            saveFileDialog.DefaultExt = ".txt"; // default
            saveFileDialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                string result = saveFile(fileName);
                if (result != null)
                {
                    showMessageBox("File Saved", "File successfully saved.");
                }
                else
                {
                    showMessageBox("Error", "Failed to save file.");
                }
            }
        }

        /// <summary>
        /// Save a file
        /// </summary>
        /// <param name="saveFileName">name of file to save</param>
        /// <returns>name of saved file, or null if error encountered</returns>
        private string saveFile(string saveFileName)
        {
            JSONObject displayedJsonObj = historyStack.Peek();
            FileSaver fileSaver = new FileSaver();
            string result = fileSaver.saveFile(displayedJsonObj, saveFileName);
            return result;
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = string.Empty;
            fileContentsStr = getFileContentsAsString();
            if (String.IsNullOrEmpty(fileContentsStr))
            {
                showMessageBox("Error", "No file contents found. Please upload a file with contents to parse.");
                return;
            }
            JSONObject jsonObj = createJSONObject(fileContentsStr);
            historyStack.Push(jsonObj);
            ResultTextBox.Text = string.Empty;
            printTreeToResultWindow(jsonObj, 1, false);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string keyToInsertInto = FindKeyTextBox.Text;
            string keyToAdd = KeyTextBox.Text;
            string valToAdd = ValueTextBox.Text;

            if (String.IsNullOrEmpty(inputFilePath))
            {
                showMessageBox("Error", "Empty File Path. Please provide a path to an input file.");
                return;
            } else if (String.IsNullOrEmpty(fileContentsStr))
            {
                showMessageBox("Error", "No file contents found. Please upload a file with contents to parse.");
                return;
            }
            else if (String.IsNullOrEmpty(keyToAdd) || String.IsNullOrEmpty(valToAdd))
            {
                showMessageBox("Error", "Key or Value missing. Please provide key and value.");
                return;
            }
            else // all info provided
            {
                ObjectModifier objModifier = new ObjectModifier();
                JSONObject currJsonObj = historyStack.Peek();
                JSONObject newJsonObj = (JSONObject)currJsonObj.Clone();
                JSONObject jsonObj = new JSONObject();
                if (String.IsNullOrEmpty(keyToInsertInto))
                {
                    jsonObj = objModifier.addKeyValuePair(newJsonObj, keyToAdd, valToAdd);
                }
                else
                {
                    jsonObj = objModifier.addKeyValuePair(newJsonObj, keyToInsertInto, keyToAdd, valToAdd);
                }

                if (jsonObj == null)
                {
                    showMessageBox("Error", "Failed to add. Make sure key does not already exist. If so, use modify to override.");
                    return;
                }
                historyStack.Push(jsonObj);
                ResultTextBox.Text = string.Empty;
                printTreeToResultWindow(jsonObj, 1, false);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string keyToDelete = KeyTextBox.Text;

            if (String.IsNullOrEmpty(inputFilePath))
            {
                showMessageBox("Error", "Empty File Path. Please provide a path to an input file.");
                return;
            }
            else if (String.IsNullOrEmpty(fileContentsStr))
            {
                showMessageBox("Error", "No file contents found. Please upload a file with contents to parse.");
                return;
            }
            else if (String.IsNullOrEmpty(keyToDelete))
            {
                showMessageBox("Error", "Key missing. Please provide key of entry to delete.");
                return;
            }
            else // all info provided
            {
                ObjectModifier objModifier = new ObjectModifier();
                JSONObject currJsonObj = historyStack.Peek();
                JSONObject newJsonObj = (JSONObject)(currJsonObj.Clone());
                JSONObject jsonObj = objModifier.deleteKeyValuePair(newJsonObj, keyToDelete);
                if (jsonObj == null)
                {
                    showMessageBox("Could not delete", "Key not found.");
                    return;
                }
                historyStack.Push(jsonObj);
                ResultTextBox.Text = string.Empty;
                printTreeToResultWindow(jsonObj, 1, false);
            }
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            string keyToModify = KeyTextBox.Text;
            string replacementVal = ValueTextBox.Text;

            if (String.IsNullOrEmpty(inputFilePath))
            {
                showMessageBox("Error", "Empty File Path. Please provide a path to an input file.");
                return;
            }
            else if (String.IsNullOrEmpty(fileContentsStr))
            {
                showMessageBox("Error", "No file contents found. Please upload a file with contents to parse.");
                return;
            }
            else if (String.IsNullOrEmpty(keyToModify))
            {
                showMessageBox("Key missing", "Please provide key of entry to modify.");
                return;
            }
            else // all info provided
            {
                ObjectModifier objModifier = new ObjectModifier();
                JSONObject currJsonObj = historyStack.Peek();
                JSONObject newJsonObj = (JSONObject)(currJsonObj.Clone());
                JSONObject jsonObj = (JSONObject)(objModifier.modifyStringValue(newJsonObj, keyToModify, replacementVal).Clone());
                if (jsonObj == null)
                {
                    showMessageBox("Could not delete", "Key not found. Nothing to delete");
                    return;
                }
                historyStack.Push(jsonObj);
                ResultTextBox.Text = string.Empty;
                printTreeToResultWindow(jsonObj, 1, false);
            }
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            if (historyStack.Count == 1)
            {
                showMessageBox("Error", "Cannot Undo. Currently displaying original tree.");
                return;
            }
            historyStack.Pop();
            JSONObject prevJsonObj = historyStack.Peek();
            ResultTextBox.Text = string.Empty;
            printTreeToResultWindow(prevJsonObj, 1, false);
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            string keyToFind = KeyTextBox.Text;

            if (String.IsNullOrEmpty(inputFilePath))
            {
                showMessageBox("Error", "Empty File Path. Please provide a path to an input file.");
                return;
            }
            else if (String.IsNullOrEmpty(fileContentsStr))
            {
                showMessageBox("Error", "No file contents found. Please upload a file with contents to parse.");
                return;
            }
            else if (String.IsNullOrEmpty(keyToFind))
            {
                showMessageBox("Error", "Key missing. Please provide key of entry to find.");
                return;
            }
            else // all info provided
            {
                ResultTextBox.Text = string.Empty;
                JSONObject currJsonObj = historyStack.Peek();
                KeyValuePair kvp = currJsonObj.getKeyValuePair(keyToFind);
                if (kvp == null)
                {
                    kvp = new KeyValuePair();
                    kvp.setKey("Error");
                    kvp.setVal("No value corresponding to input key found. Click undo to go back to json.");
                }
                JSONObject newJsonObj = new JSONObject();
                if (kvp.getVal() is string)
                {
                    newJsonObj.addKeyValuePair(kvp);
                }
                else
                {
                    newJsonObj = (JSONObject)kvp.getVal();
                }
                historyStack.Push(newJsonObj);
                printTreeToResultWindow(newJsonObj, 1, false);
            }
        }

        /// <summary>
        /// Print a json object to the console in the form of a tree
        /// </summary>
        /// <param name="jsonObj">json object to print</param>
        /// <param name="tabIndex">current index of tab</param>
        /// <param name="isNestedJson">flag indicating if printing a nested json</param>
        private void printTreeToResultWindow(JSONObject jsonObj, int tabIndex, bool isNestedJson)
        {
            List<KeyValuePair> entries = jsonObj.getAllEntries();

            int currEntry = 0;
            ResultTextBox.AppendText(Environment.NewLine);
            foreach (KeyValuePair kvp in entries)
            {
                currEntry = currEntry + 1;
                for (int i = 0; i < tabIndex; i++)
                {
                    ResultTextBox.AppendText("     ");
                    if (i == tabIndex - 1)
                    {
                        ResultTextBox.AppendText("+");
                    }
                }

                ResultTextBox.AppendText("\"");

                ResultTextBox.AppendText(kvp.getKey());
                ResultTextBox.AppendText("\"");
                ResultTextBox.AppendText(": ");

                Object val = kvp.getVal();
                if (val is JSONObject)
                {
                    printTreeToResultWindow((JSONObject)val, tabIndex + 1, true);
                }
                else
                {
                    ResultTextBox.AppendText("\"");
                    ResultTextBox.AppendText((string)val);
                    ResultTextBox.AppendText("\"");
                    ResultTextBox.AppendText(Environment.NewLine);
                }
                if (!isNestedJson)
                {
                    ResultTextBox.AppendText(Environment.NewLine);
                }
            }
            for (int i = 0; i < tabIndex - 1; i++)
            {
                ResultTextBox.AppendText("    ");
                if (i == tabIndex - 2 && !isNestedJson)
                {
                    ResultTextBox.AppendText("+");
                }
            }
        }

        /// <summary>
        /// Print a json object to the console
        /// </summary>
        /// <param name="jsonObj">json object to print</param>
        /// <param name="tabIndex">current index of tab</param>
        private void printJsonToResultWindow(JSONObject jsonObj, int tabIndex)
        {
            List<KeyValuePair> entries = jsonObj.getAllEntries();

            int currEntry = 0;
            ResultTextBox.AppendText("{");
            ResultTextBox.AppendText(Environment.NewLine);
            foreach (KeyValuePair kvp in entries)
            {
                currEntry = currEntry + 1;
                for (int i = 0; i < tabIndex; i++)
                {
                    ResultTextBox.AppendText("     ");
                }

                ResultTextBox.AppendText("\"");

                ResultTextBox.AppendText(kvp.getKey());
                ResultTextBox.AppendText("\"");
                ResultTextBox.AppendText(": ");

                Object val = kvp.getVal();
                if (val is JSONObject)
                {
                    printJsonToResultWindow((JSONObject)val, tabIndex + 1);
                    //ResultTextBox.AppendText(currEntry != entries.Count ? "," : "");
                }
                else
                {
                    ResultTextBox.AppendText("\"");
                    ResultTextBox.AppendText((string)val);
                    ResultTextBox.AppendText("\"");
                    ResultTextBox.AppendText(currEntry != entries.Count ? "," : "");
                }
                ResultTextBox.AppendText(Environment.NewLine);
            }
            for (int i = 0; i < tabIndex - 1; i++)
            {
                ResultTextBox.AppendText("     ");
            }
            ResultTextBox.AppendText("}");
        }
    }
}
