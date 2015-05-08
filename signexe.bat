@Echo OFF
call "%VS120COMNTOOLS%vsvars32.bat"

set /p input=Please enter your private key password: 
if "%input%"==""goto error 
signtool sign /f OracleOfSecrets.pfx /p %input% /t http://time.certum.pl OracleWin\bin\Release\OracleOfSecrets.exe
exit 
:error
echo You did not enter a valid password 
pause