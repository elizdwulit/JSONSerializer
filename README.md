# JSONSerializer

Elizabeth Dwulit
Software Modeling
Project 1

This project provides a program JSONProject1.exe that allows a user to provide an input txt file and transform it into a JSON object. The user can add, modify, or remove from the JSON. The output of the txt file after an operation is performed will be saved to the user’s computer.

***************************
To run without user input: in command line, use “RUN_ME.bat” found in the directory "EDwulitProject1\JSONProject1\bin\Release"
**************************

Valid commands are:
sourcefilename.txt add <newKey> <value>
sourcefilename.txt add <key> <newKey> <value>
sourcefilename.txt modify <key> <replacement string value>
sourcefilename.txt delete <key>

Example commands:
JSONProject1.exe TestJson.txt add testkey testval
JSONProject1.exe TestJson.txt add image test123 test456
JSONProject1.exe TestJson.txt modify type testing
JSONProject1.exe TestJson.txt delete image

Files in Project:
TestJson.txt - the sample json the bat uses
FileReader.cs - Class responsible for recieving and reading a file
FileSaver.cs - Class responsible for saving an output to a new file
JSONObject.cs - Represents an object in a json
JSONParser.cs - Provides a way to parse a string as a JSONObject
JSONSerializer.cs - Contains Main
KeyValuePair.cs - Represents a key-value entry in a json
ObjectFactory.cs - Class responsible for generating JSONObject(s)
ObjectFinder.cs - Class used to find objects in JSONObject
ObjectModifier.cs - Class used to insert/delete/modify a value in an JSONObject
