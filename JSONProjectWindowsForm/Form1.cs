using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace JSONProjectUI
{
    public partial class Form1 : Form
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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // do nothing
        }

        /// <summary>
        /// Opens a message box
        /// </summary>
        /// <param name="title">title of message box</param>
        /// <param name="message">message to show</param>
        private void showMessageBox(string title, string message)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
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
                showMessageBox("Empty File Path", "Please provide a path to an input file.");
            } else
            {
                return FileReader.convertFileToString(inputFilePath);
            }
            return string.Empty;
        }

        private JSONObject createJSONObject(string inputStr)
        {
            string fileContents = getFileContentsAsString();
            JSONObject jsonObj = JSONParser.getJsonObject(fileContents);
            return jsonObj;
        }

        private void fileChooserButton_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "txt";
            //openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.InitialDirectory = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory());
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inputFilePath = openFileDialog1.FileName;
                fileChooserTextBox.Text = inputFilePath;
            }
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            fileContentsStr = getFileContentsAsString();
            JSONObject jsonObj = createJSONObject(fileContentsStr);
            historyStack.Push(jsonObj);
            ResultTextBox.Text = string.Empty;
            printJsonToResultWindow(jsonObj, 1);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string keyToInsertInto = FindKeyTextBox.Text;
            string keyToAdd = KeyTextBox.Text;
            string valToAdd = ValueTextBox.Text;

            if (String.IsNullOrEmpty(fileContentsStr))
            {
                showMessageBox("Empty File Path", "Please provide a path to an input file.");
                return;
            } else if (String.IsNullOrEmpty(keyToAdd) || String.IsNullOrEmpty(valToAdd)) 
            {
                showMessageBox("Key or Value missing", "Please provide key and value.");
                return;
            } else // all info provided
            {
                ObjectModifier objModifier = new ObjectModifier();
                JSONObject currJsonObj = new JSONObject();
                currJsonObj = historyStack.Peek();
                JSONObject jsonObj = new JSONObject();
                if (String.IsNullOrEmpty(keyToInsertInto))
                {
                    jsonObj = objModifier.addKeyValuePair(currJsonObj, keyToAdd, valToAdd);
                } else
                {
                    jsonObj = objModifier.addKeyValuePair(currJsonObj, keyToInsertInto, keyToAdd, valToAdd);
                }
                historyStack.Push(jsonObj);
                ResultTextBox.Text = string.Empty;
                printJsonToResultWindow(jsonObj, 1);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string keyToDelete = KeyTextBox.Text;
            
            if (String.IsNullOrEmpty(fileContentsStr))
            {
                showMessageBox("Empty File Path", "Please provide a path to an input file.");
                return;
            }
            else if (String.IsNullOrEmpty(keyToDelete))
            {
                showMessageBox("Key missing", "Please provide key of entry to delete.");
                return;
            }
            else // all info provided
            {
                ObjectModifier objModifier = new ObjectModifier();
                JSONObject currJsonObj = historyStack.Peek();
                JSONObject jsonObj = objModifier.deleteKeyValuePair(currJsonObj, keyToDelete);
                historyStack.Push(jsonObj);
                ResultTextBox.Text = string.Empty;
                printJsonToResultWindow(jsonObj, 1);
            }
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            string keyToModify = KeyTextBox.Text;
            string replacementVal = ValueTextBox.Text;

            if (String.IsNullOrEmpty(fileContentsStr))
            {
                showMessageBox("Empty File Path", "Please provide a path to an input file.");
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
                JSONObject jsonObj = objModifier.modifyStringValue(currJsonObj, keyToModify, replacementVal);
                historyStack.Push(jsonObj);
                ResultTextBox.Text = string.Empty;
                printJsonToResultWindow(jsonObj, 1);
            }
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            historyStack.Pop();
            JSONObject prevJsonObj = historyStack.Peek();
            ResultTextBox.Text = string.Empty;
            printJsonToResultWindow(prevJsonObj, 1);
        }

        /// <summary>
        /// Print a json object to the console
        /// </summary>
        /// <param name="jsonObj">json object to print</param>
        /// <param name="tabIndex"></param>
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
