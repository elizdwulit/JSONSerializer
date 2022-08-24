echo off

set exe=JSONProject1.exe
set filename=TestJson.txt
set add_command=add
set sub_obj=image
set newkey=testaddkey
set newval=testaddvalue
set delete_command=delete
set delkey=name
set nested_delkey=image
set modify_command=modify
set modifykey=type
set modifyval=chocolate
set modifykey2=image
set modifyval2=testreplace

echo Demo ADD 1: Adding "testaddkey": "testaddvalue" to main json body
%exe% %filename% %add_command% %newkey% %newval%

echo:

echo Demo ADD 2: Adding "testaddkey": "testaddvalue" to existing "image" key
%exe% %filename% %add_command% %sub_obj% %newkey% %newval%

echo:

echo Demo DELETE 1: Deleting "name" from json
%exe% %filename% %delete_command% %delkey%

echo:

echo Demo DELETE 2: Deleting "image" from json
%exe% %filename% %delete_command% %nested_delkey%

echo:

echo Demo MODIFY 1: Changing "type" to "chocolate"
%exe% %filename% %modify_command% %modifykey% %modifyval%

echo:

echo Demo MODIFY 2: Changing "image" to "testreplace"
%exe% %filename% %modify_command% %modifykey2% %modifyval2%